using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controls.Code
{
    public readonly struct LanguageSpecification
    {
        public string Name { get; init; }

        public string JavascriptLocation { get; init; }

        public static readonly LanguageSpecification CSharpSpecification = new() { Name = "csharp", JavascriptLocation = "/_content/Controls/languages/csharp.min.js" };
        public static readonly LanguageSpecification CssSpecification = new() { Name = "css", JavascriptLocation = "/_content/Controls/languages/css.min.js" };
        public static readonly LanguageSpecification HtmlSpecification = new() { Name = "xml", JavascriptLocation = "/_content/Controls/languages/xml.min.js" };
        public static readonly LanguageSpecification JavascriptSpecification = new() { Name = "javascript", JavascriptLocation = "/_content/Controls/languages/javascript.min.js" };
    }
}
