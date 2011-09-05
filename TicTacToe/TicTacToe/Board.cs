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

        Boolean _playerCurrent;

        Dictionary<Position, Piece> _board = new Dictionary<Position, Piece>();

        MouseState _mouseCurrent;
        MouseState _mousePrevious;

        Texture2D _blank;
        SpriteFont _gameFont;
        Vector2 _position;
        Rectangle _rectangle;

        Vector2 _from;
        Vector2 _to;

        bool _win;
        int _winSequence;
        int _size;
        int _cellSize;

        public Board(ContentManager content, GraphicsDevice graphicsDevice, 
            SpriteBatch spriteBatch, Vector2 position, int size, int cellSize)
        {
            _content = content;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = spriteBatch;

            _playerCurrent = (new Random().Next(0, 2) == 1);
            _winSequence = 5;
            _size = size;
            _cellSize = cellSize;
            _position = position;
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y,
                _size * _cellSize, _size * _cellSize);

            LoadContent();
        }

        /// <summary>
        /// Returns all moves
        /// </summary>
        /// <returns></returns>
        public List<Move> AllMoves()
        {
            return new List<Move>();
        }

        /// <summary>
        /// Check winner.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public List<Position> CheckWinner(bool player, Position last)
        {
            var chain = new List<List<Position>>();

            for (int i = 0; i < 4; i++)
            {
                chain.Add(new List<Position>());
            }

            for (int i = 0; i < _size; i++)
            {
                // Directions
                var checks = new List<Position>
                {
                    new Position(last.x, i), new Position(i, last.y), 
                    new Position(i, i - last.x + last.y), new Position(i, last.x + last.y - i)                    
                };
                                  
                // Check vertical, horizontal, diagonal, anti-diagonal
                for (var j = 0; j < checks.Count; j++)
                {
                    Piece piece;
                    if (_board.TryGetValue(checks[j], out piece) && player == piece.Player)
                    {
                        chain[j].Add(checks[j]);
                    }
                    else
                    {
                        chain[j].Clear();
                    }
                }

                // Got winner?
                var path = chain.Find(x => x.Count >= _winSequence);
                if (path != null)
                {
                    return path;
                }
            }

            return null;
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
                var translate = new Vector2(_mouseCurrent.X - _position.X, 
                    _mouseCurrent.Y - _position.Y) / _cellSize;
                var position = new Position((int)translate.X, (int)translate.Y);
                if (!_board.ContainsKey(position))
                {
                    _board.Add(position, new Piece(_playerCurrent));
                    var path = CheckWinner(_playerCurrent, position);
                    if (path != null)
                    {
                        _win = true;
                        var center = new Vector2(_cellSize / 2, _cellSize / 2);
                        _from = new Vector2(path.First().x, path.First().y) * _cellSize + _position + center;
                        _to = new Vector2(path.Last().x, path.Last().y) * _cellSize + _position + center;
                    }
                    _playerCurrent = !_playerCurrent;
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
                    _cellSize / 2 - size.Y / 2);

                _spriteBatch.DrawString(_gameFont, player, _position + center +
                    new Vector2(_cellSize * pair.Key.x, _cellSize * pair.Key.y), color);
            }
            if (_win)
            {
                DrawLine(_spriteBatch, 2, Color.Red, _from, _to);
                _spriteBatch.DrawString(_gameFont, "Win!", new Vector2(250, 50), Color.CadetBlue);
            }
        }

        public bool CurrentPlayer
        {
            get;
            set;
        }

    }
}
