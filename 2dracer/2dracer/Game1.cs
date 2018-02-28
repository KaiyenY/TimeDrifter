using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        // SpriteFonts
        public static SpriteFont comicSans;

        private Turret turret1;
        private Car car1;

        // Texture2Ds
        public static Texture2D square;

        private Mover test;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // show the mouse
            this.IsMouseVisible = true;
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // SpriteFonts
            comicSans = Content.Load<SpriteFont>("comic");

            // Texture2Ds
            square = Content.Load<Texture2D>("square");
            Texture2D gun = Content.Load<Texture2D>("turret");
            Texture2D bullet = Content.Load<Texture2D>("bullet");
            turret1 = new Turret(gun, bullet);

            Texture2D car = Content.Load<Texture2D>("RedCar");
            car1 = new Car(car, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            // Other Content
            test = new Mover();
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                test.AddForceAtPos(new Vector2(5, 5), new Vector2(50,0));
            }

            test.Update(gameTime);

            car1.Update();
            turret1.Update(gameTime, car1.posX, car1.posY);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string text = turret1.Debug();

            spriteBatch.Begin();
            car1.Draw();
            test.Draw();
            turret1.Draw();
            
            spriteBatch.DrawString(comicSans, Vector2.Divide(test.Velocity, Vector2.Normalize(test.Velocity)).ToString(), new Vector2(300, 100), Color.Black);
            spriteBatch.DrawString(comicSans, text, new Vector2(20, 80), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
