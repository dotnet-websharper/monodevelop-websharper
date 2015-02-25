namespace MonoDevelop.WebSharper

module PU = ProjectUtility
module T = WebSharper.Templates.All

[<Sealed>]
type BundleWebsiteProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.BundleWebsite)

[<Sealed>]
type ExtensionProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.Extension)

[<Sealed>]
type LibraryProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.Library)
   
[<Sealed>]
type SiteletsHostProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsHost)

[<Sealed>]
type SiteletsHtmlProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsHtml)

[<Sealed>]
type SiteletsWebsiteProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsWebsite)
