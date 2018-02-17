namespace Bugsnag.NET.FSharp

open System

module ExceptionMetadata =

  let private _idKey = "__TODO_BUGSNAG_ID__"

  let tryReadMetadataId (ex: Exception) : Guid option =
    try
      ex.Data.[_idKey] :?> Guid |> Some
    with
    | _ -> None

  let tryWriteNewMetadataId (ex: Exception) : Guid option =
    let newId = Guid.NewGuid()

    try
        ex.Data.[_idKey] <- (newId :> obj)
        Some newId
    with
    | _ -> None

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