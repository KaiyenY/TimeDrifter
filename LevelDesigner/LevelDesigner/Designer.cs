using LevelDesigner.Managers;
using LevelDesigner.MapElements;
using LevelDesigner.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner
{
    public class Designer : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Texture2D's
        public static Texture2D square;
        public static Texture2D menuButtonSprite;
        public static Texture2D[] TileSprites = new Texture2D[6];
        #endregion

        #region UI
        private Button menuButton;
        private Button[] tileButtons;
        private Container textures;
        private Container tileInfo;
        #endregion

        public static SpriteFont arial;

        public static int SelectedTexture = 0;

        private StartUp form;

        public static int screenHeight;
        public static int screenWidth;

        public Designer(int screenHeight, int screenWidth, StartUp form)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferredBackBufferWidth = screenWidth;

            this.form = form;

            IsMouseVisible = true;
        }
        
        protected override void Initialize()
        {
            base.Initialize();

            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menuButtonSprite = Content.Load<Texture2D>("Menu Button");

            square = Content.Load<Texture2D>("Square");

            arial = Content.Load<SpriteFont>("Arial");

            for (int i = 0; i < 6; i++)
            {
                TileSprites[i] = Content.Load<Texture2D>("Tiles/Tile" + i);
            }

            // Create menu button
            menuButton = new Button(new Rectangle(graphics.PreferredBackBufferWidth - 64, 0, 64, 64), menuButtonSprite, true);

            // Create textures container
            textures = new Container(new Rectangle(
                graphics.PreferredBackBufferWidth - (graphics.PreferredBackBufferWidth / 5), 64, 
                graphics.PreferredBackBufferWidth / 5, graphics.PreferredBackBufferHeight / 2),
                false);

            // Create tile buttons
            tileButtons = new Button[6]
            {
                new Button(new Rectangle(textures.Position.X, textures.Position.Y, textures.Size.X / 2, textures.Size.Y / 3), TileSprites[0], true),
                new Button(new Rectangle(textures.Position.X, textures.Position.Y + textures.Size.Y / 3, textures.Size.X / 2, textures.Size.Y / 3), TileSprites[1], true),
                new Button(new Rectangle(textures.Position.X, textures.Position.Y + textures.Size.Y * 2 / 3, textures.Size.X / 2, textures.Size.Y / 3), TileSprites[2], true),
                new Button(new Rectangle(textures.Position.X + textures.Size.X / 2, textures.Position.Y, textures.Size.X / 2, textures.Size.Y / 3), TileSprites[3], true),
                new Button(new Rectangle(textures.Position.X + textures.Size.X / 2, textures.Position.Y + textures.Size.Y / 3, textures.Size.X / 2, textures.Size.Y / 3), TileSprites[4], true),
                new Button(new Rectangle(textures.Position.X + textures.Size.X / 2, textures.Position.Y + textures.Size.Y * 2 / 3, textures.Size.X / 2, textures.Size.Y / 3), TileSprites[5], true),
            };
        }

        protected override void UnloadContent()
        {
            form.Close();
        }
        
        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            Map.Update();

            Camera.Update();
            
            if (textures.Enabled)
            {
                for (int i = 0; i < tileButtons.Length; i++)
                {
                    if (tileButtons[i].IsClicked())
                    {
                        SelectedTexture = i;
                    }
                }
            }

            if (!menuButton.Enabled && Input.MouseReleased(MouseButton.Left) && textures.Enabled)
            {
                menuButton.Enabled = true;
                textures.Enabled = false;
            }

            if (menuButton.Enabled && menuButton.IsClicked() && !textures.Enabled)
            {
                menuButton.Enabled = false;
                textures.Enabled = true;
            }

            if (Input.KeyTap(Keys.Escape))
                Exit();
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Camera.ViewMatrix);

            Map.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            // Draw UI here
            menuButton.Draw(spriteBatch);
            textures.Draw(spriteBatch);

            if (textures.Enabled)
            {
                // Draw elements in textures container here
                foreach(Button butt in tileButtons)
                {
                    butt.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }
    }
}

// -- Genoah Martinelli