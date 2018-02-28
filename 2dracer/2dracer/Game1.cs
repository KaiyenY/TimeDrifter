using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //spritefont
        SpriteFont comicSans;
        Turret turret1;

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
            comicSans = Content.Load<SpriteFont>("comic");

            Texture2D arrow = Content.Load<Texture2D>("turret");
            Texture2D bullet = Content.Load<Texture2D>("bullet");
            turret1 = new Turret(arrow, bullet);
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update turret position to car position
            // or in this case, the center of the screen
            turret1.Update(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string text = turret1.Debug();

            spriteBatch.Begin();
            turret1.Draw(spriteBatch);

            spriteBatch.DrawString(comicSans, text, new Vector2(20, 20), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
