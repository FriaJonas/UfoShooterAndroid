using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoShooterAndroid.Lib
{
    public abstract class Button
    {
        public Game game;
        public Texture2D Texture { get; set; }
        public Rectangle Position { get; set; }
        public virtual Color Color { get; set; } = Color.White;
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }
    }

    public class StartButton:Button
    {
        public StartButton(Texture2D texture, Game game,Point pos ) {
            Position = new Rectangle(pos,new Point(texture.Width,texture.Height));
            Texture = texture;
        }
    }
}
