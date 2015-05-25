using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaEngine
{
    public enum Symbol
    {
        Identifier,
        Number,
        Lambda,
        Period,
        LParen,
        RParen,
        Semi,
        Equals,
        Let,
        Plus,
        Minus,
        EOF,
        Unknown,
    }
}
