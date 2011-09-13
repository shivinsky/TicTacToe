using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TicTacToe
{
    /// <summary>
    /// Cleaner.
    /// </summary>
    class Cleaner : GameComponent
    {
        private Board _board;
        private Queue<Vector2> _waypoints;
        private Queue<Vector2> _path;
        private Vector2 _pos;

        private float _speed;

        private SpriteFont _test;

        public Cleaner(Board board) 
            : base(board.Game)
        {
            _board = board;
            _waypoints = CalculatePath();
            _path = new Queue<Vector2>(_waypoints);

            _pos = _waypoints.Peek();
            _speed = 5;
        }

        /// <summary>
        /// Calculate path.
        /// </summary>
        public Queue<Vector2> CalculatePath()
        {
            var path = new Queue<Vector2>();

            var width = _board.Size.X;
            var height = _board.Size.Y; 
            for (var i = 0; i < width + height ; i += 2)
            {
                var from = new Vector2(Math.Max(i - height, 0), Math.Min(i, height));
                var to = new Vector2(Math.Min(i, width), Math.Max(0, i - width));

                var diagonal = new List<Vector2>();

                for (var j = 0; j < to.X - from.X; j++)
                {
                    diagonal.Add(from + new Vector2(j * 1, j * - 1));
                }

                foreach (var point in diagonal)
                {
                    path.Enqueue(point);
                }
            }

            return path;
        }

        /// <summary>
        /// Load content.
        /// </summary>
        public void LoadContent()
        {
            _test = Game.Content.Load<SpriteFont>("Fonts/main");
        }

        /// <summary>
        /// Update.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_path.Count > 0) 
            {
                if (Vector2.Distance(_pos, _path.Peek()) < 0.1f)
                {
                    _pos = _path.Dequeue();
                }
                else
                {
                    var direction = Vector2.Normalize(_path.Peek() - _pos);
                    _pos += direction * _speed * delta;
                }
            }
        }

        /// <summary>
        /// Draw.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            var from = _waypoints.Peek();

            // TODO
            foreach (var point in _waypoints)
            {
                spriteBatch.DrawLine(3, Color.Green, 
                    _board.Position + from * new Vector2(26, 26),
                    _board.Position + point * new Vector2(26, 26));
                from = point;
            }

            spriteBatch.DrawString(_test, "+", _board.Position + new Vector2(-10, - 13) + _pos * new Vector2(26, 26), Color.Red);
        }
    }
}
