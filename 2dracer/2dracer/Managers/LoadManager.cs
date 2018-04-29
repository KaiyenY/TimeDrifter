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
                { "Ambience01", Load<SoundEffect>("Audio/Sound Effects/Ambience01") },
                { "Ambience02", Load<SoundEffect>("Audio/Sound Effects/Ambience02") },
                { "Click", Load<SoundEffect>("Audio/Sound Effects/ButtonClick") },
                { "Explosion", Load<SoundEffect>("Audio/Sound Effects/Explosion") },
                { "GameOver", Load<SoundEffect>("Audio/Sound Effects/GameOver") },
                { "Gunshot", Load<SoundEffect>("Audio/Sound Effects/GunFire") },
                { "SlowMotion", Load<SoundEffect>("Audio/Sound Effects/SlowMotion") }
            };
        }

        /// <summary>
        /// Loads all of the sprites that will be used in the game.
        /// </summary>
        private static void LoadSprites()
        {
            Sprites = new Dictionary<string, Texture2D>
            {
                { "Building1", Load<Texture2D>("Textures/Buildings/Building0") },
                { "Building2", Load<Texture2D>("Textures/Buildings/Building1") },
                { "Building3", Load<Texture2D>("Textures/Buildings/Building2") },
                { "Building4", Load<Texture2D>("Textures/Buildings/Building3") },
                { "Bullet", Load<Texture2D>("Textures/Bullet") },
                { "Button", Load<Texture2D>("Textures/UI/Button") },
                { "Cop", Load<Texture2D>("Textures/Cop") },
                { "CornerRoad", Load<Texture2D>("Textures/Tiles/CornerRoad") },
                { "FIntersection", Load<Texture2D>("Textures/Tiles/FourWayIntersection") },
                { "Grass", Load<Texture2D>("Textures/Tiles/Grass") },
                { "HealthGauge", Load<Texture2D>("Textures/UI/HealthGauge") },
                { "Knob", Load<Texture2D>("Textures/UI/Knob") },
                { "MenuBackground", Load<Texture2D>("Textures/MenuBackground") },
                { "Needle", Load<Texture2D>("Textures/UI/Needle") },
                { "RedCar", Load<Texture2D>("Textures/RedCar") },
                { "Roof", Load<Texture2D>("Textures/Tiles/Roof") },
                { "Slider", Load<Texture2D>("Textures/UI/Slider") },
                { "Square", Load<Texture2D>("Textures/Square") },
                { "StraightRoad", Load<Texture2D>("Textures/Tiles/StraightRoad") },
                { "TimeEffect", Load<Texture2D>("Textures/UI/TimeEffect") },
                { "TimeGauge", Load<Texture2D>("Textures/UI/TimeGauge") },
                { "TIntersection", Load<Texture2D>("Textures/Tiles/ThreeWayIntersection") },
                { "Turret", Load<Texture2D>("Textures/Turret") }
            };
        }
        #endregion
    }
}

// -- Genoah Martinelli