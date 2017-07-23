#load @"._fake/loader.fsx"

open Fake
open NuGetHelper
open RestorePackageHelper
open Bugsnag.NET.Fake.Config

datNET.Targets.initialize (fun p ->
  { p with
      AccessKey = Nuget.ApiKey
      AssemblyInfoFilePaths = Build.AssemblyInfoFilePaths
      Authors = Release.Authors
      Description = Release.Description
      OutputPath = Release.OutputPath
      Project = Release.Project
      ProjectFilePath = Some "Bugsnag.NET/Bugsnag.NET.csproj"
      Publish = true
      TestAssemblies = Build.TestAssemblies
      WorkingDir = Release.WorkingDir
  }
)

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
"Package:Project" <== [ "MSBuild" ]
"Publish" <== [ "Package:Project" ]

RunTargetOrDefault "MSBuild"
