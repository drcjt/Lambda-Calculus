using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LambdaEngine;

using NUnit.Framework;

namespace LambdaEngineTests
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void LexNumber()
        {
            var lexer = new Lexer("123");
            Assert.AreEqual(Symbol.Number, lexer.CurrentToken.Symbol);
            Assert.AreEqual(123, lexer.CurrentToken.Value);
        }

        [Test]
        public void LexIdentifier()
        {
            var lexer = new Lexer("abc");
            Assert.AreEqual(Symbol.Identifier, lexer.CurrentToken.Symbol);
            Assert.AreEqual("abc", lexer.CurrentToken.Name);
        }

        [Test]
        public void LexNumberWithWhiteSpace()
        {
            var lexer = new Lexer(" \t\n 123");
            Assert.AreEqual(Symbol.Number, lexer.CurrentToken.Symbol);
            Assert.AreEqual(123, lexer.CurrentToken.Value);
        }

        [Test]
        public void LexNumberWithCommentInWhiteSpace()
        {
            var lexer = new Lexer(" \t { This is a comment } \n 123");
            Assert.AreEqual(Symbol.Number, lexer.CurrentToken.Symbol);
            Assert.AreEqual(123, lexer.CurrentToken.Value);
        }

        [Test]
        public void LexEOF()
        {
            var lexer = new Lexer("");
            Assert.AreEqual(Symbol.EOF, lexer.CurrentToken.Symbol);
        }

        [TestCase("Let", Symbol.Let)]
        [TestCase(";", Symbol.Semi)]
        [TestCase("(", Symbol.LParen)]
        [TestCase(")", Symbol.RParen)]
        [TestCase("\\", Symbol.Lambda)]
        [TestCase(".", Symbol.Period)]
        [TestCase("=", Symbol.Equals)]
        [TestCase("+", Symbol.Plus)]
        [TestCase("-", Symbol.Minus)]
        public void LexSymbol(string symbol, Symbol expectedSymbol)
        {
            var lexer = new Lexer(symbol);
            Assert.AreEqual(expectedSymbol, lexer.CurrentToken.Symbol);
        }

        [Test]
        public void LexPhrase()
        {
            var lexer = new Lexer("\\x.x");
            Assert.AreEqual(Symbol.Lambda, lexer.CurrentToken.Symbol);
            lexer.NextToken();
            Assert.AreEqual(Symbol.Identifier, lexer.CurrentToken.Symbol);
            Assert.AreEqual("x", lexer.CurrentToken.Name);
            lexer.NextToken();
            Assert.AreEqual(Symbol.Period, lexer.CurrentToken.Symbol);
            lexer.NextToken();
            Assert.AreEqual(Symbol.Identifier, lexer.CurrentToken.Symbol);
            Assert.AreEqual("x", lexer.CurrentToken.Name);
            lexer.NextToken();
            Assert.AreEqual(Symbol.EOF, lexer.CurrentToken.Symbol);
        }
    }
}
