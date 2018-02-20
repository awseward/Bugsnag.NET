namespace Bugsnag.NET.FSharp

open System

module ExceptionTagging =
  let private _idKey = "__TODO_BUGSNAG_ID__"

  let rec tryRead (ex: Exception) : Guid option =
    let tryRead' () =
      try
        ex.Data.[_idKey] :?> Guid |> Some
      with
      | _ -> None

    match tryRead'() with
    | Some _ as result                -> result
    | _ when isNull ex.InnerException -> None
    | _                               -> tryRead ex.InnerException

  let tryIdentify (ex: Exception) =
    let tryWrite () =
      try
        Guid.NewGuid()
        |> (fun guid -> ex.Data.Add(_idKey, guid); guid)
        |> Some
      with
      | _ -> None

    ex
    |> tryRead
    |> function
        | Some _ as result -> result
        | _ -> tryWrite()

type MetadataTracker () =
  let mutable _map = Map.empty<Guid, Map<string, string>>

  member this.Map = _map

  member this.Set guid (key: string, value: string) =
    let map' =
      _map
      |> Map.tryFind guid
      |> function
          | Some metadata -> _map.Add(guid, metadata.Add(key, value))
          | _             -> _map.Add(guid, Map.empty.Add(key, value))

    _map <- map'

  member this.TryRemove guid =
    let metadataOpt = _map |> Map.tryFind guid

    _map <- _map.Remove guid

    metadataOpt

module MetadataTracking =
  let tryAssoc (tracker: MetadataTracker) ex (key, value) =
    ex
    |> ExceptionTagging.tryIdentify
    |> Option.iter (fun guid -> tracker.Set guid (key, value))

  let tryRemove (tracker: MetadataTracker) ex =
    ex
    |> ExceptionTagging.tryIdentify
    |> Option.bind (tracker.TryRemove)