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
        internal static readonly List<Token> LanguageKeywords = new List<Token>()
        {
            new Token("start", TokenType.START),
            new Token("int", TokenType.INTEGER),
            new Token("double", TokenType.DOUBLE),
            new Token("string", TokenType.STRING),
            new Token("boolean", TokenType.BOOLEAN),
            new Token("if", TokenType.IF),
            new Token("else", TokenType.ELSE),
            new Token("for", TokenType.FOR),
            new Token("true", TokenType.TRUE),
            new Token("false", TokenType.FALSE),
            new Token("output", TokenType.OUTPUT)
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
            "",
            " "
        };
    }
}
