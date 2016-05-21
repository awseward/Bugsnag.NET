#load @"._fake/loader.fsx"

open Fake
open NuGetHelper
open RestorePackageHelper
open Bugsnag.NET.Fake.Config

let private _overrideConfig (parameters: datNET.Targets.ConfigParams) =
  { parameters with
      AccessKey = Nuget.ApiKey
      AssemblyInfoFilePaths = Build.AssemblyInfoFilePaths
      Authors = Release.Authors
      Description = Release.Description
      OutputPath = Release.OutputPath
      Project = Release.Project
      Publish = true
      TestAssemblies = Build.TestAssemblies
      WorkingDir = Release.WorkingDir
  }

datNET.Targets.initialize _overrideConfig

Target "RestorePackages" (fun _ ->
  Source.SolutionFile
  |> Seq.head
  |> RestoreMSSolutionPackages (fun parameters ->
      { parameters with
          Sources = [ "https://nuget.org/api/v2"; ]
          OutputPath = "packages"
          Retries = 4 })
)

"MSBuild" <== [ "Clean"; "RestorePackages" ]
"Test"    <== [ "MSBuild" ]
"Package" <== [ "MSBuild" ]
"Publish" <== [ "Package" ]

RunTargetOrDefault "MSBuild"
