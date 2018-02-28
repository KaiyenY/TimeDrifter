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

            Texture2D arrow = Content.Load<Texture2D>("arrow");
            turret1 = new Turret(arrow);
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
            turret1.setPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            // get angle
            turret1.calcAngle();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string text = turret1.Debug();

            spriteBatch.Begin();
            turret1.Draw(spriteBatch);

            spriteBatch.DrawString(comicSans, text, new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
