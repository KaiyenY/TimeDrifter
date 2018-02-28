using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    /// <summary>
    //ruben was here
    /// Matthew is the best architect.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //spritefont
        SpriteFont comicSans;

        //arrow
        Texture2D arrow;

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
            arrow = Content.Load<Texture2D>("arrow");
        }
        
        protected override void UnloadContent()
        {
        }

        // variables for the turret
        static double x = 0;
        static double y = 0;
        static double angle = 0;
        static bool shooting;

        static void CalculateTurretData(float centerX, float centerY)
        {
            // Mouse position is compared to the center of the screen
            // In the future, we will compare it to the position of the car

            MouseState s = Mouse.GetState();
            shooting = (s.LeftButton == ButtonState.Pressed);

            x = s.Position.X - centerX;
            y = s.Position.Y - centerY;
            angle = Math.Atan(y / x) * 180/3.14159;

            if (x < 0)
            {
                angle += 180;
            }

            if (angle < 0)
                angle += 360;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Get data, comparing mouse position to center of screen
            CalculateTurretData(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            string text = "Checking distance between mouse\nand center of screen\nfor gun on roof: \nX: " + x + "\nY: " + y + "\nangle: " + angle;

            spriteBatch.Begin();




            spriteBatch.Draw(arrow, 
                new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, arrow.Width, arrow.Height), null, Color.White, (float)((angle+90) * 3.14159 / 180), new Vector2(arrow.Width / 2, arrow.Height / 2), SpriteEffects.None, 0f);//draw it :D




            spriteBatch.DrawString(comicSans, text, new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2), Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
