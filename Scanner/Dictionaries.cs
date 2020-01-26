using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    /// <summary>
    /// Different dictionaries class that helps with language keywords and identifiers
    /// </summary>
    internal static class Dictionaries
    {
        /// <summary>
        /// The list of keywords that's supported in language
        /// </summary>
        public static readonly List<string> LanguageKeywords = new List<string>()
        {
            "consolelog",
            "var",
            "const",
            "if",
            "else",
            "for",
            "function"
        };

        public static readonly List<string> UnresolvedSymbols = new List<string>()
        {
            "~",
            "`",
            "@",
            "#",
            "№",
            "$",
            "^",
            ":",
            "?",
            ""
        };
    }
}
