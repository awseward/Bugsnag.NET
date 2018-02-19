namespace Bugsnag.NET.FSharp

open System

module ExceptionMetadata =

  let private _idKey = "__TODO_BUGSNAG_ID__"

  let rec tryReadMetadataId (ex: Exception) : Guid option =
    try
      ex.Data.[_idKey] :?> Guid |> Some
    with
    | _ -> None
    |> function
        | None ->
            if isNull ex.InnerException
            then None
            else tryReadMetadataId ex.InnerException
        | idOpt -> idOpt

  let tryIdentify (ex: Exception) =
    ex
    |> tryReadMetadataId
    |> function
        | None ->
            let newId = Guid.NewGuid()

            try
                ex.Data.[_idKey] <- (newId :> obj)
                Some newId
            with
            | _ -> None
        | existing -> existing

module Thing =
  open Microsoft.FSharp.Collections

  type TagSetId = Guid

  type TagSet =
    { Id: TagSetId
      Tags: Map<string, string list> }

  let private _tagSets = Map.empty<TagSetId, TagSet>

  let tag (ex: Exception) (key: string) (value: string) : unit =
    ()

  let tryGetTags (ex: Exception) : TagSet option =
    None