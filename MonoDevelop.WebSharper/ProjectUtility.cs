using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.FSharp.Core;
using MonoDevelop.Core.ProgressMonitoring;
using MonoDevelop.Projects;
using T = IntelliFactory.WebSharper.Templates;

namespace MonoDevelop.WebSharper
{
	public sealed class ProjectUtility
	{
		public static FSharpOption<T> None<T> ()
		{
			return FSharpOption<T>.None;
		}

		public static FSharpOption<T> Some<T> (T value)
		{
			return FSharpOption<T>.Some (value);
		}

		public abstract class BaseProjectBinding : DotNetProjectBinding
		{
			private T.Template template;

			public BaseProjectBinding (T.Template template)
			{
				this.template = template;
			}

			protected override DotNetProject CreateProject (string languageName, ProjectCreateInformation info, XmlElement projectOptions)
			{
				var dir = info.ProjectBasePath.FullPath.ToString ();
				var pkgs = info.SolutionPath.Combine ("packages").ToString ();
				var ns = new T.NuGetSource (ReadWebSharperPackage (), pkgs);
				var cfg = new T.InitOptions (dir, info.ProjectName, T.Source.NuGet (ns));
				template.Init (cfg);
				var path = Directory.EnumerateFiles (dir, "*.*proj").First ();
				CleanFsProj (path);
				return (DotNetProject)Project.LoadProject (path, new NullProgressMonitor ());
			}

			private T.NuGetPackage ReadWebSharperPackage ()
			{
				using (var s = typeof(ProjectUtility).Assembly.GetManifestResourceStream ("WebSharper.nupkg")) {
					return T.NuGetPackage.FromStream (s);
				}
			}

			/// this is a hack around the fact that there are F#/Web templates that use
			/// WebApp GUIDs, but this breaks them in MonoDevelop/XamarinStudio.
			/// A simple workaround is to remove the ProjectTypeGuid property.
			private void CleanFsProj (string path)
			{
				if (Path.GetExtension (path) == ".fsproj") {
					var lines = File.ReadAllLines (path).Where (line => !line.Contains ("<ProjectTypeGuids>"));
					File.WriteAllLines (path, lines, new UTF8Encoding (false, false));
				}
			}

			public override string Name {
				get {
					return this.GetType ().Name;
				}
			}
		}
	}
}
