using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    /// <summary>
    /// Position.
    /// </summary>
    struct Position
    {
        public readonly uint x;
        public readonly uint y;

        public Position(uint x, uint y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
