using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.Transliterator;
using static Android.Widget.GridLayout;

namespace UfoShooterAndroid.Lib
{
    internal class Ship:Sprite
    {

        public Ship(Texture2D graphics, Game _game) : base(_game)
        {
            game = _game;
            Texture = graphics;
            Position = new Vector2(100, 100);
            Velocity = new Vector2(5, 5);
        }

        public override void Update(GameTime gameTime)
        {
            //denna funktionen Måste implementeras!!

            //lite random för att få bollarna att få olika fart och riktning
            Position += Velocity;
            if(Position.X<0) Position.X = 0;
            if(Position.X>game.Window.ClientBounds.Width-Texture.Width)
                Position.X = game.Window.ClientBounds.Width-Texture.Width;
            if(Position.Y<0) Position.Y = 0;
            if (Position.Y > game.Window.ClientBounds.Height-Texture.Height)
                Position.Y = game.Window.ClientBounds.Height-Texture.Height;
        }
    }
}
