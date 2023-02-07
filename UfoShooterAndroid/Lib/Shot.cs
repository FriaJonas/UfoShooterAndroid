using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoShooterAndroid.Lib
{
    public class Shot : Sprite
    {

        public Shot(Texture2D texture ,Game game) : base(game)
        {
            Speed = -10;
            Texture= texture;

        }

        public override void Update(GameTime gameTime)
        {
            Position.Y += Speed;
            if (Position.Y < 0) this.IsActive = false;

        }

    }
}
