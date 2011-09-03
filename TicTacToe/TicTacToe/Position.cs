using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    /// <summary>
    /// Position.
    /// </summary>
    class Position
    {
        public Position(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public uint X
        {
            get;
            private set;
        }

        public uint Y
        {
            get;
            private set;
        }
    }
}
