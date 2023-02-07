
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using UfoShooterAndroid.Lib;

namespace UfoShooterAndroid
{
    public enum GameState
    {
        Start,
        InGame,
        GameOver
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Accelerometer acc;
        Random ran = new Random();

        int Life = 3;
        int Points = 0;

        int Shot_delay = 400;
        int Shot_time;

        int Enemy_delay = 1000;
        int Enemy_time;

        //Hur ofta Ufo ska komma
        int Ufo_delay = 10000;
        int Ufo_time;

        Texture2D Background;
        Texture2D shipTexture;
        Texture2D shotTexture;
        Texture2D monster1Texture;
        Texture2D monster2Texture;
        Texture2D explosionTexture;
        Texture2D ufoTexture;

        SpriteFont Font;

        List<Sprite> sprites { get; set; } = new List<Sprite>();
        //List<Shot> shots { get; set; } = new List<Shot>();

        Ship ship;

        SoundEffect laserSound;

        SoundEffect explosionSound;


        float sensorValueX;
        float sensorValueY;
        GameState gameState;
        public Game1()
        {
            Life= 3;
            Points = 0;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            acc = new Accelerometer();
            acc.CurrentValueChanged += Acc_CurrentValueChanged;
            gameState = GameState.InGame;
        }

        private void Acc_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            sensorValueX = (float)e.SensorReading.Acceleration.X;
            sensorValueY = (float)e.SensorReading.Acceleration.Y;
            ship.Velocity.X = -sensorValueX * 100;
            ship.Velocity.Y = sensorValueY * 100;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;// GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();
            base.Initialize();
            acc.Start();
        }

        protected override void LoadContent()
        {
            laserSound = Content.Load<SoundEffect>("laserSound");
            explosionSound = Content.Load<SoundEffect>("explosionSound");

            Font = Content.Load<SpriteFont>("mtext");

            shotTexture = Content.Load<Texture2D>("shot");
            explosionTexture = Content.Load<Texture2D>("explosion");
            monster1Texture = Content.Load<Texture2D>("monster1");
            monster2Texture = Content.Load<Texture2D>("monster2");
            ufoTexture = Content.Load<Texture2D>("ufo");

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Background = Content.Load<Texture2D>("spacebg");
            shipTexture = Content.Load<Texture2D>("ship");
            ship = new Ship(shipTexture, this);
            sprites.Add(ship);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            
            TouchCollection tc = TouchPanel.GetState();

            if (gameState == GameState.Start || gameState == GameState.GameOver)
            {
                foreach (TouchLocation tl in tc)
                {
                    if (TouchLocationState.Pressed == tl.State)
                    {
                        gameState = GameState.InGame;
                        Life = 3;
                        Points = 0;
                        sprites.RemoveAll(e => e.GetType() != typeof(Ship));

                    }
                }
            }
            if (gameState == GameState.InGame)
            {
                // Skapa Ufo
                Ufo_time -= gameTime.ElapsedGameTime.Milliseconds;
                if (Ufo_time < 0)
                {
                    Ufo_time = ran.Next(1000,Ufo_delay);
                    sprites.Add(new Ufo(ufoTexture, this));
                    Shot_delay = 800; // Vi gör skjut-tiden långsammare när det kommer ett nytt ufo!
                }

                //Skapa lite fiender
                Enemy_time -= gameTime.ElapsedGameTime.Milliseconds;
                if (Enemy_time < 0)
                {
                    Enemy_time = 0;
                }
                if (Enemy_time == 0 && Life > 0)
                {
                    Enemy_time = Enemy_delay;
                    //Slumpa x position.
                    int x = ran.Next(0, graphics.GraphicsDevice.Viewport.Width - 100);
                    int type = ran.Next(0, 2);
                    if(type== 0)
                    {
                        sprites.Add(new Enemy(monster1Texture, this)
                        {
                            Position = new Vector2(x, -200)
                        });
                    }
                    else
                    {
                        sprites.Add(new Enemy(monster2Texture, this)
                        {
                            Position = new Vector2(x, -200)
                        });
                    }
                    
                }

                //Skapa skotten och uppdatera samt radera gamla
                Shot_time -= gameTime.ElapsedGameTime.Milliseconds; // Tiden för en loop

                //Känner av tapp på skärmen för att skjuta!
                foreach (TouchLocation tl in tc)
                {
                    if (TouchLocationState.Pressed == tl.State && Shot_time <= 0)
                    {
                        SoundPlayer.Play(laserSound);

                        Shot_time = Shot_delay;
                        sprites.Add(new Shot(shotTexture, this)
                        {
                            Position = new Vector2(ship.Position.X + 70, ship.Position.Y)
                        });
                        Shot_time = Shot_delay;
                    }
                }
                // TODO: Add your update logic here

                //Kolla kollissioner - behöver sätta ToList() efter för att vi ändrar den befintliga listan     
                foreach (Enemy e in sprites.OfType<Enemy>().ToList())
                {
                    if (e.Rectangle.Intersects(ship.Rectangle))
                    {
                        SoundPlayer.Play(explosionSound);
                        Life -= 1;
                        sprites.Add(new Explosion(explosionTexture, this)
                        {
                            Position = ship.Position
                        });
                        e.IsActive = false;
                    }
                    foreach (Shot s in sprites.OfType<Shot>().ToList())
                    {
                        if (s.Rectangle.Intersects(e.Rectangle))
                        {
                            SoundPlayer.Play(explosionSound);
                            s.IsActive = false;
                            e.IsActive = false;
                            Points += 1;
                            sprites.Add(new Explosion(explosionTexture, this)
                            {
                                Position = e.Position
                            });
                        }
                    }
                }
                foreach (Shot s in sprites.OfType<Shot>().ToList())
                {
                    foreach (Ufo u in sprites.OfType<Ufo>().ToList())
                    {
                        if (u.Rectangle.Intersects(s.Rectangle))
                        {
                            SoundPlayer.Play(explosionSound);
                            
                            sprites.Add(new Explosion(explosionTexture, this)
                            {
                                Position = u.Position
                            });
                            Points += 100; //Poäng!!!!
                           
                            u.IsActive = false;
                            s.IsActive = false;
                            Shot_delay = 150; //Lite bonus vid träff - skjuta oftare
                        }
                    }
                        
                }
                sprites.ForEach(e => e.Update(gameTime));
                sprites.RemoveAll(e => e.IsActive == false);
            }
            if (Life == 0)
            {
                gameState = GameState.GameOver;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(Background, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White);

            spriteBatch.DrawString(Font, "Life: " + Life, new Vector2(60, 20), Color.White);
            spriteBatch.DrawString(Font, "Points: " + Points, new Vector2(680, 20), Color.White);
            if (gameState == GameState.InGame)
            {
                sprites.ForEach(e => e.Draw(spriteBatch));
            }
            else if (gameState == GameState.Start)
            {
                spriteBatch.DrawString(Font, "Tap to start", new Vector2(380, 820), Color.White);
            }
            else if (gameState == GameState.GameOver)
            {
                spriteBatch.DrawString(Font, "Game Over - Tap to retry", new Vector2(150, 820), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}