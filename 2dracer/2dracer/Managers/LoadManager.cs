using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        /// Stores all music in the game.
        /// </summary>
        public static Dictionary<string, Song> Music;

        /// <summary>
        /// Stores all the sounds in the game.
        /// </summary>
        public static Dictionary<string, SoundEffect> Sounds;

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
            LoadModels();
            LoadMusic();
            LoadSounds();
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
        /// Loads all models that will be used in the game.
        /// </summary>
        private static void LoadModels()
        {
            Models = new Dictionary<string, Model>
            {
                { "Cube", Load<Model>("Models/Cube") }      // Default cubic building
            };
        }

        /// <summary>
        /// Loads all music that will be used in the game.
        /// </summary>
        private static void LoadMusic()
        {
            Music = new Dictionary<string, Song>
            {
                { "ExtremeAction", Load<Song>("Audio/Tracks/ExtremeAction") },
                { "HappyRock", Load<Song>("Audio/Tracks/HappyRock") }
            };
        }

        /// <summary>
        /// Loads all sounds that will be used in the game.
        /// </summary>
        private static void LoadSounds()
        {
            Sounds = new Dictionary<string, SoundEffect>
            {
                { "Gunshot", Load<SoundEffect>("Audio/Sound Effects/GunFire") },
                { "Click", Load<SoundEffect>("Audio/Sound Effects/ButtonClick") }
            };
        }

        /// <summary>
        /// Loads all of the sprites that will be used in the game.
        /// </summary>
        private static void LoadSprites()
        {
            Sprites = new Dictionary<string, Texture2D>
            {
                { "Wheels", Load<Texture2D>("3D_Car/car/0000") },
                { "CarRed", Load<Texture2D>("3D_Car/skin00/0000") },
                { "CarGold", Load<Texture2D>("3D_Car/skin01/0000") },
                { "CarGray", Load<Texture2D>("3D_Car/skin02/0000") },
                { "CarPurple", Load<Texture2D>("3D_Car/skin03/0000") },
                { "CarGreen", Load<Texture2D>("3D_Car/skin04/0000") },
                { "CarOrange", Load<Texture2D>("3D_Car/skin05/0000") },
                { "CarYellow", Load<Texture2D>("3D_Car/skin06/0000") },
                { "CarBlack", Load<Texture2D>("3D_Car/skin07/0000") },
                { "Building1", Load<Texture2D>("Textures/Buildings/Building0") },
                { "Building2", Load<Texture2D>("Textures/Buildings/Building1") },
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