using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Pause
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
        public static SpriteFont comicSans64;
        
        private Player player;

        // Texture2Ds
        public static Texture2D square;
        public static List<Texture2D> tileSprites;

        //GameState Enum
        private static GameState GameState;

        // Menu elements
        private MenuElement startButton;
        private MenuElement exitButton;

        // Pause elements
        private MenuElement resumeButton;           // Resumes game
        private MenuElement optionsButton;          // Goes to options
        private MenuElement exitPauseButton;        // Exits to menu
        private MenuElement quitButton;             // Quits game

        // all cops and tanks
        private AI ai;
        private float timeSinceLastReRoute = 0.0f;

        public static Map map;
        private Camera camera;
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Texture2Ds
            Texture2D turretSprite = Content.Load<Texture2D>("Textures/Turret");
            Texture2D bulletSprite = Content.Load<Texture2D>("Textures/Bullet");
            Texture2D playerSprite = Content.Load<Texture2D>("Textures/RedCar");
            Texture2D cop = Content.Load<Texture2D>("Textures/Cop");
            square = Content.Load<Texture2D>("Textures/Square");
            Texture2D idle = Content.Load<Texture2D>("Textures/UI/ButtonRectangleTemp");
            Texture2D pressed = Content.Load<Texture2D>("Textures/UI/ButtonPressed");

            for (int i = 0; i < 6; i++)
            {
                tileSprites.Add(Content.Load<Texture2D>("Textures/Tiles/Tile" + i));
            }

            // SpriteFonts
            comicSans = Content.Load<SpriteFont>("comic");
            comicSans64 = Content.Load<SpriteFont>("comic64");

            // objects
            player = new Player(
                playerSprite,
                bulletSprite,
                turretSprite,
                new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                comicSans);
            ai = new AI(cop);

            // MenuButtons
            startButton = new MenuElement(new Rectangle(new Point((screenWidth / 2) - 100, 200), new Point(200, 50)), idle, pressed);
            exitButton = new MenuElement(new Rectangle(new Point((screenWidth / 2) - 100, 300), new Point(200, 50)), idle, pressed);
            resumeButton = new MenuElement(new Rectangle(new Point((screenWidth / 2) - 100, 250), new Point(200, 50)), idle, pressed);
            optionsButton = new MenuElement(new Rectangle(new Point((screenWidth / 2) - 100, 350), new Point(200, 50)), idle, pressed);
            exitPauseButton = new MenuElement(new Rectangle(new Point((screenWidth / 2) - 100, 450), new Point(200, 50)), idle, pressed);
            quitButton = new MenuElement(new Rectangle(new Point((screenWidth / 2) - 100, 550), new Point(200, 50)), idle, pressed);
        }

        protected override void UnloadContent()
        {
            map = null;
        }

        protected override void Update(GameTime g)
        {
            gameTime = g;

            Input.Update();     // Should be the FIRST thing that updates
            
            switch (GameState) //Check for gamestate
            {
                case GameState.Menu:
                    if (Input.KeyTap(Keys.Escape))
                        Exit();

                    if (Input.KeyTap(Keys.U))
                    {
                        Editor editor = new Editor();
                        editor.Show();
                    }

                    if (startButton.IsClicked())
                    {
                        byte[] size = { 5, 5 };

                        map = new Map();

                        GameState = GameState.Game;
                    }

                    if(exitButton.IsClicked())
                    {
                        Exit();
                    }
                    break;

                case GameState.Game:
                    if (Input.KeyTap(Keys.Escape))
                    {
                        GameState = GameState.Pause;
                    }

                    Managers.GameMaster.Update();

                    // update turret position to player car position
                    // or in this case, the center of the screen
                    player.Update();
                    if(timeSinceLastReRoute > 10)
                    {
                        ai.AssignNewPathsToEnemies(ai.nodes[6]);
                        timeSinceLastReRoute = 0;
                    }
                    ai.Update();
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    camera.Update(player.Position);
                    break;

                case GameState.Pause:
                    if (resumeButton.IsClicked())
                    {
                        GameState = GameState.Game;
                    }
                    if (optionsButton.IsClicked())
                    {
                        // Not implemented
                    }
                    if (exitPauseButton.IsClicked())
                    {
                        GameState = GameState.Menu;
                    }
                    if (quitButton.IsClicked())
                    {
                        Exit();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime g)
        {
            gameTime = g;
            switch (GameState)
            {
                case GameState.Menu:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    spriteBatch.DrawString(
                        comicSans64, 
                        "Welcome to Project Apathy", 
                        new Vector2((screenWidth / 2) - (comicSans64.MeasureString("Welcome to Project Apathy").X / 2), 20), 
                        Color.White);
                    startButton.DrawWithText(comicSans, "Start", Color.White);
                    exitButton.DrawWithText(comicSans, "Exit", Color.White);
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

                case GameState.Pause:
                    spriteBatch.Begin();

                    // Pause title
                    spriteBatch.DrawString(
                        comicSans64, 
                        "Pause", 
                        new Vector2((screenWidth / 2) - (comicSans64.MeasureString("Pause").X / 2), 100),
                        Color.White);

                    // Buttons
                    resumeButton.DrawWithText(comicSans, "Resume", Color.White);
                    optionsButton.DrawWithText(comicSans, "Options", Color.White);
                    exitPauseButton.DrawWithText(comicSans, "Quit to Menu", Color.White);
                    quitButton.DrawWithText(comicSans, "Exit Game", Color.White);

                    spriteBatch.End();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
