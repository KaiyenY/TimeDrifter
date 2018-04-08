using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
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
        Pause
    }


    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        #region Options
        public static bool fullscreen = false;
        public static int screenHeight = 720;
        public static int screenWidth = 1280;
        #endregion

        // SpriteFonts
        public static SpriteFont connection;

        #region Texture2D's
        public static Texture2D button;
        public static Texture2D square;
        public static List<Texture2D> tileSprites;
        #endregion

        // Models
        public static Model building;

        //GameState Enum
        public static GameState GameState;
        
        private float timeSinceLastReRoute = 0.0f;
        
        public static Camera camera;
        public static GameTime gameTime;

        // Constructor
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

        protected override void Initialize()
        {
            IsMouseVisible = true;                  // Make mouse visible

            GameState = GameState.Menu;             // Default GameState    
            camera = new Camera();                  // Camera thing
            tileSprites = new List<Texture2D>();    // List of tile sprites

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            button = Content.Load<Texture2D>("Textures/UI/Button");
            square = Content.Load<Texture2D>("Textures/Square");

            // GameMaster Load
            foreach (GameObject obj in GameMaster.GameObjects)
                obj.Sprite = Content.Load<Texture2D>(obj.SpritePath);

            // Map Load
            for (int i = 0; i < 6; i++)
                tileSprites.Add(Content.Load<Texture2D>("Textures/Tiles/Tile" + i));

            // SpriteFonts
            connection = Content.Load<SpriteFont>("Fonts/ConnectionSerif");

            //3D
            building = Content.Load<Model>("3D/untitled");
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

                    camera.Update();
                    GameMaster.Update();

                    if (timeSinceLastReRoute > 10)
                    {
                        timeSinceLastReRoute = 0;
                    }
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;

                case GameState.Menu:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime g)
        {
            gameTime = g;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            if (GameState != GameState.Menu && GameState != GameState.Options)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);

                Map.DrawGround();
                GameMaster.Draw();
                Map.DrawBuildings();

                spriteBatch.End();
            }

            UIManager.Draw();           // UI always draws on top
            base.Draw(gameTime);
        }
    }
}
