using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _2dracer.MapElements;
using _2dracer.Managers;

namespace _2dracer
{
    /// <summary>
    /// FSM that switches between GameStates
    /// </summary>
    public enum GameState
    {
        Game,
        Menu,
        Options,
        Pause,
        GameOver
    }


    public class Game1 : Game
    {
        #region Fields
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        #region Options
        public static bool fullscreen = false;
        public static int screenHeight = 720;
        public static int screenWidth = 1280;
        #endregion

        //GameState Enum
        public static GameState GameState;
        
        private float timeSinceLastReRoute = 0.0f;
        
        public static Camera camera;
        public static GameTime gameTime;
        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Window properties
            graphics.IsFullScreen = fullscreen;                     // Fullscreen or not
            graphics.PreferredBackBufferHeight = screenHeight;      // Window height
            graphics.PreferredBackBufferWidth = screenWidth;        // Window width

            gameTime = new GameTime();
        }


        #region Methods
        protected override void Initialize()
        {
            IsMouseVisible = true;                  // Make mouse visible

            GameState = GameState.Menu;             // Default GameState    
            camera = new Camera();                  // Camera thing

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Handles all of the loading
            LoadManager.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime g)
        {
            gameTime = g;

            Input.Update();     // Should be the FIRST thing that updates

            UIManager.Update();
            
            switch (GameState)
            {
                case GameState.Game:
                    if (Input.KeyTap(Keys.Escape))
                    {
                        GameState = GameState.Pause;
                    }

                    if (Input.KeyTap(Keys.M))
                    {
                        GameState = GameState.GameOver;
                    }

                    camera.Update();
                    GameMaster.Update();

                    if (timeSinceLastReRoute > 10)
                    {
                        timeSinceLastReRoute = 0;
                    }
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime g)
        {
            gameTime = g;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (GameState != GameState.Menu && GameState != GameState.Options && GameState != GameState.GameOver)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);

                Map.DrawGround();
                GameMaster.Draw();
                spriteBatch.End();

                Map.DrawBuildings();
            }

            UIManager.Draw();           // UI always draws on top
            base.Draw(gameTime);
        }
        #endregion
    }
}
