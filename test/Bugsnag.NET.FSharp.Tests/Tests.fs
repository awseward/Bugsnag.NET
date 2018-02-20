namespace Bugsnag.NET.FSharp

open System
open Xunit

module Utils =
  let assertEqual<'a> (expected: 'a) (actual: 'a) =
    Assert.Equal(expected, actual)

  let assertSome<'a> (input: 'a option) =
    input
    |> Option.isSome<'a>
    |> (fun isSome ->
        let failureMessage =
            input
            |> sprintf "Expected Some<%s>, but got: %A" typeof<'a>.Name

        Assert.True(isSome, failureMessage))

    input.Value

  let assertNone input =
    input
    |> Option.isNone
    |> (fun isNone -> Assert.True(isNone, sprintf "Expected None, but got: %A" input))

    input

  let mustBeSome input =
    match input with
    | Some x -> x
    | None ->
        input
        |> sprintf "Cannot be None. Input: %A"
        |> failwith

  let must fn input =
    match fn input with
    | Some x -> x
    | None ->
        input
        |> sprintf "Result cannot be None. Input: %A"
        |> failwith

module ExceptionTaggingTests =
  open ExceptionTagging

  [<Fact>]
  let ``tryRead gives None when id not found`` () =
    Exception()
    |> tryRead
    |> Utils.assertNone

  [<Fact>]
  let ``tryRead reads metadata id`` () =
    let ex = Exception()
    let metadataId = ex |> (Utils.must tryIdentify)

    ex
    |> tryRead
    |> Utils.assertSome
    |> Utils.assertEqual metadataId

  [<Fact>]
  let ``tryRead reads ids from inner Exceptions`` () =
    let innerEx = Exception("Inner exception")
    let metadataId = innerEx |> (Utils.must tryIdentify)
    let outerEx = Exception("Outer exception", innerEx)

    outerEx
    |> tryRead
    |> Utils.assertSome
    |> Utils.assertEqual metadataId

  [<Fact>]
  let ``tryRead reads ids from deeply nested Exceptions`` () =
    let rec wrapException maxDepth depth ex =
      if depth >= maxDepth
      then ex
      else
        ex
        |> (fun e -> Exception(sprintf "Wrapping exception (%i)" depth, e))
        |> wrapException maxDepth (depth + 1)

    let innerMostEx = Exception("Inner exception")
    let outerMostEx = wrapException 5 0 innerMostEx
    let metadataId = innerMostEx |> (Utils.must tryIdentify)

    outerMostEx
    |> tryRead
    |> Utils.assertSome
    |> Utils.assertEqual metadataId

  [<Fact>]
  let ``tryIdentify acts as read or ceate`` () =
    let ex = InvalidOperationException()
    let identify = Utils.must tryIdentify
    let preexistingId = identify ex

    ex
    |> identify
    |> Utils.assertEqual preexistingId

module MetadataTrackerTests =
  [<Fact>]
  let ``Set assigns a key and value under a GUID`` () =
    let tracker = MetadataTracker()
    let guid = Guid.NewGuid()
    let (foo, bar) as fooBar = ("foo", "bar")

    tracker.Set guid fooBar

    tracker.Map
    |> Utils.assertEqual (Map [(guid, (Map [(foo, bar)]))])

  [<Fact>]
  let ``TryRemove gives the map under the given Guid`` () =
    let tracker = MetadataTracker()
    let guid = Guid.NewGuid()
    let (foo, bar) as fooBar = ("foo", "bar")

    tracker.Set guid fooBar

    guid
    |> tracker.TryRemove
    |> Utils.assertSome
    |> Utils.assertEqual (Map [(foo, bar)])

module MetadataTrackingTests =
  [<Fact>]
  let ``tryAssoc associates a key and value to an Exception`` () =
    let tracker = MetadataTracker()
    let ex = Exception("Oops!")
    let (foo, bar) as fooBar = ("foo", "bar")

    fooBar |> MetadataTracking.tryAssoc tracker ex

    ex
    |> MetadataTracking.tryRemove tracker
    |> Utils.assertSome
    |> Utils.assertEqual (Map [(foo, bar)])