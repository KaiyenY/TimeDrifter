using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LevelDesigner
{
    public class Designer : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Texture2D[] TileSprites = new Texture2D[6];

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

            for (int i = 0; i < 6; i++)
            {
                TileSprites[i] = Content.Load<Texture2D>("Tiles/Tile" + i);
            }
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

            if (Input.KeyTap(Keys.Escape))
                Exit();
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Camera.ViewMatrix);

            Map.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
