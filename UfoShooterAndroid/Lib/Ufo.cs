using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;

namespace UfoShooterAndroid.Lib
{
    internal class Ufo:Sprite
    {
        public Ufo(Texture2D texture, Game1 _game) : base(_game)
        {
            Texture = texture;
            Random rnd = new Random();
            int t = rnd.Next(0, 2);
            int top = rnd.Next(0, game.GraphicsDevice.Viewport.Height/2);
            Speed = rnd.Next(5, 9);
            if (t == 0)
            {
                
                Position = new Vector2(-200, top);
            }
            else
            {
                Speed = -Speed;

                Position = new Vector2(game.GraphicsDevice.Viewport.Width + 200, top);
            }

        }
        public override void Update(GameTime gameTime)
        {
            Position.X += Speed;
            if ( Position.X > game.GraphicsDevice.Viewport.Width + 400 | Position.X < -400)
            {
                this.IsActive = false;
            }
            //base.Update(gameTime);
        }
    }
}
