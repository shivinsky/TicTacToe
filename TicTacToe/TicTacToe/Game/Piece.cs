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

        public float Alpha
        {
            get;
            set;
        }

        public Position Position
        {
            get;
            set;
        }

        public void Draw(SpriteBatch spriteBatch)
        {          

          //      Color color = pair.Value.Player ? Color.Blue : Color.Red;
          //      string player = pair.Value.Player ? "O" : "X";
          ///      Vector2 size = _gameFont.MeasureString(player);

          //      // Center align
          //      Vector2 center = new Vector2(_cellSize / 2 - size.X / 2,
         //           _cellSize / 2 - size.Y / 2);

         /////       spriteBatch.DrawString(_gameFont, player, _position + center +
           ///         new Vector2(_cellSize * pair.Key.x, _cellSize * pair.Key.y), color);
        }
   } 
}
