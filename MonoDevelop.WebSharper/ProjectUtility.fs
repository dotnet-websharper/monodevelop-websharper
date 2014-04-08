namespace MonoDevelop.WebSharper

open System
open System.IO
open System.Text
open System.Xml
open MonoDevelop
open MonoDevelop.Components
open MonoDevelop.Components.Commands
open MonoDevelop.Core
open MonoDevelop.Core.ProgressMonitoring
open MonoDevelop.Ide
open MonoDevelop.Ide.Commands
open MonoDevelop.Projects
module T = IntelliFactory.WebSharper.Templates.All

#nowarn "40"

module ProjectUtility =

    let wsSource pkgDir =
        let opts =            
            {
                T.NuGetOptions.Create() with
                    PackagesDirectory = pkgDir
            }
        T.WebSharperSource.FromNuGet(opts = opts)

    type Setup =
        | Setup of lang: string * ProjectCreateInformation * XmlElement * T.Template

    let encoding =
        UTF8Encoding(false, true)

    let noMonitor () =
        new NullProgressMonitor()
 
    /// this is a hack around the fact that there are F#/Web templates that use
    /// WebApp GUIDs, but this breaks them in MonoDevelop/XamarinStudio.
    /// A simple workaround is to remove the ProjectTypeGuid property.
    let cleanFsProj path =
        match Path.GetExtension(path) with
        | ".fsproj" ->
            File.ReadAllLines(path)
            |> Seq.filter (fun line -> line.Contains("<ProjectTypeGuids>") |> not)
            |> fun lines -> File.WriteAllLines(path, lines, encoding)
        | _ -> () 

    let createProject (Setup (lang, info, opts, template)) =
        let dir = info.ProjectBasePath.FullPath.ToString()
        let pkg = info.SolutionPath.Combine("packages").ToString()
        let cfg =
            {
                T.InitOptions.Create() with
                    Directory = dir
                    ProjectName = info.ProjectName
                    WebSharperSource = wsSource pkg
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
