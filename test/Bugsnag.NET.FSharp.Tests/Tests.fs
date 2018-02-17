namespace Bugsnag.NET.FSharp

open Xunit

module Utils =
  let assertEqual<'a> (expected: 'a) (actual: 'a) =
    Assert.Equal(expected, actual)

  let assertSome input =
    input
    |> (Option.isSome >> Assert.True)

    input.Value

  let assertNone input =
    input
    |> (Option.isNone >> Assert.True)

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
  let ``tryReadMetadataId gives None when id not found`` () =
    InvalidOperationException()
    |> tryReadMetadataId
    |> Utils.assertNone

  [<FactAttribute>]
  let ``tryReadMetadataId reads metadata id`` () =
    let ex = InvalidOperationException()
    let preexistingId =
      ex
      |> (Utils.must tryIdentify)

    ex
    |> tryReadMetadataId
    |> function
        | Some idFromRead -> Utils.assertEqual preexistingId idFromRead
        | _ -> failwith "Failed to read"

  [<FactAttribute>]
  let ``tryIdentify acts as read or ceate`` () =
    let ex = InvalidOperationException()
    let identify = Utils.must tryIdentify
    let preexistingId = identify ex

    ex
    |> identify
    |> Utils.assertEqual preexistingId