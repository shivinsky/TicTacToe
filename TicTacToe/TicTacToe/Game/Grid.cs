using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    /// <summary>
    /// Grid.
    /// </summary>
    class Grid : GameComponent
    {
        private Rectangle _rectangle;
        private Vector2 _size;
        private Vector2 _position;
        private int _cellSize;

        private MouseState _mouseCurrent;
        private MouseState _mousePrevious;

        private Board _board;

        public Grid(Vector2 position, Vector2 size, int cellSize, Board board) 
            : base(board.Game)
        {
            _position = position;
            _size = size;
            _cellSize = cellSize;
            _board = board;
        }

        /// <summary>
        /// Load content.
        /// </summary>
        public void LoadContent()
        {
        }

        /// <summary>
        /// Update.
        /// </summary>
        public override void Update(GameTime gameTime)
        {         
            _mouseCurrent = Mouse.GetState();
            if (_mouseCurrent.LeftButton == ButtonState.Pressed &&
                _mousePrevious.LeftButton == ButtonState.Released &&
                _rectangle.Contains(new Point(_mouseCurrent.X, _mouseCurrent.Y)))
            {
                // Convert coordinates
                var translate = new Vector2(_mouseCurrent.X - _position.X,
                    _mouseCurrent.Y - _position.Y) / _cellSize;

                var position = new Position((int)translate.X, (int)translate.Y);
                // _board.Turn(position);
                // if (!_board.ContainsKey(position))
                // {
                //    _board.Add(position, new Piece(_playerCurrent));
                //    var path = _board.CheckWinner(_playerCurrent, position);
                //    if (path != null)
                //    {
                //        var center = new Vector2(_cellSize / 2, _cellSize / 2);
                //        Vector2 dir = new Vector2(path[1].x - path[0].x, path[1].y - path[0].y);
                //        _from = new Vector2(path.First().x, path.First().y) * _cellSize + (dir * _cellSize / 2) + _position + center;
                //        _to = new Vector2(path.Last().x, path.Last().y) * _cellSize - (dir * _cellSize / 2) + _position + center;
                //    }
                // }
            }
            _mousePrevious = _mouseCurrent;
        }

        /// <summary>
        /// Draw grid.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw columns
            for (int i = 1; i < _size.X; i++)
            {
                spriteBatch.DrawLine(1, Color.Gray,
                    _position + new Vector2(i * _cellSize, 0),
                    _position + new Vector2(i * _cellSize, _cellSize * _size.Y));
            }

            // Draw rows
            for (int i = 1; i < _size.Y; i++)
            {
                 spriteBatch.DrawLine(1, Color.Gray,
                    _position + new Vector2(0, i * _cellSize),
                    _position + new Vector2(_cellSize * _size.X, i * _cellSize));
            }
        }
    }
}
