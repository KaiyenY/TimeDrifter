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
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        // SpriteFonts
        public static SpriteFont comicSans;

        private Turret turret1;
        private Player player;

        // Texture2Ds
        public static Texture2D square;

        //GameState Enum
        private static GameState GameState;

        private Mover test;
        private MenuElement button;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // show the mouse
            this.IsMouseVisible = true;
            GameState = GameState.Game;

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
            turret1 = new Turret(gun, bullet);

            Texture2D buttonTexture = Content.Load<Texture2D>("ButtonRectangleTemp");
            Texture2D car = Content.Load<Texture2D>("RedCar");
            player = new Player(car, GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            // Other Content
            test = new Mover();

            button = new MenuElement(new Rectangle(new Point(20, 200), new Point(400, 100)), buttonTexture);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();     // Should be the FIRST thing that updates
            
            if (Input.KeyTap(Keys.Escape))
                Exit();

            switch (GameState) //Check for gamestate
            {
                case GameState.Menu:
                    if (Input.KeyTap(Keys.P))
                    {
                        GameState = GameState.Game;
                    }
                    break;


                case GameState.Game:
                    if (Input.KeyTap(Keys.P))
                    {
                        GameState = GameState.Menu;
                    }
                    if (Input.KeyTap(Keys.Space))
                    {
                        test.AddForceAtPos(new Vector2(5, 5), new Vector2(50, 0));
                    }
                    // update turret position to player car position
                    // or in this case, the center of the screen
                    player.Update();
                    //turret1.Update(gameTime, player.posX, player.posY);
                    turret1.Update(gameTime, player.Position);
                    test.Update(gameTime);
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
                        player.Draw();
                        test.Draw();
                        turret1.Draw();

                        spriteBatch.DrawString(comicSans, Vector2.Divide(test.Velocity, Vector2.Normalize(test.Velocity)).ToString(), new Vector2(300, 300), Color.Black);
                        break;
                    }

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
