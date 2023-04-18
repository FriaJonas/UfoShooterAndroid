using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoShooterAndroid.Lib
{
    internal class Explosion : Sprite
    {
        private int Timer = 30;
        public Explosion(Texture2D texture,Game1 game) : base(game)
        {
            Texture=texture;
        }
        public override void Update(GameTime gameTime)
        {
            if (Timer > 0)
                Timer--;
            else
                IsActive = false;

            //base.Update(gameTime);
        }
        public override Color Color
        {
            //Gör så att den försvinner sakta
            get { return new Color(Timer * 8, Timer * 8, Timer * 8, Timer * 8); }
        }
    }
}
