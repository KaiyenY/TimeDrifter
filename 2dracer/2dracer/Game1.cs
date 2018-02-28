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
        Turret turret1;

        // Texture2Ds
        public static Texture2D square;

        Mover test;

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
            Texture2D arrow = Content.Load<Texture2D>("turret");
            Texture2D bullet = Content.Load<Texture2D>("bullet");

            // Other Content
            test = new Mover();

            turret1 = new Turret(arrow, bullet);
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

            // update turret position to car position
            // or in this case, the center of the screen
            turret1.Update(gameTime, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string text = turret1.Debug();

            spriteBatch.Begin();
            spriteBatch.DrawString(comicSans, "Hello World\nAnd Goodbye!", new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2), Color.White);
            spriteBatch.DrawString(comicSans, Vector2.Divide(test.Velocity, Vector2.Normalize(test.Velocity)).ToString(), new Vector2(300, 300), Color.Black);
            test.Draw();
            turret1.Draw();

            spriteBatch.DrawString(comicSans, text, new Vector2(20, 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
