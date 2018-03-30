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
        LevelEditor,
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
        public static SpriteFont comicSans;
        public static SpriteFont comicSans64;

        // Texture2Ds
        public static Texture2D square;
        public static List<Texture2D> tileSprites;

        //GameState Enum
        public static GameState GameState;
        
        private float timeSinceLastReRoute = 0.0f;

        public static AI ai;
        public static Map map;
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
            square = Content.Load<Texture2D>("Textures/Square");


            // GameMaster Load
            foreach (GameObject obj in GameMaster.GameObjects)
            {
                obj.Sprite = Content.Load<Texture2D>(obj.SpritePath);
            }


            // Map Load
            for (int i = 0; i < 6; i++)
            {
                tileSprites.Add(Content.Load<Texture2D>("Textures/Tiles/Tile" + i));
            }

            
            // SpriteFonts
            comicSans = Content.Load<SpriteFont>("comic");
            comicSans64 = Content.Load<SpriteFont>("comic64");


            // UI Load
            foreach (UIElement element in UIManager.Elements)
            {
                // Will make this better soonish
                if (element.SpritePath != null)
                {
                    element.Sprite = Content.Load<Texture2D>(element.SpritePath);
                }
                if (element.Font != comicSans64)
                {
                    element.Font = comicSans;
                }
            }
        }

        protected override void UnloadContent()
        {
            map = null;
        }

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
                        ai.AssignNewPathsToEnemies(ai.nodes[6]);
                        timeSinceLastReRoute = 0;
                    }
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;

                case GameState.Menu:
                    if (Input.KeyTap(Keys.U))
                    {
                        Editor editor = new Editor();
                        editor.Show();

                        GameState = GameState.LevelEditor;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime g)
        {
            gameTime = g;

            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            switch (GameState)
            {
                case GameState.Game:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);
                    map.Draw();
                    GameMaster.Draw();
                    spriteBatch.End();
                    
                    break;

                case GameState.LevelEditor:
                    GraphicsDevice.Clear(Color.Black);
                    break;

                case GameState.Pause:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);
                    map.Draw();
                    GameMaster.Draw();
                    spriteBatch.End();

                    break;
            }

            UIManager.Draw();           // UI always draws on top

            base.Draw(gameTime);
        }
    }
}
