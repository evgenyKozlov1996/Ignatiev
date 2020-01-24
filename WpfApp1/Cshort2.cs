
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using WpfApp1;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF                =  0, // (EOF)
        SYMBOL_ERROR              =  1, // (Error)
        SYMBOL_COMMENT            =  2, // Comment
        SYMBOL_NEWLINE            =  3, // NewLine
        SYMBOL_WHITESPACE         =  4, // Whitespace
        SYMBOL_TIMESDIV           =  5, // '*/'
        SYMBOL_DIVTIMES           =  6, // '/*'
        SYMBOL_DIVDIV             =  7, // '//'
        SYMBOL_MINUS              =  8, // '-'
        SYMBOL_MINUSMINUS         =  9, // '--'
        SYMBOL_EXCLAM             = 10, // '!'
        SYMBOL_EXCLAMEQ           = 11, // '!='
        SYMBOL_AMPAMP             = 12, // '&&'
        SYMBOL_LPAREN             = 13, // '('
        SYMBOL_RPAREN             = 14, // ')'
        SYMBOL_TIMES              = 15, // '*'
        SYMBOL_COMMA              = 16, // ','
        SYMBOL_DIV                = 17, // '/'
        SYMBOL_COLON              = 18, // ':'
        SYMBOL_SEMI               = 19, // ';'
        SYMBOL_LBRACKET           = 20, // '['
        SYMBOL_RBRACKET           = 21, // ']'
        SYMBOL_LBRACE             = 22, // '{'
        SYMBOL_PIPEPIPE           = 23, // '||'
        SYMBOL_RBRACE             = 24, // '}'
        SYMBOL_PLUS               = 25, // '+'
        SYMBOL_PLUSPLUS           = 26, // '++'
        SYMBOL_LT                 = 27, // '<'
        SYMBOL_LTEQ               = 28, // '<='
        SYMBOL_EQ                 = 29, // '='
        SYMBOL_EQEQ               = 30, // '=='
        SYMBOL_GT                 = 31, // '>'
        SYMBOL_GTEQ               = 32, // '>='
        SYMBOL_BOOL               = 33, // bool
        SYMBOL_CHARLITERAL        = 34, // CharLiteral
        SYMBOL_DECLITERAL         = 35, // DecLiteral
        SYMBOL_DOUBLE             = 36, // double
        SYMBOL_ELSE               = 37, // else
        SYMBOL_FALSE              = 38, // false
        SYMBOL_FOR                = 39, // for
        SYMBOL_HEXLITERAL         = 40, // HexLiteral
        SYMBOL_IDENTIFIER         = 41, // Identifier
        SYMBOL_IF                 = 42, // if
        SYMBOL_INT                = 43, // int
        SYMBOL_MEMBERNAME         = 44, // MemberName
        SYMBOL_NULL               = 45, // null
        SYMBOL_REALLITERAL        = 46, // RealLiteral
        SYMBOL_START              = 47, // START
        SYMBOL_STRING             = 48, // string
        SYMBOL_STRINGLITERAL      = 49, // StringLiteral
        SYMBOL_TRUE               = 50, // true
        SYMBOL_ADDEXP             = 51, // <Add Exp>
        SYMBOL_ANDEXP             = 52, // <And Exp>
        SYMBOL_ARGLIST            = 53, // <Arg List>
        SYMBOL_ARGLISTOPT         = 54, // <Arg List Opt>
        SYMBOL_ASSIGNTAIL         = 55, // <Assign Tail>
        SYMBOL_BASETYPE           = 56, // <Base Type>
        SYMBOL_BLOCK              = 57, // <Block>
        SYMBOL_BLOCKORSEMI        = 58, // <Block or Semi>
        SYMBOL_COMPAREEXP         = 59, // <Compare Exp>
        SYMBOL_COMPILATIONUNIT    = 60, // <Compilation Unit>
        SYMBOL_DIMSEPARATORS      = 61, // <Dim Separators>
        SYMBOL_EQUALITYEXP        = 62, // <Equality Exp>
        SYMBOL_EXPRESSION         = 63, // <Expression>
        SYMBOL_EXPRESSIONLIST     = 64, // <Expression List>
        SYMBOL_FORCONDITIONOPT    = 65, // <For Condition Opt>
        SYMBOL_FORINITOPT         = 66, // <For Init Opt>
        SYMBOL_FORITERATOROPT     = 67, // <For Iterator Opt>
        SYMBOL_LITERAL            = 68, // <Literal>
        SYMBOL_LOCALVARDECL       = 69, // <Local Var Decl>
        SYMBOL_METHOD             = 70, // <Method>
        SYMBOL_METHODEXP          = 71, // <Method Exp>
        SYMBOL_METHODSOPT         = 72, // <Methods Opt>
        SYMBOL_MULTEXP            = 73, // <Mult Exp>
        SYMBOL_NORMALSTM          = 74, // <Normal Stm>
        SYMBOL_OBJECTEXP          = 75, // <Object Exp>
        SYMBOL_OREXP              = 76, // <Or Exp>
        SYMBOL_OTHERTYPE          = 77, // <Other Type>
        SYMBOL_POINTEROPT         = 78, // <Pointer Opt>
        SYMBOL_PRIMARY            = 79, // <Primary>
        SYMBOL_PRIMARYEXP         = 80, // <Primary Exp>
        SYMBOL_QUALIFIEDID        = 81, // <Qualified ID>
        SYMBOL_RANKSPECIFIER      = 82, // <Rank Specifier>
        SYMBOL_RANKSPECIFIERS     = 83, // <Rank Specifiers>
        SYMBOL_STATEMENT          = 84, // <Statement>
        SYMBOL_STATEMENTEXP       = 85, // <Statement Exp>
        SYMBOL_STATEMENTEXPLIST   = 86, // <Statement Exp List>
        SYMBOL_STMLIST            = 87, // <Stm List>
        SYMBOL_THENSTM            = 88, // <Then Stm>
        SYMBOL_UNARYEXP           = 89, // <Unary Exp>
        SYMBOL_VALIDID            = 90, // <Valid ID>
        SYMBOL_VARIABLEDECLARATOR = 91, // <Variable Declarator>
        SYMBOL_VARIABLEDECS       = 92  // <Variable Decs>
    };

    enum RuleConstants : int
    {
        RULE_BLOCKORSEMI                           =  0, // <Block or Semi> ::= <Block>
        RULE_BLOCKORSEMI_SEMI                      =  1, // <Block or Semi> ::= ';'
        RULE_VALIDID_IDENTIFIER                    =  2, // <Valid ID> ::= Identifier
        RULE_VALIDID                               =  3, // <Valid ID> ::= <Base Type>
        RULE_QUALIFIEDID                           =  4, // <Qualified ID> ::= <Valid ID>
        RULE_LITERAL_TRUE                          =  5, // <Literal> ::= true
        RULE_LITERAL_FALSE                         =  6, // <Literal> ::= false
        RULE_LITERAL_DECLITERAL                    =  7, // <Literal> ::= DecLiteral
        RULE_LITERAL_HEXLITERAL                    =  8, // <Literal> ::= HexLiteral
        RULE_LITERAL_REALLITERAL                   =  9, // <Literal> ::= RealLiteral
        RULE_LITERAL_CHARLITERAL                   = 10, // <Literal> ::= CharLiteral
        RULE_LITERAL_STRINGLITERAL                 = 11, // <Literal> ::= StringLiteral
        RULE_LITERAL_NULL                          = 12, // <Literal> ::= null
        RULE_POINTEROPT_TIMES                      = 13, // <Pointer Opt> ::= '*'
        RULE_POINTEROPT                            = 14, // <Pointer Opt> ::= 
        RULE_BASETYPE                              = 15, // <Base Type> ::= <Other Type>
        RULE_BASETYPE_INT                          = 16, // <Base Type> ::= int
        RULE_OTHERTYPE_DOUBLE                      = 17, // <Other Type> ::= double
        RULE_OTHERTYPE_BOOL                        = 18, // <Other Type> ::= bool
        RULE_OTHERTYPE_STRING                      = 19, // <Other Type> ::= string
        RULE_RANKSPECIFIERS                        = 20, // <Rank Specifiers> ::= <Rank Specifiers> <Rank Specifier>
        RULE_RANKSPECIFIERS2                       = 21, // <Rank Specifiers> ::= <Rank Specifier>
        RULE_RANKSPECIFIER_LBRACKET_RBRACKET       = 22, // <Rank Specifier> ::= '[' <Dim Separators> ']'
        RULE_DIMSEPARATORS_COMMA                   = 23, // <Dim Separators> ::= <Dim Separators> ','
        RULE_DIMSEPARATORS                         = 24, // <Dim Separators> ::= 
        RULE_EXPRESSIONLIST                        = 25, // <Expression List> ::= <Expression>
        RULE_EXPRESSIONLIST_COMMA                  = 26, // <Expression List> ::= <Expression> ',' <Expression List>
        RULE_EXPRESSION_EQ                         = 27, // <Expression> ::= <Or Exp> '=' <Expression>
        RULE_EXPRESSION                            = 28, // <Expression> ::= <Or Exp>
        RULE_OREXP_PIPEPIPE                        = 29, // <Or Exp> ::= <Or Exp> '||' <And Exp>
        RULE_OREXP                                 = 30, // <Or Exp> ::= <And Exp>
        RULE_ANDEXP_AMPAMP                         = 31, // <And Exp> ::= <And Exp> '&&' <Equality Exp>
        RULE_ANDEXP                                = 32, // <And Exp> ::= <Equality Exp>
        RULE_EQUALITYEXP_EQEQ                      = 33, // <Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
        RULE_EQUALITYEXP_EXCLAMEQ                  = 34, // <Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
        RULE_EQUALITYEXP                           = 35, // <Equality Exp> ::= <Compare Exp>
        RULE_COMPAREEXP_LT                         = 36, // <Compare Exp> ::= <Compare Exp> '<' <Add Exp>
        RULE_COMPAREEXP_GT                         = 37, // <Compare Exp> ::= <Compare Exp> '>' <Add Exp>
        RULE_COMPAREEXP_LTEQ                       = 38, // <Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
        RULE_COMPAREEXP_GTEQ                       = 39, // <Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
        RULE_COMPAREEXP                            = 40, // <Compare Exp> ::= <Add Exp>
        RULE_ADDEXP_PLUS                           = 41, // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
        RULE_ADDEXP_MINUS                          = 42, // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
        RULE_ADDEXP                                = 43, // <Add Exp> ::= <Mult Exp>
        RULE_MULTEXP_TIMES                         = 44, // <Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
        RULE_MULTEXP_DIV                           = 45, // <Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
        RULE_MULTEXP                               = 46, // <Mult Exp> ::= <Unary Exp>
        RULE_UNARYEXP_EXCLAM                       = 47, // <Unary Exp> ::= '!' <Unary Exp>
        RULE_UNARYEXP_MINUS                        = 48, // <Unary Exp> ::= '-' <Unary Exp>
        RULE_UNARYEXP_PLUSPLUS                     = 49, // <Unary Exp> ::= '++' <Unary Exp>
        RULE_UNARYEXP_MINUSMINUS                   = 50, // <Unary Exp> ::= '--' <Unary Exp>
        RULE_UNARYEXP_LPAREN_RPAREN                = 51, // <Unary Exp> ::= '(' <Expression> ')' <Object Exp>
        RULE_UNARYEXP                              = 52, // <Unary Exp> ::= <Object Exp>
        RULE_OBJECTEXP                             = 53, // <Object Exp> ::= <Method Exp>
        RULE_METHODEXP                             = 54, // <Method Exp> ::= <Method Exp> <Method>
        RULE_METHODEXP2                            = 55, // <Method Exp> ::= <Primary Exp>
        RULE_PRIMARYEXP                            = 56, // <Primary Exp> ::= <Primary>
        RULE_PRIMARYEXP_LPAREN_RPAREN              = 57, // <Primary Exp> ::= '(' <Expression> ')'
        RULE_PRIMARY                               = 58, // <Primary> ::= <Valid ID>
        RULE_PRIMARY_LPAREN_RPAREN                 = 59, // <Primary> ::= <Valid ID> '(' <Arg List Opt> ')'
        RULE_PRIMARY2                              = 60, // <Primary> ::= <Literal>
        RULE_ARGLISTOPT                            = 61, // <Arg List Opt> ::= <Arg List>
        RULE_ARGLISTOPT2                           = 62, // <Arg List Opt> ::= 
        RULE_ARGLIST_COMMA                         = 63, // <Arg List> ::= <Arg List> ',' <Expression>
        RULE_ARGLIST                               = 64, // <Arg List> ::= <Expression>
        RULE_STMLIST                               = 65, // <Stm List> ::= <Stm List> <Statement>
        RULE_STMLIST2                              = 66, // <Stm List> ::= <Statement>
        RULE_STATEMENT_IDENTIFIER_COLON            = 67, // <Statement> ::= Identifier ':'
        RULE_STATEMENT_SEMI                        = 68, // <Statement> ::= <Local Var Decl> ';'
        RULE_STATEMENT_IF_LPAREN_RPAREN            = 69, // <Statement> ::= if '(' <Expression> ')' <Statement>
        RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE       = 70, // <Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
        RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN = 71, // <Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
        RULE_STATEMENT                             = 72, // <Statement> ::= <Normal Stm>
        RULE_THENSTM_IF_LPAREN_RPAREN_ELSE         = 73, // <Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
        RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN   = 74, // <Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
        RULE_THENSTM                               = 75, // <Then Stm> ::= <Normal Stm>
        RULE_NORMALSTM_SEMI                        = 76, // <Normal Stm> ::= <Statement Exp> ';'
        RULE_NORMALSTM_SEMI2                       = 77, // <Normal Stm> ::= ';'
        RULE_NORMALSTM                             = 78, // <Normal Stm> ::= <Block>
        RULE_BLOCK_LBRACE_RBRACE                   = 79, // <Block> ::= '{' <Stm List> '}'
        RULE_BLOCK_LBRACE_RBRACE2                  = 80, // <Block> ::= '{' '}'
        RULE_VARIABLEDECS                          = 81, // <Variable Decs> ::= <Variable Declarator>
        RULE_VARIABLEDECS_COMMA                    = 82, // <Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
        RULE_VARIABLEDECLARATOR_IDENTIFIER         = 83, // <Variable Declarator> ::= Identifier
        RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ      = 84, // <Variable Declarator> ::= Identifier '=' <Expression>
        RULE_FORINITOPT                            = 85, // <For Init Opt> ::= <Local Var Decl>
        RULE_FORINITOPT2                           = 86, // <For Init Opt> ::= <Statement Exp List>
        RULE_FORINITOPT3                           = 87, // <For Init Opt> ::= 
        RULE_FORITERATOROPT                        = 88, // <For Iterator Opt> ::= <Statement Exp List>
        RULE_FORITERATOROPT2                       = 89, // <For Iterator Opt> ::= 
        RULE_FORCONDITIONOPT                       = 90, // <For Condition Opt> ::= <Expression>
        RULE_FORCONDITIONOPT2                      = 91, // <For Condition Opt> ::= 
        RULE_STATEMENTEXPLIST_COMMA                = 92, // <Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
        RULE_STATEMENTEXPLIST                      = 93, // <Statement Exp List> ::= <Statement Exp>
        RULE_LOCALVARDECL                          = 94, // <Local Var Decl> ::= <Qualified ID> <Rank Specifiers> <Pointer Opt> <Variable Decs>
        RULE_LOCALVARDECL2                         = 95, // <Local Var Decl> ::= <Qualified ID> <Pointer Opt> <Variable Decs>
        RULE_STATEMENTEXP_LPAREN_RPAREN            = 96, // <Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')'
        RULE_STATEMENTEXP_LPAREN_RPAREN2           = 97, // <Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_LBRACKET_RBRACKET        = 98, // <Statement Exp> ::= <Qualified ID> '[' <Expression List> ']' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_PLUSPLUS                 = 99, // <Statement Exp> ::= <Qualified ID> '++' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_MINUSMINUS               = 100, // <Statement Exp> ::= <Qualified ID> '--' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP                          = 101, // <Statement Exp> ::= <Qualified ID> <Assign Tail>
        RULE_ASSIGNTAIL_PLUSPLUS                   = 102, // <Assign Tail> ::= '++'
        RULE_ASSIGNTAIL_MINUSMINUS                 = 103, // <Assign Tail> ::= '--'
        RULE_ASSIGNTAIL_EQ                         = 104, // <Assign Tail> ::= '=' <Expression>
        RULE_METHODSOPT                            = 105, // <Methods Opt> ::= <Methods Opt> <Method>
        RULE_METHODSOPT2                           = 106, // <Methods Opt> ::= 
        RULE_METHOD_MEMBERNAME                     = 107, // <Method> ::= MemberName
        RULE_METHOD_MEMBERNAME_LPAREN_RPAREN       = 108, // <Method> ::= MemberName '(' <Arg List Opt> ')'
        RULE_METHOD_LBRACKET_RBRACKET              = 109, // <Method> ::= '[' <Expression List> ']'
        RULE_METHOD_PLUSPLUS                       = 110, // <Method> ::= '++'
        RULE_METHOD_MINUSMINUS                     = 111, // <Method> ::= '--'
        RULE_COMPILATIONUNIT_START                 = 112  // <Compilation Unit> ::= START <Block>
    };

    public class MyParser
    {
        private LALRParser parser;

        MainViewModel viewmodel;
        public MyParser(string filename,MainViewModel viewmodel)
        {
            this.viewmodel = viewmodel;
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public Token Parse(string source)
        {
            Token token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                Console.WriteLine("");
                //todo: Use your object any way you like
            }
            return token;
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT :
                //Comment
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE :
                //NewLine
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESDIV :
                //'*/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVTIMES :
                //'/*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVDIV :
                //'//'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAM :
                //'!'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMPAMP :
                //'&&'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PIPEPIPE :
                //'||'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOL :
                //bool
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CHARLITERAL :
                //CharLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DECLITERAL :
                //DecLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FALSE :
                //false
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_HEXLITERAL :
                //HexLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MEMBERNAME :
                //MemberName
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NULL :
                //null
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_REALLITERAL :
                //RealLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //START
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<Add Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ANDEXP :
                //<And Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGLIST :
                //<Arg List>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGLISTOPT :
                //<Arg List Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNTAIL :
                //<Assign Tail>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BASETYPE :
                //<Base Type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCK :
                //<Block>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCKORSEMI :
                //<Block or Semi>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPAREEXP :
                //<Compare Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMPILATIONUNIT :
                //<Compilation Unit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIMSEPARATORS :
                //<Dim Separators>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQUALITYEXP :
                //<Equality Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONLIST :
                //<Expression List>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORCONDITIONOPT :
                //<For Condition Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORINITOPT :
                //<For Init Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORITERATOROPT :
                //<For Iterator Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LITERAL :
                //<Literal>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOCALVARDECL :
                //<Local Var Decl>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD :
                //<Method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODEXP :
                //<Method Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODSOPT :
                //<Methods Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP :
                //<Mult Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NORMALSTM :
                //<Normal Stm>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OBJECTEXP :
                //<Object Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OREXP :
                //<Or Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OTHERTYPE :
                //<Other Type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_POINTEROPT :
                //<Pointer Opt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARY :
                //<Primary>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARYEXP :
                //<Primary Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_QUALIFIEDID :
                //<Qualified ID>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANKSPECIFIER :
                //<Rank Specifier>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANKSPECIFIERS :
                //<Rank Specifiers>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXP :
                //<Statement Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXPLIST :
                //<Statement Exp List>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMLIST :
                //<Stm List>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_THENSTM :
                //<Then Stm>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_UNARYEXP :
                //<Unary Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALIDID :
                //<Valid ID>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATOR :
                //<Variable Declarator>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECS :
                //<Variable Decs>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_BLOCKORSEMI :
                //<Block or Semi> ::= <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCKORSEMI_SEMI :
                //<Block or Semi> ::= ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALIDID_IDENTIFIER :
                //<Valid ID> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALIDID :
                //<Valid ID> ::= <Base Type>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_QUALIFIEDID :
                //<Qualified ID> ::= <Valid ID>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_TRUE :
                //<Literal> ::= true
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_FALSE :
                //<Literal> ::= false
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_DECLITERAL :
                //<Literal> ::= DecLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_HEXLITERAL :
                //<Literal> ::= HexLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_REALLITERAL :
                //<Literal> ::= RealLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_CHARLITERAL :
                //<Literal> ::= CharLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_STRINGLITERAL :
                //<Literal> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LITERAL_NULL :
                //<Literal> ::= null
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_POINTEROPT_TIMES :
                //<Pointer Opt> ::= '*'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_POINTEROPT :
                //<Pointer Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BASETYPE :
                //<Base Type> ::= <Other Type>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BASETYPE_INT :
                //<Base Type> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OTHERTYPE_DOUBLE :
                //<Other Type> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OTHERTYPE_BOOL :
                //<Other Type> ::= bool
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OTHERTYPE_STRING :
                //<Other Type> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RANKSPECIFIERS :
                //<Rank Specifiers> ::= <Rank Specifiers> <Rank Specifier>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RANKSPECIFIERS2 :
                //<Rank Specifiers> ::= <Rank Specifier>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_RANKSPECIFIER_LBRACKET_RBRACKET :
                //<Rank Specifier> ::= '[' <Dim Separators> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIMSEPARATORS_COMMA :
                //<Dim Separators> ::= <Dim Separators> ','
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIMSEPARATORS :
                //<Dim Separators> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST :
                //<Expression List> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST_COMMA :
                //<Expression List> ::= <Expression> ',' <Expression List>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_EQ :
                //<Expression> ::= <Or Exp> '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <Or Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OREXP_PIPEPIPE :
                //<Or Exp> ::= <Or Exp> '||' <And Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OREXP :
                //<Or Exp> ::= <And Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ANDEXP_AMPAMP :
                //<And Exp> ::= <And Exp> '&&' <Equality Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ANDEXP :
                //<And Exp> ::= <Equality Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXP_EQEQ :
                //<Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXP_EXCLAMEQ :
                //<Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXP :
                //<Equality Exp> ::= <Compare Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_LT :
                //<Compare Exp> ::= <Compare Exp> '<' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_GT :
                //<Compare Exp> ::= <Compare Exp> '>' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_LTEQ :
                //<Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_GTEQ :
                //<Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP :
                //<Compare Exp> ::= <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<Add Exp> ::= <Add Exp> '+' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<Add Exp> ::= <Add Exp> '-' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<Add Exp> ::= <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_TIMES :
                //<Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV :
                //<Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP :
                //<Mult Exp> ::= <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_EXCLAM :
                //<Unary Exp> ::= '!' <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_MINUS :
                //<Unary Exp> ::= '-' <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_PLUSPLUS :
                //<Unary Exp> ::= '++' <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_MINUSMINUS :
                //<Unary Exp> ::= '--' <Unary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_LPAREN_RPAREN :
                //<Unary Exp> ::= '(' <Expression> ')' <Object Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_UNARYEXP :
                //<Unary Exp> ::= <Object Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OBJECTEXP :
                //<Object Exp> ::= <Method Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODEXP :
                //<Method Exp> ::= <Method Exp> <Method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODEXP2 :
                //<Method Exp> ::= <Primary Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXP :
                //<Primary Exp> ::= <Primary>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXP_LPAREN_RPAREN :
                //<Primary Exp> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARY :
                //<Primary> ::= <Valid ID>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARY_LPAREN_RPAREN :
                //<Primary> ::= <Valid ID> '(' <Arg List Opt> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PRIMARY2 :
                //<Primary> ::= <Literal>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGLISTOPT :
                //<Arg List Opt> ::= <Arg List>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGLISTOPT2 :
                //<Arg List Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGLIST_COMMA :
                //<Arg List> ::= <Arg List> ',' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGLIST :
                //<Arg List> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMLIST :
                //<Stm List> ::= <Stm List> <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMLIST2 :
                //<Stm List> ::= <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_IDENTIFIER_COLON :
                //<Statement> ::= Identifier ':'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_SEMI :
                //<Statement> ::= <Local Var Decl> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN :
                //<Statement> ::= if '(' <Expression> ')' <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE :
                //<Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN :
                //<Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <Normal Stm>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_THENSTM_IF_LPAREN_RPAREN_ELSE :
                //<Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN :
                //<Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_THENSTM :
                //<Then Stm> ::= <Normal Stm>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NORMALSTM_SEMI :
                //<Normal Stm> ::= <Statement Exp> ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NORMALSTM_SEMI2 :
                //<Normal Stm> ::= ';'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NORMALSTM :
                //<Normal Stm> ::= <Block>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE :
                //<Block> ::= '{' <Stm List> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE2 :
                //<Block> ::= '{' '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECS :
                //<Variable Decs> ::= <Variable Declarator>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECS_COMMA :
                //<Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER :
                //<Variable Declarator> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ :
                //<Variable Declarator> ::= Identifier '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINITOPT :
                //<For Init Opt> ::= <Local Var Decl>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINITOPT2 :
                //<For Init Opt> ::= <Statement Exp List>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORINITOPT3 :
                //<For Init Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORITERATOROPT :
                //<For Iterator Opt> ::= <Statement Exp List>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORITERATOROPT2 :
                //<For Iterator Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT :
                //<For Condition Opt> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT2 :
                //<For Condition Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST_COMMA :
                //<Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST :
                //<Statement Exp List> ::= <Statement Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOCALVARDECL :
                //<Local Var Decl> ::= <Qualified ID> <Rank Specifiers> <Pointer Opt> <Variable Decs>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOCALVARDECL2 :
                //<Local Var Decl> ::= <Qualified ID> <Pointer Opt> <Variable Decs>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN :
                //<Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN2 :
                //<Statement Exp> ::= <Qualified ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LBRACKET_RBRACKET :
                //<Statement Exp> ::= <Qualified ID> '[' <Expression List> ']' <Methods Opt> <Assign Tail>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_PLUSPLUS :
                //<Statement Exp> ::= <Qualified ID> '++' <Methods Opt> <Assign Tail>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_MINUSMINUS :
                //<Statement Exp> ::= <Qualified ID> '--' <Methods Opt> <Assign Tail>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP :
                //<Statement Exp> ::= <Qualified ID> <Assign Tail>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_PLUSPLUS :
                //<Assign Tail> ::= '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_MINUSMINUS :
                //<Assign Tail> ::= '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_EQ :
                //<Assign Tail> ::= '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODSOPT :
                //<Methods Opt> ::= <Methods Opt> <Method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODSOPT2 :
                //<Methods Opt> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME :
                //<Method> ::= MemberName
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME_LPAREN_RPAREN :
                //<Method> ::= MemberName '(' <Arg List Opt> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_LBRACKET_RBRACKET :
                //<Method> ::= '[' <Expression List> ']'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_PLUSPLUS :
                //<Method> ::= '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_MINUSMINUS :
                //<Method> ::= '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMPILATIONUNIT_START :
                //<Compilation Unit> ::= START <Block>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
            viewmodel.State = message;
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'"+ " in line: " + (args.UnexpectedToken.Location.LineNr+1);
            //todo: Report message to UI?
            viewmodel.State = message;
        }

    }
}
