using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UfoShooterAndroid.Lib
{
    public abstract class Sprite
    {
        protected Game game;
        protected Texture2D Texture { get; set; }
        public SoundEffect Sound { get; set; }
        public Vector2 Position;
        public Vector2 Velocity;
        public virtual Color Color { get; set; } = Color.White;
        public int Speed { get; set; } = 5;
        public bool IsActive { get; set; } = true;

        public Sprite(Game _game) { game = _game; }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }

        public abstract void Update(GameTime gameTime);
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
    }
}
