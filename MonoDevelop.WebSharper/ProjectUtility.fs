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
module T = WebSharper.Templates.All

#nowarn "40"

module ProjectUtility =

    type Setup =
        | Setup of lang: string * ProjectCreateInformation * XmlElement * T.Template

    let encoding =
        UTF8Encoding(false, true)

    let noMonitor () =
        new NullProgressMonitor()
 
    let createProject (Setup (lang, info, opts, template)) =
        let dir = info.ProjectBasePath.FullPath.ToString()
        let pkg = info.SolutionPath.Combine("packages").ToString()
        let cfg =
            T.InitOptions.Create(
                directory = dir,
                projectName = info.ProjectName,
                templatesPackage = (
                    use s = Assembly.GetExecutingAssembly().GetManifestResourceStream("WebSharper.Templates.nupkg")
                    use m = new MemoryStream()
                    s.CopyTo(m)
                    m.ToArray()
                )
            )
        template.Init(cfg)
        let path = Directory.EnumerateFiles(dir, "*.*proj") |> Seq.head
        Project.LoadProject(path, noMonitor ()) :?> DotNetProject
 
    [<AbstractClass>]
    type BaseProjectBinding(template: T.Template) =
        inherit DotNetProjectBinding()

        override this.CreateProject(lang, info, opts) =
            createProject (Setup (lang, info, opts, template))

        override this.Name =
            this.GetType().Name
