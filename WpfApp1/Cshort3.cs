﻿
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
        SYMBOL_ID                 = 68, // <ID>
        SYMBOL_LITERAL            = 69, // <Literal>
        SYMBOL_LOCALVARDECL       = 70, // <Local Var Decl>
        SYMBOL_METHOD             = 71, // <Method>
        SYMBOL_METHODEXP          = 72, // <Method Exp>
        SYMBOL_METHODSOPT         = 73, // <Methods Opt>
        SYMBOL_MULTEXP            = 74, // <Mult Exp>
        SYMBOL_NORMALSTM          = 75, // <Normal Stm>
        SYMBOL_OBJECTEXP          = 76, // <Object Exp>
        SYMBOL_OREXP              = 77, // <Or Exp>
        SYMBOL_OTHERTYPE          = 78, // <Other Type>
        SYMBOL_PRIMARY            = 79, // <Primary>
        SYMBOL_PRIMARYEXP         = 80, // <Primary Exp>
        SYMBOL_RANKSPECIFIER      = 81, // <Rank Specifier>
        SYMBOL_RANKSPECIFIERS     = 82, // <Rank Specifiers>
        SYMBOL_STATEMENT          = 83, // <Statement>
        SYMBOL_STATEMENTEXP       = 84, // <Statement Exp>
        SYMBOL_STATEMENTEXPLIST   = 85, // <Statement Exp List>
        SYMBOL_STMLIST            = 86, // <Stm List>
        SYMBOL_THENSTM            = 87, // <Then Stm>
        SYMBOL_UNARYEXP           = 88, // <Unary Exp>
        SYMBOL_VARIABLEDECLARATOR = 89, // <Variable Declarator>
        SYMBOL_VARIABLEDECS       = 90  // <Variable Decs>
    };

    enum RuleConstants : int
    {
        RULE_BLOCKORSEMI                           =  0, // <Block or Semi> ::= <Block>
        RULE_BLOCKORSEMI_SEMI                      =  1, // <Block or Semi> ::= ';'
        RULE_ID_IDENTIFIER                         =  2, // <ID> ::= Identifier
        RULE_ID                                    =  3, // <ID> ::= <Base Type>
        RULE_LITERAL_TRUE                          =  4, // <Literal> ::= true
        RULE_LITERAL_FALSE                         =  5, // <Literal> ::= false
        RULE_LITERAL_DECLITERAL                    =  6, // <Literal> ::= DecLiteral
        RULE_LITERAL_HEXLITERAL                    =  7, // <Literal> ::= HexLiteral
        RULE_LITERAL_REALLITERAL                   =  8, // <Literal> ::= RealLiteral
        RULE_LITERAL_CHARLITERAL                   =  9, // <Literal> ::= CharLiteral
        RULE_LITERAL_STRINGLITERAL                 = 10, // <Literal> ::= StringLiteral
        RULE_LITERAL_NULL                          = 11, // <Literal> ::= null
        RULE_BASETYPE                              = 12, // <Base Type> ::= <Other Type>
        RULE_BASETYPE_INT                          = 13, // <Base Type> ::= int
        RULE_OTHERTYPE_DOUBLE                      = 14, // <Other Type> ::= double
        RULE_OTHERTYPE_BOOL                        = 15, // <Other Type> ::= bool
        RULE_OTHERTYPE_STRING                      = 16, // <Other Type> ::= string
        RULE_RANKSPECIFIERS                        = 17, // <Rank Specifiers> ::= <Rank Specifiers> <Rank Specifier>
        RULE_RANKSPECIFIERS2                       = 18, // <Rank Specifiers> ::= <Rank Specifier>
        RULE_RANKSPECIFIER_LBRACKET_RBRACKET       = 19, // <Rank Specifier> ::= '[' <Dim Separators> ']'
        RULE_DIMSEPARATORS_COMMA                   = 20, // <Dim Separators> ::= <Dim Separators> ','
        RULE_DIMSEPARATORS                         = 21, // <Dim Separators> ::= 
        RULE_EXPRESSIONLIST                        = 22, // <Expression List> ::= <Expression>
        RULE_EXPRESSIONLIST_COMMA                  = 23, // <Expression List> ::= <Expression> ',' <Expression List>
        RULE_EXPRESSION_EQ                         = 24, // <Expression> ::= <Or Exp> '=' <Expression>
        RULE_EXPRESSION                            = 25, // <Expression> ::= <Or Exp>
        RULE_OREXP_PIPEPIPE                        = 26, // <Or Exp> ::= <Or Exp> '||' <And Exp>
        RULE_OREXP                                 = 27, // <Or Exp> ::= <And Exp>
        RULE_ANDEXP_AMPAMP                         = 28, // <And Exp> ::= <And Exp> '&&' <Equality Exp>
        RULE_ANDEXP                                = 29, // <And Exp> ::= <Equality Exp>
        RULE_EQUALITYEXP_EQEQ                      = 30, // <Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
        RULE_EQUALITYEXP_EXCLAMEQ                  = 31, // <Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
        RULE_EQUALITYEXP                           = 32, // <Equality Exp> ::= <Compare Exp>
        RULE_COMPAREEXP_LT                         = 33, // <Compare Exp> ::= <Compare Exp> '<' <Add Exp>
        RULE_COMPAREEXP_GT                         = 34, // <Compare Exp> ::= <Compare Exp> '>' <Add Exp>
        RULE_COMPAREEXP_LTEQ                       = 35, // <Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
        RULE_COMPAREEXP_GTEQ                       = 36, // <Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
        RULE_COMPAREEXP                            = 37, // <Compare Exp> ::= <Add Exp>
        RULE_ADDEXP_PLUS                           = 38, // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
        RULE_ADDEXP_MINUS                          = 39, // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
        RULE_ADDEXP                                = 40, // <Add Exp> ::= <Mult Exp>
        RULE_MULTEXP_TIMES                         = 41, // <Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
        RULE_MULTEXP_DIV                           = 42, // <Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
        RULE_MULTEXP                               = 43, // <Mult Exp> ::= <Unary Exp>
        RULE_UNARYEXP_EXCLAM                       = 44, // <Unary Exp> ::= '!' <Unary Exp>
        RULE_UNARYEXP_MINUS                        = 45, // <Unary Exp> ::= '-' <Unary Exp>
        RULE_UNARYEXP_PLUSPLUS                     = 46, // <Unary Exp> ::= '++' <Unary Exp>
        RULE_UNARYEXP_MINUSMINUS                   = 47, // <Unary Exp> ::= '--' <Unary Exp>
        RULE_UNARYEXP_LPAREN_RPAREN                = 48, // <Unary Exp> ::= '(' <Expression> ')' <Object Exp>
        RULE_UNARYEXP                              = 49, // <Unary Exp> ::= <Object Exp>
        RULE_OBJECTEXP                             = 50, // <Object Exp> ::= <Method Exp>
        RULE_METHODEXP                             = 51, // <Method Exp> ::= <Method Exp> <Method>
        RULE_METHODEXP2                            = 52, // <Method Exp> ::= <Primary Exp>
        RULE_PRIMARYEXP                            = 53, // <Primary Exp> ::= <Primary>
        RULE_PRIMARYEXP_LPAREN_RPAREN              = 54, // <Primary Exp> ::= '(' <Expression> ')'
        RULE_PRIMARY                               = 55, // <Primary> ::= <ID>
        RULE_PRIMARY_LPAREN_RPAREN                 = 56, // <Primary> ::= <ID> '(' <Arg List Opt> ')'
        RULE_PRIMARY2                              = 57, // <Primary> ::= <Literal>
        RULE_ARGLISTOPT                            = 58, // <Arg List Opt> ::= <Arg List>
        RULE_ARGLISTOPT2                           = 59, // <Arg List Opt> ::= 
        RULE_ARGLIST_COMMA                         = 60, // <Arg List> ::= <Arg List> ',' <Expression>
        RULE_ARGLIST                               = 61, // <Arg List> ::= <Expression>
        RULE_STMLIST                               = 62, // <Stm List> ::= <Stm List> <Statement>
        RULE_STMLIST2                              = 63, // <Stm List> ::= <Statement>
        RULE_STATEMENT_IDENTIFIER_COLON            = 64, // <Statement> ::= Identifier ':'
        RULE_STATEMENT_SEMI                        = 65, // <Statement> ::= <Local Var Decl> ';'
        RULE_STATEMENT_IF_LPAREN_RPAREN            = 66, // <Statement> ::= if '(' <Expression> ')' <Statement>
        RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE       = 67, // <Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
        RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN = 68, // <Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
        RULE_STATEMENT                             = 69, // <Statement> ::= <Normal Stm>
        RULE_THENSTM_IF_LPAREN_RPAREN_ELSE         = 70, // <Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
        RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN   = 71, // <Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
        RULE_THENSTM                               = 72, // <Then Stm> ::= <Normal Stm>
        RULE_NORMALSTM_SEMI                        = 73, // <Normal Stm> ::= <Statement Exp> ';'
        RULE_NORMALSTM_SEMI2                       = 74, // <Normal Stm> ::= ';'
        RULE_NORMALSTM                             = 75, // <Normal Stm> ::= <Block>
        RULE_BLOCK_LBRACE_RBRACE                   = 76, // <Block> ::= '{' <Stm List> '}'
        RULE_BLOCK_LBRACE_RBRACE2                  = 77, // <Block> ::= '{' '}'
        RULE_VARIABLEDECS                          = 78, // <Variable Decs> ::= <Variable Declarator>
        RULE_VARIABLEDECS_COMMA                    = 79, // <Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
        RULE_VARIABLEDECLARATOR_IDENTIFIER         = 80, // <Variable Declarator> ::= Identifier
        RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ      = 81, // <Variable Declarator> ::= Identifier '=' <Expression>
        RULE_FORINITOPT                            = 82, // <For Init Opt> ::= <Local Var Decl>
        RULE_FORINITOPT2                           = 83, // <For Init Opt> ::= <Statement Exp List>
        RULE_FORINITOPT3                           = 84, // <For Init Opt> ::= 
        RULE_FORITERATOROPT                        = 85, // <For Iterator Opt> ::= <Statement Exp List>
        RULE_FORITERATOROPT2                       = 86, // <For Iterator Opt> ::= 
        RULE_FORCONDITIONOPT                       = 87, // <For Condition Opt> ::= <Expression>
        RULE_FORCONDITIONOPT2                      = 88, // <For Condition Opt> ::= 
        RULE_STATEMENTEXPLIST_COMMA                = 89, // <Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
        RULE_STATEMENTEXPLIST                      = 90, // <Statement Exp List> ::= <Statement Exp>
        RULE_LOCALVARDECL                          = 91, // <Local Var Decl> ::= <ID> <Rank Specifiers> <Variable Decs>
        RULE_LOCALVARDECL2                         = 92, // <Local Var Decl> ::= <ID> <Variable Decs>
        RULE_STATEMENTEXP_LPAREN_RPAREN            = 93, // <Statement Exp> ::= <ID> '(' <Arg List Opt> ')'
        RULE_STATEMENTEXP_LPAREN_RPAREN2           = 94, // <Statement Exp> ::= <ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_LBRACKET_RBRACKET        = 95, // <Statement Exp> ::= <ID> '[' <Expression List> ']' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_PLUSPLUS                 = 96, // <Statement Exp> ::= <ID> '++' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP_MINUSMINUS               = 97, // <Statement Exp> ::= <ID> '--' <Methods Opt> <Assign Tail>
        RULE_STATEMENTEXP                          = 98, // <Statement Exp> ::= <ID> <Assign Tail>
        RULE_ASSIGNTAIL_PLUSPLUS                   = 99, // <Assign Tail> ::= '++'
        RULE_ASSIGNTAIL_MINUSMINUS                 = 100, // <Assign Tail> ::= '--'
        RULE_ASSIGNTAIL_EQ                         = 101, // <Assign Tail> ::= '=' <Expression>
        RULE_METHODSOPT                            = 102, // <Methods Opt> ::= <Methods Opt> <Method>
        RULE_METHODSOPT2                           = 103, // <Methods Opt> ::= 
        RULE_METHOD_MEMBERNAME                     = 104, // <Method> ::= MemberName
        RULE_METHOD_MEMBERNAME_LPAREN_RPAREN       = 105, // <Method> ::= MemberName '(' <Arg List Opt> ')'
        RULE_METHOD_LBRACKET_RBRACKET              = 106, // <Method> ::= '[' <Expression List> ']'
        RULE_METHOD_PLUSPLUS                       = 107, // <Method> ::= '++'
        RULE_METHOD_MINUSMINUS                     = 108, // <Method> ::= '--'
        RULE_COMPILATIONUNIT_START                 = 109  // <Compilation Unit> ::= START <Block>
    };

    public class MyParser
    {
        private LALRParser parser;

        MainViewModel viewmodel;
        public MyParser(string filename, MainViewModel viewmodel)
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
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                 
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT :
                //Comment
                 
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE :
                //NewLine
                 
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                 
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESDIV :
                //'*/'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_DIVTIMES :
                //'/*'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_DIVDIV :
                //'//'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAM :
                //'!'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                 
                return null;

                case (int)SymbolConstants.SYMBOL_AMPAMP :
                //'&&'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                 
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                 
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                 
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_PIPEPIPE :
                //'||'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                 
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                 
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                 
                return null;

                case (int)SymbolConstants.SYMBOL_BOOL :
                //bool
                 
                return null;

                case (int)SymbolConstants.SYMBOL_CHARLITERAL :
                //CharLiteral
                 
                return null;

                case (int)SymbolConstants.SYMBOL_DECLITERAL :
                //DecLiteral
                 
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                 
                return null;

                case (int)SymbolConstants.SYMBOL_FALSE :
                //false
                 
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                 
                return null;

                case (int)SymbolConstants.SYMBOL_HEXLITERAL :
                //HexLiteral
                 
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                 
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                 
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                 
                return null;

                case (int)SymbolConstants.SYMBOL_MEMBERNAME :
                //MemberName
                 
                return null;

                case (int)SymbolConstants.SYMBOL_NULL :
                //null
                 
                return null;

                case (int)SymbolConstants.SYMBOL_REALLITERAL :
                //RealLiteral
                 
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //START
                 
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                 
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                 
                return null;

                case (int)SymbolConstants.SYMBOL_TRUE :
                //true
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<Add Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ANDEXP :
                //<And Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ARGLIST :
                //<Arg List>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ARGLISTOPT :
                //<Arg List Opt>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNTAIL :
                //<Assign Tail>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_BASETYPE :
                //<Base Type>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCK :
                //<Block>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_BLOCKORSEMI :
                //<Block or Semi>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_COMPAREEXP :
                //<Compare Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_COMPILATIONUNIT :
                //<Compilation Unit>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_DIMSEPARATORS :
                //<Dim Separators>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EQUALITYEXP :
                //<Equality Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSIONLIST :
                //<Expression List>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_FORCONDITIONOPT :
                //<For Condition Opt>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_FORINITOPT :
                //<For Init Opt>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_FORITERATOROPT :
                //<For Iterator Opt>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //<ID>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LITERAL :
                //<Literal>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_LOCALVARDECL :
                //<Local Var Decl>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD :
                //<Method>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_METHODEXP :
                //<Method Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_METHODSOPT :
                //<Methods Opt>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP :
                //<Mult Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_NORMALSTM :
                //<Normal Stm>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_OBJECTEXP :
                //<Object Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_OREXP :
                //<Or Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_OTHERTYPE :
                //<Other Type>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARY :
                //<Primary>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_PRIMARYEXP :
                //<Primary Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_RANKSPECIFIER :
                //<Rank Specifier>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_RANKSPECIFIERS :
                //<Rank Specifiers>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXP :
                //<Statement Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTEXPLIST :
                //<Statement Exp List>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_STMLIST :
                //<Stm List>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_THENSTM :
                //<Then Stm>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_UNARYEXP :
                //<Unary Exp>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECLARATOR :
                //<Variable Declarator>
                 
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLEDECS :
                //<Variable Decs>
                 
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
                 
                return null;

                case (int)RuleConstants.RULE_BLOCKORSEMI_SEMI :
                //<Block or Semi> ::= ';'
                 
                return null;

                case (int)RuleConstants.RULE_ID_IDENTIFIER :
                //<ID> ::= Identifier
                 
                return null;

                case (int)RuleConstants.RULE_ID :
                //<ID> ::= <Base Type>
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_TRUE :
                //<Literal> ::= true
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_FALSE :
                //<Literal> ::= false
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_DECLITERAL :
                //<Literal> ::= DecLiteral
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_HEXLITERAL :
                //<Literal> ::= HexLiteral
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_REALLITERAL :
                //<Literal> ::= RealLiteral
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_CHARLITERAL :
                //<Literal> ::= CharLiteral
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_STRINGLITERAL :
                //<Literal> ::= StringLiteral
                 
                return null;

                case (int)RuleConstants.RULE_LITERAL_NULL :
                //<Literal> ::= null
                 
                return null;

                case (int)RuleConstants.RULE_BASETYPE :
                //<Base Type> ::= <Other Type>
                 
                return null;

                case (int)RuleConstants.RULE_BASETYPE_INT :
                //<Base Type> ::= int
                 
                return null;

                case (int)RuleConstants.RULE_OTHERTYPE_DOUBLE :
                //<Other Type> ::= double
                 
                return null;

                case (int)RuleConstants.RULE_OTHERTYPE_BOOL :
                //<Other Type> ::= bool
                 
                return null;

                case (int)RuleConstants.RULE_OTHERTYPE_STRING :
                //<Other Type> ::= string
                 
                return null;

                case (int)RuleConstants.RULE_RANKSPECIFIERS :
                //<Rank Specifiers> ::= <Rank Specifiers> <Rank Specifier>
                 
                return null;

                case (int)RuleConstants.RULE_RANKSPECIFIERS2 :
                //<Rank Specifiers> ::= <Rank Specifier>
                 
                return null;

                case (int)RuleConstants.RULE_RANKSPECIFIER_LBRACKET_RBRACKET :
                //<Rank Specifier> ::= '[' <Dim Separators> ']'
                 
                return null;

                case (int)RuleConstants.RULE_DIMSEPARATORS_COMMA :
                //<Dim Separators> ::= <Dim Separators> ','
                 
                return null;

                case (int)RuleConstants.RULE_DIMSEPARATORS :
                //<Dim Separators> ::= 
                 
                return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST :
                //<Expression List> ::= <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_EXPRESSIONLIST_COMMA :
                //<Expression List> ::= <Expression> ',' <Expression List>
                 
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_EQ :
                //<Expression> ::= <Or Exp> '=' <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <Or Exp>
                 
                return null;

                case (int)RuleConstants.RULE_OREXP_PIPEPIPE :
                //<Or Exp> ::= <Or Exp> '||' <And Exp>
                 
                return null;

                case (int)RuleConstants.RULE_OREXP :
                //<Or Exp> ::= <And Exp>
                 
                return null;

                case (int)RuleConstants.RULE_ANDEXP_AMPAMP :
                //<And Exp> ::= <And Exp> '&&' <Equality Exp>
                 
                return null;

                case (int)RuleConstants.RULE_ANDEXP :
                //<And Exp> ::= <Equality Exp>
                 
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXP_EQEQ :
                //<Equality Exp> ::= <Equality Exp> '==' <Compare Exp>
                 
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXP_EXCLAMEQ :
                //<Equality Exp> ::= <Equality Exp> '!=' <Compare Exp>
                 
                return null;

                case (int)RuleConstants.RULE_EQUALITYEXP :
                //<Equality Exp> ::= <Compare Exp>
                 
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_LT :
                //<Compare Exp> ::= <Compare Exp> '<' <Add Exp>
                 
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_GT :
                //<Compare Exp> ::= <Compare Exp> '>' <Add Exp>
                 
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_LTEQ :
                //<Compare Exp> ::= <Compare Exp> '<=' <Add Exp>
                 
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP_GTEQ :
                //<Compare Exp> ::= <Compare Exp> '>=' <Add Exp>
                 
                return null;

                case (int)RuleConstants.RULE_COMPAREEXP :
                //<Compare Exp> ::= <Add Exp>
                 
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<Add Exp> ::= <Add Exp> '+' <Mult Exp>
                 
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<Add Exp> ::= <Add Exp> '-' <Mult Exp>
                 
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<Add Exp> ::= <Mult Exp>
                 
                return null;

                case (int)RuleConstants.RULE_MULTEXP_TIMES :
                //<Mult Exp> ::= <Mult Exp> '*' <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV :
                //<Mult Exp> ::= <Mult Exp> '/' <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_MULTEXP :
                //<Mult Exp> ::= <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_EXCLAM :
                //<Unary Exp> ::= '!' <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_MINUS :
                //<Unary Exp> ::= '-' <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_PLUSPLUS :
                //<Unary Exp> ::= '++' <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_MINUSMINUS :
                //<Unary Exp> ::= '--' <Unary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_UNARYEXP_LPAREN_RPAREN :
                //<Unary Exp> ::= '(' <Expression> ')' <Object Exp>
                 
                return null;

                case (int)RuleConstants.RULE_UNARYEXP :
                //<Unary Exp> ::= <Object Exp>
                 
                return null;

                case (int)RuleConstants.RULE_OBJECTEXP :
                //<Object Exp> ::= <Method Exp>
                 
                return null;

                case (int)RuleConstants.RULE_METHODEXP :
                //<Method Exp> ::= <Method Exp> <Method>
                 
                return null;

                case (int)RuleConstants.RULE_METHODEXP2 :
                //<Method Exp> ::= <Primary Exp>
                 
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXP :
                //<Primary Exp> ::= <Primary>
                 
                return null;

                case (int)RuleConstants.RULE_PRIMARYEXP_LPAREN_RPAREN :
                //<Primary Exp> ::= '(' <Expression> ')'
                 
                return null;

                case (int)RuleConstants.RULE_PRIMARY :
                //<Primary> ::= <ID>
                 
                return null;

                case (int)RuleConstants.RULE_PRIMARY_LPAREN_RPAREN :
                //<Primary> ::= <ID> '(' <Arg List Opt> ')'
                 
                return null;

                case (int)RuleConstants.RULE_PRIMARY2 :
                //<Primary> ::= <Literal>
                 
                return null;

                case (int)RuleConstants.RULE_ARGLISTOPT :
                //<Arg List Opt> ::= <Arg List>
                 
                return null;

                case (int)RuleConstants.RULE_ARGLISTOPT2 :
                //<Arg List Opt> ::= 
                 
                return null;

                case (int)RuleConstants.RULE_ARGLIST_COMMA :
                //<Arg List> ::= <Arg List> ',' <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_ARGLIST :
                //<Arg List> ::= <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_STMLIST :
                //<Stm List> ::= <Stm List> <Statement>
                 
                return null;

                case (int)RuleConstants.RULE_STMLIST2 :
                //<Stm List> ::= <Statement>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENT_IDENTIFIER_COLON :
                //<Statement> ::= Identifier ':'
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENT_SEMI :
                //<Statement> ::= <Local Var Decl> ';'
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN :
                //<Statement> ::= if '(' <Expression> ')' <Statement>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENT_IF_LPAREN_RPAREN_ELSE :
                //<Statement> ::= if '(' <Expression> ')' <Then Stm> else <Statement>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENT_FOR_LPAREN_SEMI_SEMI_RPAREN :
                //<Statement> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Statement>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <Normal Stm>
                 
                return null;

                case (int)RuleConstants.RULE_THENSTM_IF_LPAREN_RPAREN_ELSE :
                //<Then Stm> ::= if '(' <Expression> ')' <Then Stm> else <Then Stm>
                 
                return null;

                case (int)RuleConstants.RULE_THENSTM_FOR_LPAREN_SEMI_SEMI_RPAREN :
                //<Then Stm> ::= for '(' <For Init Opt> ';' <For Condition Opt> ';' <For Iterator Opt> ')' <Then Stm>
                 
                return null;

                case (int)RuleConstants.RULE_THENSTM :
                //<Then Stm> ::= <Normal Stm>
                 
                return null;

                case (int)RuleConstants.RULE_NORMALSTM_SEMI :
                //<Normal Stm> ::= <Statement Exp> ';'
                 
                return null;

                case (int)RuleConstants.RULE_NORMALSTM_SEMI2 :
                //<Normal Stm> ::= ';'
                 
                return null;

                case (int)RuleConstants.RULE_NORMALSTM :
                //<Normal Stm> ::= <Block>
                 
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE :
                //<Block> ::= '{' <Stm List> '}'
                 
                return null;

                case (int)RuleConstants.RULE_BLOCK_LBRACE_RBRACE2 :
                //<Block> ::= '{' '}'
                 
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECS :
                //<Variable Decs> ::= <Variable Declarator>
                 
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECS_COMMA :
                //<Variable Decs> ::= <Variable Decs> ',' <Variable Declarator>
                 
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER :
                //<Variable Declarator> ::= Identifier
                 
                return null;

                case (int)RuleConstants.RULE_VARIABLEDECLARATOR_IDENTIFIER_EQ :
                //<Variable Declarator> ::= Identifier '=' <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_FORINITOPT :
                //<For Init Opt> ::= <Local Var Decl>
                 
                return null;

                case (int)RuleConstants.RULE_FORINITOPT2 :
                //<For Init Opt> ::= <Statement Exp List>
                 
                return null;

                case (int)RuleConstants.RULE_FORINITOPT3 :
                //<For Init Opt> ::= 
                 
                return null;

                case (int)RuleConstants.RULE_FORITERATOROPT :
                //<For Iterator Opt> ::= <Statement Exp List>
                 
                return null;

                case (int)RuleConstants.RULE_FORITERATOROPT2 :
                //<For Iterator Opt> ::= 
                 
                return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT :
                //<For Condition Opt> ::= <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_FORCONDITIONOPT2 :
                //<For Condition Opt> ::= 
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST_COMMA :
                //<Statement Exp List> ::= <Statement Exp List> ',' <Statement Exp>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXPLIST :
                //<Statement Exp List> ::= <Statement Exp>
                 
                return null;

                case (int)RuleConstants.RULE_LOCALVARDECL :
                //<Local Var Decl> ::= <ID> <Rank Specifiers> <Variable Decs>
                 
                return null;

                case (int)RuleConstants.RULE_LOCALVARDECL2 :
                //<Local Var Decl> ::= <ID> <Variable Decs>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN :
                //<Statement Exp> ::= <ID> '(' <Arg List Opt> ')'
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LPAREN_RPAREN2 :
                //<Statement Exp> ::= <ID> '(' <Arg List Opt> ')' <Methods Opt> <Assign Tail>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_LBRACKET_RBRACKET :
                //<Statement Exp> ::= <ID> '[' <Expression List> ']' <Methods Opt> <Assign Tail>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_PLUSPLUS :
                //<Statement Exp> ::= <ID> '++' <Methods Opt> <Assign Tail>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP_MINUSMINUS :
                //<Statement Exp> ::= <ID> '--' <Methods Opt> <Assign Tail>
                 
                return null;

                case (int)RuleConstants.RULE_STATEMENTEXP :
                //<Statement Exp> ::= <ID> <Assign Tail>
                 
                return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_PLUSPLUS :
                //<Assign Tail> ::= '++'
                 
                return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_MINUSMINUS :
                //<Assign Tail> ::= '--'
                 
                return null;

                case (int)RuleConstants.RULE_ASSIGNTAIL_EQ :
                //<Assign Tail> ::= '=' <Expression>
                 
                return null;

                case (int)RuleConstants.RULE_METHODSOPT :
                //<Methods Opt> ::= <Methods Opt> <Method>
                 
                return null;

                case (int)RuleConstants.RULE_METHODSOPT2 :
                //<Methods Opt> ::= 
                 
                return null;

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME :
                //<Method> ::= MemberName
                 
                return null;

                case (int)RuleConstants.RULE_METHOD_MEMBERNAME_LPAREN_RPAREN :
                //<Method> ::= MemberName '(' <Arg List Opt> ')'
                 
                return null;

                case (int)RuleConstants.RULE_METHOD_LBRACKET_RBRACKET :
                //<Method> ::= '[' <Expression List> ']'
                 
                return null;

                case (int)RuleConstants.RULE_METHOD_PLUSPLUS :
                //<Method> ::= '++'
                 
                return null;

                case (int)RuleConstants.RULE_METHOD_MINUSMINUS :
                //<Method> ::= '--'
                 
                return null;

                case (int)RuleConstants.RULE_COMPILATIONUNIT_START :
                //<Compilation Unit> ::= START <Block>
                 
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '" + args.Token.ToString() + "'";
            //todo: Report message to UI?
            viewmodel.State = message;
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "'" + " in line: " + (args.UnexpectedToken.Location.LineNr + 1);
            //todo: Report message to UI?
            viewmodel.State = message;
        }

    }
}
