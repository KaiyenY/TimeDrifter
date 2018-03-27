﻿using Microsoft.Xna.Framework;
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
        Options,
        Pause
    }


    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
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
        public static GameState GameState;

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

            // Load UI
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

            UIManager.Update();         // Updates all UI elements
            
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
                    break;

                case GameState.Game:
                    if (Input.KeyTap(Keys.Escape))
                    {
                        GameState = GameState.Pause;
                    }

                    GameMaster.Update();

                    player.Update();
                    camera.Update(player.Position);

                    if (timeSinceLastReRoute > 10)
                    {
                        ai.AssignNewPathsToEnemies(ai.nodes[6]);
                        timeSinceLastReRoute = 0;
                    }
                    ai.Update();
                    timeSinceLastReRoute += (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                case GameState.Menu:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

                    UIManager.Draw();

                    spriteBatch.End();
                    break;

                case GameState.Game:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);
                    
                    GameMaster.Draw();          // Need to put everything inside of this
                    map.Draw();
                    ai.Draw();
                    player.Draw();
                    UIManager.Draw();

                    spriteBatch.End();
                    break;

                case GameState.Pause:
                    spriteBatch.Begin();

                    map.Draw();
                    ai.Draw();
                    player.Draw();
                    UIManager.Draw();

                    spriteBatch.End();
                    break;

                case GameState.Options:
                    spriteBatch.Begin();

                    GraphicsDevice.Clear(Color.Black);

                    UIManager.Draw();

                    spriteBatch.End();
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
