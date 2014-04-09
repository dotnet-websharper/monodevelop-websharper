namespace MonoDevelop.WebSharper

open System
open System.IO
open System.Reflection
open System.Text
open System.Xml
open MonoDevelop
// open MonoDevelop.Core
open MonoDevelop.Core.ProgressMonitoring
open MonoDevelop.Projects
module T = IntelliFactory.WebSharper.Templates.All

#nowarn "40"

module ProjectUtility =

    let wsPackage () =
        use s = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebSharper.nupkg")
        T.NuGetPackage.FromStream(s)

    let wsSource pkgDir =
        {
            T.NuGetSource.Create() with
                NuGetPackage = wsPackage ()
                PackagesDirectory = pkgDir
        }
        |> T.Source.NuGet

    type Setup =
        | Setup of lang: string * ProjectCreateInformation * XmlElement * T.Template

    let encoding =
        UTF8Encoding(false, true)

    let noMonitor () =
        new NullProgressMonitor()
 
    /// this is a hack around the fact that there are F#/Web templates that use
    /// WebApp GUIDs, but this breaks them in MonoDevelop/XamarinStudio.
    /// A simple workaround is to remove the ProjectTypeGuid property.
    /// Also, currently WebSharper F# projects use Choose construct that does not
    /// seem to work on Mac OS X / Xamarin Studio - this func replaces it with a
    /// direct F# targets import. 
    let cleanFsProj path =
        match Path.GetExtension(path) with
        | ".fsproj" ->
            let out = ResizeArray()
            let (|ChooseStart|_|) (line: string) =
                if line.ToLower().Trim() = "<choose>" then Some () else None
            let (|ChooseEnd|_|) (line: string) =
                if line.ToLower().Trim() = "</choose>" then Some () else None
            let addFSharpTargets () =
                let line = @"  <Import Project=""$(MSBuildExtensionsPath32)\..\Microsoft SDKs\F#\3.1\Framework\v4.0\Microsoft.FSharp.Targets"" />"
                out.Add(line)
            let rec fixup lines =
                match lines with
                | ChooseStart :: lines -> skip lines
                | line :: lines -> out.Add(line); fixup lines
                | [] -> ()
            and skip lines =
                match lines with
                | ChooseEnd :: lines -> addFSharpTargets (); fixup lines
                | _ :: lines -> skip lines
                | [] -> () 
            File.ReadAllLines(path)
            |> Seq.filter (fun line -> line.Contains("<ProjectTypeGuids>") |> not)
            |> List.ofSeq
            |> fixup
            File.WriteAllLines(path, out, encoding)
        | _ -> () 

    let createProject (Setup (lang, info, opts, template)) =
        let dir = info.ProjectBasePath.FullPath.ToString()
        let pkg = info.SolutionPath.Combine("packages").ToString()
        let cfg =
            {
                T.InitOptions.Create() with
                    Directory = dir
                    ProjectName = info.ProjectName
                    Source = wsSource pkg
            }
        template.Init(cfg)
        let path = Directory.EnumerateFiles(dir, "*.*proj") |> Seq.head
        cleanFsProj path
        Project.LoadProject(path, noMonitor ()) :?> DotNetProject
 
    [<AbstractClass>]
    type BaseProjectBinding(template: T.Template) =
        inherit DotNetProjectBinding()

        override this.CreateProject(lang, info, opts) =
            createProject (Setup (lang, info, opts, template))

        override this.Name =
            this.GetType().Name
