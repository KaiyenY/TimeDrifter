using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
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
        Menu
    }


    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        // Options (Maybe implement?)
        public static bool fullscreen = false;
        public static int screenHeight = 720;
        public static int screenWidth = 1280;

        // SpriteFonts
        public static SpriteFont comicSans;
        
        private Player player;

        // Texture2Ds
        public static Texture2D square;
        private List<Texture2D> tileSprites;

        //GameState Enum
        private static GameState GameState;

        private MenuElement startButton;
        private MenuElement exitButton;

        // all cops and tanks
        private AI ai;
        private float timeSinceLastReRoute = 0.0f;

        public static Map map;
        private Camera camera;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Window properties
            graphics.IsFullScreen = fullscreen;                     // Fullscreen or not
            graphics.PreferredBackBufferHeight = screenHeight;      // Window height
            graphics.PreferredBackBufferWidth = screenWidth;        // Window width
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;              // Show the mouse

            GameState = GameState.Menu;         // Default GameState

            camera = new Camera();              // Sets up camera

            tileSprites = new List<Texture2D>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Texture2Ds
            Texture2D turretSprite = Content.Load<Texture2D>("Textures/Turret");
            Texture2D bulletSprite = Content.Load<Texture2D>("bullet");
            Texture2D playerSprite = Content.Load<Texture2D>("Textures/RedCar");
            Texture2D cop = Content.Load<Texture2D>("cop");
            square = Content.Load<Texture2D>("square");
            Texture2D idle = Content.Load<Texture2D>("ButtonRectangleTemp");
            Texture2D pressed = Content.Load<Texture2D>("buttonPressed");

            for (int i = 0; i < 6; i++)
            {
                tileSprites.Add(Content.Load<Texture2D>("Textures/Tiles/Tile" + i));
            }

            // SpriteFonts
            comicSans = Content.Load<SpriteFont>("comic");

            // objects
            player = new Player(
                playerSprite, 
                bulletSprite, 
                turretSprite,
                new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                comicSans);
            ai = new AI(cop);

            //MenuButtons
            startButton = new MenuElement(new Rectangle(new Point(20, 50), new Point(200, 50)), idle, pressed);
            exitButton = new MenuElement(new Rectangle(new Point(20, 120), new Point(200, 50)), idle, pressed);
        }

        protected override void UnloadContent()
        {
            map = null;
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();         // Should be the FIRST thing that updates
            
            switch (GameState)      //Check for gamestate
            {
                case GameState.Menu:

                    if (Input.KeyTap(Keys.Escape))
                        Exit();

                    if (Input.KeyTap(Keys.U))
                    {
                        // Open up editor
                        GameState = GameState.LevelEditor;

                        Editor editor = new Editor();
                    }

                    if (startButton.IsClicked())
                    {
                        map = new Map(tileSprites);

                        GameState = GameState.Game;
                    }

                    if(exitButton.IsClicked())
                    {
                        Exit();
                    }
                    break;

                case GameState.LevelEditor:
                    if (Input.KeyTap(Keys.Escape))
                        GameState = GameState.Menu;
                    
                    break;

                case GameState.Game:
                    if (Input.KeyTap(Keys.Escape))
                    {
                        GameState = GameState.Menu;

                        map = null;
                    }

                    Managers.GameMaster.Update(gameTime);

                    // update turret position to player car position
                    // or in this case, the center of the screen
                    player.Update(gameTime);
                    if(timeSinceLastReRoute > 10)
                    {
                        ai.AssignNewPathsToEnemies(ai.nodes[7]);
                        timeSinceLastReRoute = 0;
                    }
                    ai.Update();
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    camera.Update(player.Position);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (GameState)
            {
                case GameState.Menu:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.DrawString(comicSans, "Welcome to Project Apathy", new Vector2(GraphicsDevice.Viewport.Width / 2, 20), Color.White);
                    startButton.DrawWithText(comicSans, "Start", Color.White);
                    exitButton.DrawWithText(comicSans, "Exit", Color.White);
                    spriteBatch.DrawString(comicSans, "Press Esc to Quit", new Vector2(50, 600), Color.White);
                    break;

                case GameState.LevelEditor:

                    // Acts like a debug mode while in the editor
                    if (map != null)
                    {
                        map.Draw();
                    }

                    break;

                case GameState.Game:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    map.Draw();
                    Managers.GameMaster.Draw();
                    ai.Draw();
                    player.Draw();
                    spriteBatch.End();

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                    spriteBatch.DrawString(comicSans, "Press Esc to go to the Menu", new Vector2(50, 600), Color.White);
                    player.DrawHUD();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
