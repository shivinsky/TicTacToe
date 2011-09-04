using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe
{
    /// <summary>
    /// Piece.
    /// </summary>
    class Piece
    {
        public Piece(bool player)
        {
            Player = player;
        }

        public bool Player
        {
            get;
            private set;
       } 
   } 
}
