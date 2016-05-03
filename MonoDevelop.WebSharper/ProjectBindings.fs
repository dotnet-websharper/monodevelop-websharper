namespace MonoDevelop.WebSharper

module PU = ProjectUtility
module T = WebSharper.Templates.All

[<Sealed>]
type CSharpBundleWebsiteProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.CSharpBundleWebsite)

[<Sealed>]
type CSharpLibraryProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.CSharpLibrary)

[<Sealed>]
type CSharpBundleUINextProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.CSharpBundleUINext)

[<Sealed>]
type CSharpSiteletsUINextProjectBinding() =
    inherit PU.BaseProjectBinding(T.Template.CSharpSiteletsUINext)
