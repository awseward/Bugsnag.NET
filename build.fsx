#r @"./packages/fake/tools/FakeLib.dll"
#r @"./packages/fsharp.faketargets/tools/FSharp.FakeTargets.dll"

open Fake
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

Target "RestorePackages" (fun _ ->
  "Bugsnag.NET.sln" |> RestoreMSSolutionPackages id
)

"MSBuild"         <== ["Clean"; "RestorePackages"]
"Test"            <== ["MSBuild"]
"Package:Project" <== ["MSBuild"]
"Publish"         <== ["Package:Project"]

RunTargetOrDefault "MSBuild"
