#r @"._fake/packages/FAKE/tools/FakeLib.dll"
#r @"._fake/packages/FSharp.FakeTargets/tools/FSharp.FakeTargets.dll"

open Fake
open NuGetHelper
open RestorePackageHelper

datNET.Targets.initialize (fun p ->
  { p with
      AccessKey             = environVar "BUGSNAG_NET_NUGET_API_KEY"
      AssemblyInfoFilePaths = ["SharedAssemblyInfo.cs"]
      Authors               = ["Andrew Seward"; "Mathew Glodack"]
      Description           = "A Bugsnag notifier client for .NET projects"
      OutputPath            = "bin"
      Project               = "Bugsnag.NET"
      ProjectFilePath       = Some "Bugsnag.NET/Bugsnag.NET.csproj"
      Publish               = true
      TestAssemblies        = !! "Bugsnag.NET.Tests/**/bin/**/*.Tests.dll"
      WorkingDir            = "bin" // NOTE: This seems a bit off for the label's name of "WorkingDir"
  }
)

Target "RestorePackages" (fun _ ->
  "Bugsnag.NET.sln" |> RestoreMSSolutionPackages id
)

"MSBuild" <== [ "Clean"; "RestorePackages" ]
"Test"    <== [ "MSBuild" ]
"Package:Project" <== [ "MSBuild" ]
"Publish" <== [ "Package:Project" ]

RunTargetOrDefault "MSBuild"
