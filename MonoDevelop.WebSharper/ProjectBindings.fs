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
type OwinSelfHostProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.OwinSelfHost)
   
[<Sealed>]
type SiteletsHostProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsHost)

[<Sealed>]
type SiteletsHtmlProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsHtml)

[<Sealed>]
type SiteletsWebsiteProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsWebsite)

[<Sealed>]
type BundleUINextProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.BundleUINext)

[<Sealed>]
type SiteletsUINextProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsUINext)

[<Sealed>]
type SiteletsUINextSuaveProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.SiteletsUINextSuave)
