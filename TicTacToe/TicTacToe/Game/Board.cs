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
    /// Board.
    /// </summary>
    class Board : GameComponent
    {
        private Grid _grid;
        private Cleaner _cleaner;
        private Dictionary<Position, Piece> _board;

        public Board(Vector2 position, Vector2 size, int cellSize, Game game) 
            : base(game)
        {
            Size = size;
            Position = position;

            _board = new Dictionary<Position, Piece>();

            _grid = new Grid(position, size, cellSize, this);

            _cleaner = new Cleaner(this);

            LoadContent();
        }

        public Vector2 Size
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public int WinSequence
        {
            get;
            set;
        }

        /// <summary>
        /// Returns all moves.
        /// </summary>
        public List<Move> GetAllMoves()
        {
            return new List<Move>();
        }

        /// <summary>
        /// Get piece.
        /// </summary>
        public Piece GetPiece(Position position)
        {
            Piece piece;
            _board.TryGetValue(position, out piece);
            return piece;
        }

        /// <summary>
        /// Add piece.
        /// </summary>
        public void AddPiece(Piece piece)
        {
            _board.Add(piece.Position, piece);   
        }

        /// <summary>
        /// Turn.
        /// </summary>
        public void Turn(Piece piece)
        {

        }

        /// <summary>
        /// Check a winner.
        /// </summary>
        public List<Position> CheckWinner(bool player, Position last)
        {
            var chain = Enumerable.Repeat(new List<Position>(), 4).ToList(); 

            for (var i = 0; i < Math.Max(Size.X, Size.Y); i++)
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
                var path = chain.Find(x => x.Count == WinSequence);
                if (path != null)
                {
                    return path;
                }
            }

            return null;
        }

        /// <summary>
        /// Load content
        /// </summary>
        public void LoadContent()
        {
            _cleaner.LoadContent();
            _grid.LoadContent();
        }

        /// <summary>
        /// Update.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            _cleaner.Update(gameTime);
            _grid.Update(gameTime);
        }

        /// <summary>
        /// Draw board.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            _grid.Draw(spriteBatch);

            foreach (var piece in _board)
            {
                piece.Value.Draw(spriteBatch);
            }

            _cleaner.Draw(spriteBatch);
        }
    }
}
