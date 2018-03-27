using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2dracer.UI;

namespace _2dracer.Managers
{
    /// <summary>
    /// Controls the user interface
    /// </summary>
    public static class UIManager
    {
        // Properties
        public static List<UIElement> Elements { get; private set; }

        // Constructor
        static UIManager()
        {
            Elements = new List<UIElement>
            {
                // Menu Elements
                new UIElement(new Point((Game1.screenWidth / 2), 100), Game1.comicSans64, GameState.Menu, "Project Apathy"),
                new Button(new Point((Game1.screenWidth / 2) - 100, 250), GameState.Menu, "Game", "Play"),
                new Button(new Point((Game1.screenWidth / 2) - 100, 350), GameState.Menu, "Options", "Options"),
                new Button(new Point((Game1.screenWidth / 2) - 100, 450), GameState.Menu, "Exit", "Exit"),

                // Pause Elements
                new UIElement(new Point((Game1.screenWidth / 2), 100), Game1.comicSans64, GameState.Pause, "Pause"),
                new Button(new Point((Game1.screenWidth / 2) - 100, 250), GameState.Pause, "Game", "Resume"),
                new Button(new Point((Game1.screenWidth / 2) - 100, 350), GameState.Pause, "Menu", "Exit to Menu"),

                // Option Elements
                new UIElement(new Point((Game1.screenWidth / 2), 100), Game1.comicSans64, GameState.Options, "Options"),
                new Button(new Point((Game1.screenWidth / 2) - 100, 450), GameState.Options, "Menu", "Exit to Menu")

                // Game Elements

            };
        }

        #region Methods
        /// <summary>
        /// Updates every element
        /// </summary>
        public static void Update()
        {
            foreach (UIElement element in Elements)
            {
                if (element.State == Game1.GameState)
                {
                    element.Update();
                }
            }
        }

        /// <summary>
        /// Draws every element
        /// </summary>
        public static void Draw()
        {
            // Game1.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            foreach (UIElement element in Elements)
            {
                if (element.State == Game1.GameState)
                {
                    element.Draw();
                }
            }

            // Game1.spriteBatch.End();
        }
        #endregion
    }
}
