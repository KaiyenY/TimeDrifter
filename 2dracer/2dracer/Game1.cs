using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using _2dracer.MapElements;

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

        private Turret turret1;
        private Player player;

        // Texture2Ds
        public static Texture2D square;
        public static Texture2D tilespritesheet;

        //GameState Enum
        private static GameState GameState;

        private Mover test;
        private MenuElement startButton;
        private MenuElement exitButton;

        // all cops and tanks
        private AI ai;
        private float timeSinceLastReRoute = 0.0f;

        public static Map map;

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
            // show the mouse
            this.IsMouseVisible = true;

            GameState = GameState.Menu;


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
            Texture2D gun = Content.Load<Texture2D>("Textures/Turret");
            Texture2D bullet = Content.Load<Texture2D>("bullet");
            turret1 = new Turret(gun, bullet);
            Texture2D car = Content.Load<Texture2D>("Textures/RedCar");
            player = new Player(car, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));

            Texture2D cop = Content.Load<Texture2D>("cop");
            ai = new AI(cop);

            tilespritesheet = Content.Load<Texture2D>("Textures/Spritesheet");

            // Other Content
            test = new Mover();

            //MenuButtons
            Texture2D idle = Content.Load<Texture2D>("ButtonRectangleTemp");
            Texture2D pressed = Content.Load<Texture2D>("buttonPressed");
            startButton = new MenuElement(new Rectangle(new Point(20, 50), new Point(200, 50)), idle, pressed);
            exitButton = new MenuElement(new Rectangle(new Point(20, 120), new Point(200, 50)), idle, pressed);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();     // Should be the FIRST thing that updates
            
            switch (GameState) //Check for gamestate
            {
                case GameState.Menu:
                    if (Input.KeyTap(Keys.Escape))
                        Exit();

                    if (startButton.IsClicked())
                    {
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
                        GameState = GameState.Menu;

                        map = null;
                    }
                    if (Input.KeyTap(Keys.Space))
                    {
                        test.AddForceAtPos(new Vector2(5, 5), new Vector2(50, 0));
                    }
                    // update turret position to player car position
                    // or in this case, the center of the screen
                    player.Update();
                    turret1.Update(gameTime, player.Position);
                    test.Update(gameTime);
                    if(timeSinceLastReRoute > 2)
                    {
                        ai.Pathfind(ai.nodes[10]);
                    }
                    ai.Update(player.Position);
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    map.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (GameState)
            {
                case GameState.Menu:

                    spriteBatch.DrawString(comicSans, "Welcome to Project Apathy", new Vector2(GraphicsDevice.Viewport.Width / 2, 20), Color.White);
                    startButton.DrawWithText(comicSans, "Start", Color.White);
                    exitButton.DrawWithText(comicSans, "Exit", Color.White);
                    spriteBatch.DrawString(comicSans, "Press Esc to Quit", new Vector2(0, 420), Color.White);
                    break;

                case GameState.Game:
                    map.Draw();

                    ai.Draw();
                    player.Draw();
                    test.Draw();
                    turret1.Draw();

                    spriteBatch.DrawString(comicSans, Vector2.Divide(test.Velocity, Vector2.Normalize(test.Velocity)).ToString(), new Vector2(300, 300), Color.Black);
                    spriteBatch.DrawString(comicSans, "Press Esc to go to the Menu", new Vector2(0, 420), Color.White);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
