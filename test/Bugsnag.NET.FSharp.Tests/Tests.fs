namespace Bugsnag.NET.FSharp

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

module ExceptionMetadataTests =
  open ExceptionMetadata
  open System

  [<FactAttribute>]
  let ``tryRead gives None when id not found`` () =
    Exception()
    |> tryReadMetadataId
    |> Utils.assertNone

  [<FactAttribute>]
  let ``tryRead reads metadata id`` () =
    let ex = Exception()
    let metadataId = ex |> (Utils.must tryIdentify)

    ex
    |> tryReadMetadataId
    |> Utils.assertSome
    |> Utils.assertEqual metadataId

  [<FactAttribute>]
  let ``tryRead reads ids from inner Exceptions`` () =
    let innerEx = Exception("Inner exception")
    let metadataId = innerEx |> (Utils.must tryIdentify)
    let outerEx = Exception("Outer exception", innerEx)

    outerEx
    |> tryReadMetadataId
    |> Utils.assertSome
    |> Utils.assertEqual metadataId

  [<FactAttribute>]
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
    |> tryReadMetadataId
    |> Utils.assertSome
    |> Utils.assertEqual metadataId

  [<FactAttribute>]
  let ``tryIdentify acts as read or ceate`` () =
    let ex = InvalidOperationException()
    let identify = Utils.must tryIdentify
    let preexistingId = identify ex

    ex
    |> identify
    |> Utils.assertEqual preexistingId
