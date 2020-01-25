using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    /// <summary>
    /// Class that scans text
    /// </summary>
    public class Scaner
    {
        /// <summary>
        /// Token list that's received as a result of text scan
        /// </summary>
        public readonly List<Token> ResultTokens = new List<Token>();
        public readonly List<Variable> Variables = new List<Variable>();

        /// <summary>
        /// A text to be scanned
        /// </summary>
        public readonly string TextToScan;

        private StringBuilder currentChain = new StringBuilder();
        private int currentPosition = 0;
        private int currentTokenToSend = 0;

		private Token LastToken => ResultTokens[ResultTokens.Count - 1];

        private char currentChar
        {
            get
            {
                try
                {
                    return TextToScan[currentPosition];
                }
                catch
                {
                    throw;
                }
            }
        }

		private char previousChar => TextToScan[currentPosition - 1];

        public Token NextToken
        {
            get
            {
                return currentTokenToSend == ResultTokens.Count ? null : ResultTokens[currentTokenToSend++];
            }
        }

        public Token PreviousToken
        {
            get
            {
                return currentTokenToSend == 0 ? null : ResultTokens[--currentTokenToSend];
            }
        }

        public Scaner(string text)
        {
            this.TextToScan = text;
        }

        public string ScanText()
        {
            try
            {
                for (currentPosition = 0; currentPosition < TextToScan.Length; currentPosition++)
                {
                    if (!Dictionaries.UnresolvedSymbols.Contains(currentChar.ToString()))
                    {
                        SkipWhitespaces();
                        //SkipComments();

                        if (currentChar == '-') // done
                        {
							if (LastToken.IsAriphmeticOperationToken())
								ScanForNumericConstant();
							else ScanForMeaningfulDelimeters();
						}
						else if (currentChar.IsDigit())
						{
							ScanForNumericConstant();
						}
                        else if (char.IsLetter(currentChar)) // done
                        {
                            ScanForKeywordsAndIdentifiers();
                        }
                        else if (currentChar == '"') // done
                        {
                            ScanForStringConstants();
                        }
                        else // done
                        {
                            ScanForMeaningfulDelimeters();
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Adds current char to chain and advance position
        /// </summary>
        private void AddCharToChainAndAdvance()
        {
            currentChain.Append(currentChar);
            currentPosition++;
        }

        /// <summary>
        /// Adding dot to chain
        /// </summary>
        private void AddDotToCurrentChain()
        {
            currentChain.Append(".");
            currentPosition++;
        }

        private void SkipWhitespaces()
        {
            while (currentChar.IsWhiteSpace())
            {
                currentPosition++;
            }
        }

        private void SkipComments()
        {
            if (currentChar == '/')
            {
                currentPosition++;
                if (currentChar == '/')
                {
                    this.SkipSigleLineComment();
                    return;
                }

                if (currentChar == '*')
                {
                    this.SkipMultilineComment();
                    return;
                }
                ThrowParseErrorException("Invalid token '/'");
                currentPosition--;
            }
        }

        private void SkipSigleLineComment()
        {
            while (currentChar != '\n')
            {
                currentPosition++;
            }
            currentPosition++;
        }

        private void SkipMultilineComment()
        {
            bool commentEnd = false;
            do
            {
                currentPosition++;
                if (currentChar == '*')
                {
                    currentPosition++;
                    if (currentChar == '/')
                    {
                        commentEnd = true;
                    }
                }
            } while (!commentEnd);
            currentPosition++;
        }

        private void ScanForNumericConstant()
        {
            bool isMinusEncountered = false;
            if (currentChar == '-')
            {
                AddCharToChainAndAdvance();
                isMinusEncountered = true;
            }

            if (currentChar.IsDigit())
            {
                CreateNumericConstToken();
            }
            else if (isMinusEncountered) // if we encountered minus sign, digits should follow them. If that's not happening, it's parse error in terms of language.
            {
                ThrowParseErrorException("After '-' sign there's no digit. It's not possible");
            }
        }

        private void CreateNumericConstToken()
        {
            AddAllDigitsToCurrentChain();

            if (currentChar.IsDoubleConstDelimeter())
            {
                AddDotToCurrentChain();
                AddAllDigitsToCurrentChain();
                if (currentChar == '.')
                {
                    ThrowParseErrorException("Unrecognized double variable on position " + currentPosition);
                }
                AddTokenFromCurrentChainValue(TokenType.DOUBLE_CONST);

                currentPosition--;
            }
            else
            {
                AddTokenFromCurrentChainValue(TokenType.INT_CONST);
                currentPosition--;
            }

        }

        private void AddAllDigitsToCurrentChain()
        {
            while (currentChar.IsDigit())
            {
                AddCharToChainAndAdvance();
            }
        }

        private void ScanForMeaningfulDelimeters()
        {
            switch (currentChar)
            {
                case '{':; AddTokenWithCurrentCharValue(TokenType.CODEBLOCK_START); break;
                case '}': AddTokenWithCurrentCharValue(TokenType.CODEBLOCK_END); break;
                case '/': AddTokenWithCurrentCharValue(TokenType.DIV); break;
                case '*': AddTokenWithCurrentCharValue(TokenType.MULTIPLY); break;
                case '+': AddMeaningfulDelimetersWithTwoCharacters('+', '+', TokenType.PLUS, TokenType.PLUS_ONE); break;
                case '-': AddMeaningfulDelimetersWithTwoCharacters('-', '-', TokenType.MINUS, TokenType.MINUS_ONE); break;
                case ';': AddTokenWithCurrentCharValue(TokenType.SEMICOLON); break;
                case '(': AddTokenWithCurrentCharValue(TokenType.PARANTHESIS_START); break;
                case ')': AddTokenWithCurrentCharValue(TokenType.PARANTHESIS_END); break;
                case '=':
                    {
                        AddMeaningfulDelimetersWithTwoCharacters('=', '=', TokenType.ASSIGN, TokenType.EQUALS);
                        break;
                    }
                case '!':
                    {
                        AddMeaningfulDelimetersWithTwoCharacters('!', '=', TokenType.NOT, TokenType.NON_EQUALS);
                        break;
                    }
                case '<':
                    {
                        AddMeaningfulDelimetersWithTwoCharacters('<', '=', TokenType.LESS, TokenType.LESS_OR_EQUAL);
                        break;
                    }
                case '>':
                    {
                        AddMeaningfulDelimetersWithTwoCharacters('>', '=', TokenType.MORE, TokenType.MORE_OR_EQUAL);
                        break;
                    }
                case '|':
                    {
                        currentPosition++;
                        if (currentChar == '|')
                        {
                            ResultTokens.Add(new Token(TokenType.LOGICAL_OR, "||"));

                        }
                        else ThrowParseErrorException("'|' expected");
                        break;
                    }
                case '&':
                    {
                        currentPosition++;
                        if (currentChar == '&')
                        {
                            ResultTokens.Add(new Token(TokenType.LOGICAL_AND, "&&"));
                            break;
                        }
                        else ThrowParseErrorException("'&' expected");
                        break;
                    }
            }
        }

        /// <summary>
        /// Scans for keywords (including '<see langword="true"/>' and '<see langword="false"/>) and identifiers
        /// </summary>
        private void ScanForKeywordsAndIdentifiers()
        {
            if ('a' <= currentChar && currentChar <= 'z')
            {
                while ('a' <= currentChar && currentChar <= 'z' || currentChar == '.' || '0' <= currentChar && currentChar <= '9')
                {
                    AddCharToChainAndAdvance();
                }

                if (currentChain.Length > 0)
                {
                    string chain = currentChain.ToString();

                    //Check for bool values
                    if (chain.Length >= 4)
                    {
                        switch (chain)
                        {
                            case "true":
                                {
                                    AddTokenFromCurrentChainValue(TokenType.BOOLEAN_TRUE);
                                    currentPosition--;
                                    return;
                                }
                            case "false":
                                {
                                    AddTokenFromCurrentChainValue(TokenType.BOOLEAN_FALSE);
                                    currentPosition--;
                                    return;
                                }
                        }
                    }

                    Token languageKeyword = Dictionaries.LanguageKeywords.SingleOrDefault(x => x.Value == chain);
                    if (languageKeyword != null)
                    {
                        AddTokenFromCurrentChainValue(languageKeyword.TokenType);
                        CheckAllowedCharacterAfterKeyword();
                    }
                    if (chain != " ")
                        AddTokenFromCurrentChainValue(TokenType.VARIABLE);
                    if (!this.Variables.Any(x => x.Name == chain))
                    {
                        this.Variables.Add(new Variable(chain));
                    }
                    currentPosition--;
                }
            }
        }

        private void ScanForStringConstants()
        {
            do
            {
                AddCharToChainAndAdvance();
            }
            while (currentChar != '"');
            AddCharToChainAndAdvance();
            AddTokenFromCurrentChainValue(TokenType.STRING_CONST);
        }

        /// <summary>
        /// Adds delimeter with 2 characters or 1 (e.g. = or ==, ! or !=)
        /// </summary>
        private void AddMeaningfulDelimetersWithTwoCharacters(char firstChar, char secondChar, TokenType caseOneChar, TokenType caseTwoChars)
        {
            currentPosition++;
            if (currentChar == secondChar)
            {
                string tokenValue = firstChar.ToString() + secondChar.ToString();
                ResultTokens.Add(new Token(caseTwoChars, tokenValue));
            }
            else
            {
                ResultTokens.Add(new Token(caseOneChar, firstChar.ToString()));
                currentPosition--; // rollback position change in case we've found non-two characters delimeter.
            }
        }

        /// <summary>
        /// Add the token with current char value to results token set
        /// </summary>
        /// <param name="tokenType"></param>
        private void AddTokenWithCurrentCharValue(TokenType tokenType)
        {
            Token newToken = new Token(tokenType, currentChar.ToString());
            ResultTokens.Add(newToken);
        }

        /// <summary>
        /// Add the token with current chain value to results token set and clears the chain.
        /// </summary>
        /// <param name="tokenType"></param>
        private void AddTokenFromCurrentChainValue(TokenType tokenType)
        {
            Token newToken = new Token(tokenType, currentChain.ToString());
            if (newToken.Value != "")
                ResultTokens.Add(newToken);
            currentChain.Clear();
        }

        private void CheckAllowedCharacterAfterKeyword()
        {
            char[] charsAllowedAfterKeyword = { '(', '{', ';', ' ', ')' };

            if (charsAllowedAfterKeyword.Any(x => x == currentChar))
            {
                return;
            }
            else ThrowParseErrorException("Bad character after keyword/identifier declaration");
        }

        private void ThrowParseErrorException(string message)
        {
            // throw new ParseErrorException(currentPosition, message);
        }
    }

}