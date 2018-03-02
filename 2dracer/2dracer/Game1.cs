using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    /// <summary>
    /// FSM that switches between GameStates
    /// </summary>
    public enum GameState
    {
        Game,
        LevelEditor,
        Menu
    }


    public class Game1 : Game
    {
        // Fields
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        
        // Input Manager
        private Input input;

        // GameObjects
        private Mover test;
        private Player player;

        // UI
        private MenuElement button;

        // SpriteFonts
        public static SpriteFont comicSans;

        // Texture2Ds
        public static Texture2D square;

        // GameState Enum
        private static GameState GameState;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        // Methods
        protected override void Initialize()
        {
            this.IsMouseVisible = true;     // Show the mouse

            GameState = GameState.Game;     // Default GameState
            
            input = new Input();            // Input Manager

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

            Texture2D buttonTexture = Content.Load<Texture2D>("ButtonRectangleTemp");
            Texture2D car = Content.Load<Texture2D>("RedCar");
            player = new Player(car, gun, bullet, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            // Other Content
            test = new Mover();

            
            button = new MenuElement(new Rectangle(new Point(20, 200), new Point(400,100)), buttonTexture);
        }
        
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            input.Update();     // Should be the FIRST thing that updates

            if (Input.TapKey(Keys.Escape))
                Exit();

            switch (GameState) //Check for gamestate
            {
                case GameState.Menu:
                    if (Input.TapKey(Keys.P))
                    {
                        GameState = GameState.Game;
                    }
                    break;

                 
                case GameState.Game:
                    // For changing GameState
                    if (Input.TapKey(Keys.P))
                    {
                        GameState = GameState.Menu;
                    }
                    
                    if (Input.TapKey(Keys.Space))
                    {
                        test.AddForceAtPos(new Vector2(5, 5), new Vector2(50, 0));
                    }

                    // Update GameObjects here ----

                    // Make update manager?

                    player.Update(gameTime, input);
                    test.Update(gameTime);

                    // ----------------------------
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (GameState)
            {
                case GameState.Menu:
                    {
                        button.DrawWithText("Game");
                        break;
                    }
                case GameState.Game:
                    {
                        // Make draw manager?

                        player.Draw();
                        test.Draw();
                        
                        spriteBatch.DrawString(comicSans, Vector2.Divide(test.Velocity, Vector2.Normalize(test.Velocity)).ToString(), new Vector2(300, 300), Color.Black);
                        break;
                    }
                
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
