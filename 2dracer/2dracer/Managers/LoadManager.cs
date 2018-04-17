using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _2dracer.Managers
{
    public static class LoadManager
    {
        #region Properties
        /// <summary>
        /// Loads in assets from the pipeline.
        /// </summary>
        private static ContentManager content;

        /// <summary>
        /// Stores all fonts in the game.
        /// </summary>
        public static Dictionary<string, SpriteFont> Fonts;

        /// <summary>
        /// Stores all models in the game.
        /// </summary>
        public static Dictionary<string, Model> Models;

        /// <summary>
        /// Stores all sprites in the game.
        /// </summary>
        public static Dictionary<string, Texture2D> Sprites;
        #endregion

        #region Methods
        /// <summary>
        /// Uses the content manager to load in assets.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load in.</typeparam>
        /// <param name="assetPath">The path of the asset being loaded in.</param>
        /// <returns>The asset if it loaded in successfully.</returns>
        private static T Load<T>(string assetPath)
        {
            return content.Load<T>(assetPath);
        }

        /// <summary>
        /// Calls every load method to populate the dictionaries.
        /// </summary>
        /// <param name="content">The <see cref="ContentManager"/> from Game1.</param>
        public static void LoadContent(ContentManager content)
        {
            LoadManager.content = content;
            LoadFonts();
            LoadSprites();
        }

        /// <summary>
        /// Loads all of the fonts that will be used in the game.
        /// </summary>
        private static void LoadFonts()
        {
            Fonts = new Dictionary<string, SpriteFont>
            {
                { "Connection", Load<SpriteFont>("Fonts/ConnectionSerif") }
            };
        }

        /// <summary>
        /// Loads all of the sprites that will be used in the game.
        /// </summary>
        private static void LoadSprites()
        {
            Sprites = new Dictionary<string, Texture2D>
            {
                { "Bullet", Load<Texture2D>("Textures/Bullet") },
                { "Button", Load<Texture2D>("Textures/UI/Button") },
                { "Cop", Load<Texture2D>("Textures/Cop") },
                { "CornerRoad", Load<Texture2D>("Textures/Tiles/CornerRoad") },
                { "FIntersection", Load<Texture2D>("Textures/Tiles/FourWayIntersection") },
                { "Grass", Load<Texture2D>("Textures/Tiles/Grass") },
                { "RedCar", Load<Texture2D>("Textures/RedCar") },
                { "Roof", Load<Texture2D>("Textures/Tiles/Roof") },
                { "Square", Load<Texture2D>("Textures/Square") },
                { "StraightRoad", Load<Texture2D>("Textures/Tiles/StraightRoad") },
                { "TIntersection", Load<Texture2D>("Textures/Tiles/ThreeWayIntersection") },
                { "Turret", Load<Texture2D>("Textures/Turret") }
            };
        }
        #endregion
    }
}

// -- Genoah Martinelli