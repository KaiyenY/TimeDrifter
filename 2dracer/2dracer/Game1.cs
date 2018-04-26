using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        Instructions,
        Options,
        Pause,
        GameOver
    }


    public class Game1 : Game
    {
        #region Fields
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        //GameState Enum
        public static GameState GameState;
        
        public static Camera camera;
        public static GameTime gameTime;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Options.InitializeSettings(graphics, Window);

            gameTime = new GameTime();
        }


        #region Methods
        protected override void Initialize()
        {
            IsMouseVisible = true;                  // Make mouse visible

            GameState = GameState.Menu;             // Default GameState    
            camera = new Camera();                  // Camera thing
            //Map.AssignNeighbors();                  // Assign Neighbors to each node (for pathfinding)
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Handles all of the loading
            LoadManager.LoadContent(Content);
            Map.LoadMap();
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime g)
        {
            gameTime = g;

            // Updates all input states (should update first)
            Input.Update();

            // Handles all of the music c:
            AudioManager.Update();

            // Handles drawing the UI
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
                    Map.Update();


                    int x = (int)(Player.PlayerPos.X / 768);
                    int y = (int)(Player.PlayerPos.Y / 768);

                    if (x >= 0 && y >= 0)
                        if (x < Map.Tiles.GetLength(0))
                            if(y < Map.Tiles.GetLength(1))
                                if (Map.Tiles[x, y].Node != null)
                                    System.Console.WriteLine(Map.Tiles[x, y].Node.ToString());
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime g)
        {
            // Depth Buffer for Buildings
            var state = new DepthStencilState
            {
                DepthBufferEnable = true
            };

            gameTime = g;
            GraphicsDevice.Clear(Color.Black);
            

            if (GameState != GameState.Menu && GameState != GameState.Options && GameState != GameState.GameOver && GameState != GameState.Instructions)
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.ViewMatrix);
                Map.Draw();
                GameMaster.Draw();
                spriteBatch.End();


                //transparent texture on screen
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);
                if(Player.slowMo) spriteBatch.Draw(LoadManager.Sprites["TimeJuiceScreen"], new Rectangle(0, 0, Options.Graphics.GraphicsDevice.Viewport.Width, Options.Graphics.GraphicsDevice.Viewport.Height), Color.White);
                spriteBatch.End();

                // Apply Depth Buffer for 3D
                GraphicsDevice.DepthStencilState = state;
                Map.DrawBuildings();

            }

            UIManager.Draw();           // UI always draws on top
            base.Draw(gameTime);
        }
        #endregion
    }
}
