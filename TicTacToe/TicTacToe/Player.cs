using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class Player
    {
        Board _board;

        public Player(Board board)
        {
            _board = board;
        }

        public int Score
        {
            get;
            set;
        }

        public int Winner
        {
            get;
            set;
        }

        public void Draw()
        {

        }
    }
}
