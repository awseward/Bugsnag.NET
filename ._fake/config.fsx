namespace Bugsnag.NET.Fake.Config
open Fake
open System.IO

module Common =
  let RootDir = Directory.GetCurrentDirectory()

module Source =
  open Common

  let SolutionFile = !! (Path.Combine(RootDir, "*.sln"))

module Build =
  let TestAssemblies = !! "Bugsnag.NET.Tests/**/*.Tests.dll" -- "**/obj/**/*.Tests.dll"
  let DotNetVersion = "4.5"
  let MSBuildArtifacts = !! "**/bin/**.*" ++ "**/obj/**/*.*"
  let AssemblyInfoFilePaths = !! "SharedAssemblyInfo.cs"

module Nuget =
  let ApiEnvVar      = "BUGSNAG_NET_NUGET_API_KEY"
  let ApiKey         = environVar ApiEnvVar
  let PackageDirName = "nupkgs"

module Release =
  (*
    TODO: Most of this will be blocked until this can be consolidated to one
          NuGet package
  *)
  let Items = !! "**/bin/Release/*"
  let Nuspec = "Bugsnag.NET.nuspec"

  let Project = "Bugsnag.NET"
  let Authors = [ "Andrew Seward"; "Mathew Glodack" ]
  let Description = "A Bugsnag notifier client for .NET projects"
  let WorkingDir = "bin"
  let OutputPath = WorkingDir
