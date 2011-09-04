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
    /// Board.
    /// </summary>
    class Board
    {
        ContentManager _content;
        SpriteBatch _spriteBatch;
        GraphicsDevice _graphicsDevice;

        Dictionary<Position, Piece> _board = new Dictionary<Position, Piece>();

        MouseState _mouseCurrent;
        MouseState _mousePrevious;

        Texture2D _blank;
        SpriteFont _gameFont;
        Vector2 _position;
        Rectangle _rectangle;

        int _size;
        int _cellSize;

        public Board(ContentManager content, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Vector2 position, int size)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;

            _size = size;
            _cellSize = 26;
            _position = position;
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                _size * _cellSize, _size * _cellSize);

            LoadContent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadContent()
        {
            _gameFont = _content.Load<SpriteFont>("main");
            _blank = new Texture2D(_graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _blank.SetData(new[] { Color.White });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _mouseCurrent = Mouse.GetState();
            if (_mouseCurrent.LeftButton == ButtonState.Pressed &&
                _mousePrevious.LeftButton == ButtonState.Released &&
                _rectangle.Contains(new Point(_mouseCurrent.X, _mouseCurrent.Y)))
            {
                Vector2 translate = new Vector2(_mouseCurrent.X - _position.X, 
                    _mouseCurrent.Y - _position.Y) / _cellSize;
                Position position = new Position((int)translate.X, (int)translate.Y);
                if (!_board.ContainsKey(position))
                {
                    _board.Add(position, new Piece(new Random().Next(0, 2) == 1));
                }
            }
            _mousePrevious = _mouseCurrent;
        }

        /// <summary>
        /// Draw line.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="width"></param>
        /// <param name="color"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        void DrawLine(SpriteBatch spriteBatch, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            spriteBatch.Draw(_blank, point1, null, color,
                angle, Vector2.Zero, new Vector2(length, width),
                SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw grid.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gridSize"></param>
        /// <param name="cellSize"></param>
        void DrawGrid(Vector2 position, int gridSize, int cellSize)
        {
            for (int i = 1; i < gridSize; i++)
            {
                // Draw columns
                DrawLine(_spriteBatch, 1, Color.Gray,
                    position + new Vector2(i * cellSize, 0),
                    position + new Vector2(i * cellSize, cellSize * gridSize));

                // Draw rows
                DrawLine(_spriteBatch,  1, Color.Gray,
                    position + new Vector2(0, i * cellSize),
                    position + new Vector2(cellSize * gridSize, i * cellSize));
            }
        }

        /// <summary>
        /// Draw board.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            DrawGrid(_position, _size, _cellSize);

            // Draw pieces
            foreach (var pair in _board)
            {
                Color color = pair.Value.Player ? Color.Blue : Color.Red;
                string player = pair.Value.Player ? "O" : "X";
                Vector2 size = _gameFont.MeasureString(player);

                // Center align
                Vector2 center = new Vector2(_cellSize / 2 - size.X / 2, 
                    _cellSize / 2 - size.Y / 2 + 4);

                _spriteBatch.DrawString(_gameFont, player, _position + center +
                    new Vector2(_cellSize * pair.Key.x, _cellSize * pair.Key.y), color);
            }
        }

    }
}
