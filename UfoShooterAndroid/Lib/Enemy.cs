using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Webkit.WebStorage;

namespace UfoShooterAndroid.Lib
{
    internal class Enemy : Sprite
    {
        //Hur ofta bomberna ska falla

        Vector2 center;
        
        public Enemy(Texture2D texture,Game1 game) : base(game)
        {
            Texture=texture;
            Random rnd = new Random();

            Speed = rnd.Next(3, 10);
            center = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }
        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Texture,new Rectangle((int)Position.X,(int)Position.Y,Texture.Width,Texture.Height), null, Color.White,rotation, center, SpriteEffects.None, 0f);
        //}
        public override void Update(GameTime gameTime)
        {
           
            Position.Y += Speed;
            //Om bomberna faller förbi
            if (Position.Y > game.GraphicsDevice.Viewport.Height + 20)
            {
                game.lifeBar.Width -= 50;
                this.IsActive = false;
            }
        }
    }
}
