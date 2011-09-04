using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TicTacToe
{
    /// <summary>
    /// This is the main type for Tic Tac Toe
    /// </summary>
    public class TicTacToe : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        SpriteFont _gameFont;
        SpriteFont _menuFont;

        Texture2D _back;

        Board _board;

        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = 512;
            _graphics.PreferredBackBufferWidth = 512;

            _graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {     
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameFont = Content.Load<SpriteFont>("main");
            _menuFont = Content.Load<SpriteFont>("menu");
            _back = Content.Load<Texture2D>("back");

            _board = new Board(Content, GraphicsDevice, _spriteBatch, new Vector2(20, 100), 7);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _board.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_back, new Vector2(0, 0), Color.White);

            // TODO Fix this shit.
            _spriteBatch.DrawString(_gameFont, "Player 1", new Vector2(25, 25), Color.Black);
            _spriteBatch.DrawString(_gameFont, "Player 2", new Vector2(200, 25), Color.Black);
            _spriteBatch.DrawString(_gameFont, "0", new Vector2(75, 55), Color.Green);
            _spriteBatch.DrawString(_gameFont, "0", new Vector2(250, 55), Color.Green);

            _spriteBatch.DrawString(_menuFont, "New", new Vector2(100, 455), Color.Red, 25, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_menuFont, "Top", new Vector2(190, 445), Color.Red, 25, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_menuFont, "About", new Vector2(270, 430), Color.Red, 25, new Vector2(0, 0), 1, SpriteEffects.None, 0);

            _board.Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
