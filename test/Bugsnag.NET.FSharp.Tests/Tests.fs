namespace Bugsnag.NET.FSharp

open Xunit

module TestingUtils =
  let assertSome<'a> = Option.isSome<'a> >> Assert.True
  let assertNone<'a> = Option.isNone<'a> >> Assert.True

module ExceptionMetadataTests =
  open ExceptionMetadata
  open System

  [<FactAttribute>]
  let ``tryReadMetadataId gives None when a metadata id has not been assigned`` () =
    let ex = InvalidOperationException()

    ex
    |> tryReadMetadataId
    |> TestingUtils.assertNone

  [<FactAttribute>]
  let ``tryReadMetadataId reads the correct metadata id that was written`` () =
    let ex = InvalidOperationException()

    match tryWriteNewMetadataId ex with
    | Some idFromWrite ->
        ex
        |> tryReadMetadataId
        |> function
            | Some idFromRead -> Assert.Equal(idFromWrite, idFromRead)
            | _ -> failwith "FIXME"
    | _ -> failwith "FIXME"
