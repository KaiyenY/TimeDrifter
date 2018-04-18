using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _2dracer.Managers
{
    /// <summary>
    /// Makes changing options within the game easier
    /// </summary>
    public static class Options
    {
        #region Fields
        /// <summary>
        /// Controls some setting for the game window.
        /// </summary>
        public static GameWindow Window;

        /// <summary>
        /// Controls graphics settings
        /// </summary>
        public static GraphicsDeviceManager Graphics;

        /// <summary>
        /// Determines if the game is currently within fullscreen mode.
        /// </summary>
        public static bool Fullscreen;

        /// <summary>
        /// Determines the vertical resolution for the game window.
        /// </summary>
        public static int ScreenHeight;

        /// <summary>
        /// Determines the horizontal resolution for the game window.
        /// </summary>
        public static int ScreenWidth;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        static Options()
        {
            Fullscreen = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets up the default settings for when the game starts up.
        /// </summary>
        public static void InitializeSettings(GraphicsDeviceManager graphics, GameWindow window)
        {
            Graphics = graphics;
            Window = window;

            Graphics.IsFullScreen = Fullscreen;
            // Graphics.PreferredBackBufferHeight = ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 3 / 4;
            // Graphics.PreferredBackBufferWidth = ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 3 / 4;
            Graphics.PreferredBackBufferHeight = ScreenHeight = 720;
            Graphics.PreferredBackBufferWidth = ScreenWidth = 1280;

            Window.Title = "Time Drifter Deluxe";
        }

        public static void ToggleFullscreen()
        {
            if (Fullscreen)
            {
                // Graphics.PreferredBackBufferHeight = ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 3 / 4;
                // Graphics.PreferredBackBufferWidth = ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 3 / 4;
                Graphics.PreferredBackBufferHeight = ScreenHeight = 720;
                Graphics.PreferredBackBufferWidth = ScreenWidth = 1280;
            }
            else
            {
                ScreenHeight = Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                ScreenWidth = Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            }

            Fullscreen = Graphics.IsFullScreen = !Graphics.IsFullScreen;
            Graphics.ApplyChanges();

            UIManager.RefreshList();
        }
        #endregion
    }
}
