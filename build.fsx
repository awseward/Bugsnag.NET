#r @"._fake/packages/FAKE/tools/FakeLib.dll"
#r @"._fake/packages/FSharp.FakeTargets/tools/FSharp.FakeTargets.dll"

open Fake
open Fake.Testing.XUnit2
open NuGetHelper
open RestorePackageHelper

datNET.Targets.initialize (fun p ->
  { p with
      AccessKey             = environVar "BUGSNAG_NET_NUGET_API_KEY"
      AssemblyInfoFilePaths = ["SharedAssemblyInfo.cs"]
      Authors               = ["Andrew Seward"; "Mathew Glodack"]
      Description           = "A Bugsnag notifier client for .NET projects"
      OutputPath            = "."
      Project               = "Bugsnag.NET"
      ProjectFilePath       = Some "Bugsnag.NET/Bugsnag.NET.csproj"
      Publish               = true
      TestAssemblies        = !! "Bugsnag.NET.Tests/**/bin/**/*.Tests.dll"
      WorkingDir            = "."
  }
)

//let cleanRebuild = Fake.MSBuildHelper.MSBuild null "Clean;Rebuild"

Target "Xunit" (fun _ ->
  "test/Bugsnag.NET.FSharp.Tests/bin/Release/Bugsnag.NET.FSharp.Tests.dll"
  |> Seq.singleton
  |> xUnit2 id
)

Target "RestorePackages" (fun _ ->
  "Bugsnag.NET.sln" |> RestoreMSSolutionPackages id
)

"MSBuild"         <== ["Clean"; "RestorePackages"]
"Test"            <== ["MSBuild"; "Xunit"]
"Xunit"           <== ["MSBuild"]
"Package:Project" <== ["MSBuild"]
"Publish"         <== ["Package:Project"]

RunTargetOrDefault "MSBuild"
