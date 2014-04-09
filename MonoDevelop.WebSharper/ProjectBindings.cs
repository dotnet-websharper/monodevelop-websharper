using System;
using T = IntelliFactory.WebSharper.Templates;

namespace MonoDevelop.WebSharper
{
	public sealed class BundleWebsiteProjectBinding : ProjectUtility.BaseProjectBinding
	{
		public BundleWebsiteProjectBinding () : base (T.Template.BundleWebsite)
		{
		}
	}

	public sealed class ExtensionProjectBinding : ProjectUtility.BaseProjectBinding
	{
		public ExtensionProjectBinding () : base (T.Template.Extension)
		{
		}
	}

	public sealed class LibraryProjectBinding : ProjectUtility.BaseProjectBinding
	{
		public LibraryProjectBinding () : base (T.Template.Library)
		{
		}
	}

	public sealed class SiteletsHostProjectBinding : ProjectUtility.BaseProjectBinding
	{
		public SiteletsHostProjectBinding () : base (T.Template.SiteletsHost)
		{
		}
	}

	public sealed class SiteletsHtmlProjectBinding : ProjectUtility.BaseProjectBinding
	{
		public SiteletsHtmlProjectBinding () : base (T.Template.SiteletsHtml)
		{
		}
	}

	public sealed class SiteletsWebsiteProjectBinding : ProjectUtility.BaseProjectBinding
	{
		public SiteletsWebsiteProjectBinding () : base (T.Template.SiteletsWebsite)
		{
		}
	}
}
