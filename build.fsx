#r "paket: groupref Build //"

#load ".fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators
open Fake.DotNet.Testing.XUnit2

Target.create "Clean" (fun _ ->
    !! "**/bin"
    ++ "**/obj"
    |> Shell.cleanDirs 
)

Target.create "Restore" (fun _ ->
    let paketPath = Path.combine ".paket" "paket.exe"
    Shell.Exec (paketPath, "install", "")
    |> ignore
)

Target.create "Build" (fun _ ->
    !! "**/*.*proj"
    |> Seq.iter (DotNet.build id)
)

Target.create "Test" (fun _ ->
    !! "**/*Tests.dll"
    |> Fake.DotNet.Testing.XUnit2.run (fun p -> { p with HtmlOutputPath = Some (Path.combine "./RoadStatus.Tests.Output" "xunit.html")})
)

Target.create "All" ignore

"Clean"
    ==> "Restore"
    ==> "Build"
    ==> "Test"

Target.runOrDefault "All"