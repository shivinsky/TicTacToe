using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe
{
    /// <summary>
    /// Renderer.
    /// </summary>
    static public class Renderer
    {
        static Texture2D _blank;
        
        /// <summary>
        /// Draw line.
        /// </summary>
        static public void DrawLine(this SpriteBatch batch, float width, Color color, Vector2 from, Vector2 to)
        {
            // HACK
            if (_blank == null)
            {
                _blank = new Texture2D(batch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _blank.SetData(new[] { Color.White });
            }
            var angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            var scale = new Vector2(Vector2.Distance(from, to), width);
            batch.Draw(_blank, from, null, color, angle, Vector2.Zero, scale, 
                SpriteEffects.None, 0);
        }
    }
}
