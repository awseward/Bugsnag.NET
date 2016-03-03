#r @"./._fake/packages/FAKE/tools/FakeLib.dll"

#load @"./._fake/loader.fsx"

open Fake
open NuGetHelper
open RestorePackageHelper
open FSharpVersionUtils.Fake.Config

Target "RestorePackages" (fun _ ->
  Source.SolutionFile
  |> Seq.head
  |> RestoreMSSolutionPackages (fun p ->
      { p with
          Sources = [ "https://nuget.org/api/v2"; ]
          OutputPath = "packages"
          Retries = 4 })
)

Target "MSBuild" (fun _ ->
  Source.SolutionFile
    |> MSBuildRelease null "Build"
    |> ignore
)

Target "Test" (fun _ ->
  let setParams = (fun p ->
    { p with DisableShadowCopy = true; ErrorLevel = DontFailBuild; Framework = Build.DotNetVersion; })

  Build.TestAssemblies |> NUnit setParams
)

Target "Clean" (fun _ ->
  DeleteFiles Build.MSBuildArtifacts
)

"MSBuild" <== [ "Clean"; "RestorePackages"; ]
"Test"    <== [ "MSBuild"; ]

RunTargetOrDefault "MSBuild"
