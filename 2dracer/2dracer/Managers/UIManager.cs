using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2dracer.Managers;

using _2dracer.UI;

namespace _2dracer.Managers
{
    /// <summary>
    /// Controls the user interface
    /// </summary>
    public static class UIManager
    {
        // Properties
        private static List<Element> Elements { get; set; }
        private static GameState prevState;

        // Constructor
        static UIManager()
        {
            RefreshList();
        }

        #region Methods
        /// <summary>
        /// Updates every element
        /// </summary>
        public static void Update()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].Update();
            }

            if (prevState != Game1.GameState)
            {
                RefreshList();
            }

            prevState = Game1.GameState;
        }

        /// <summary>
        /// Draws every element
        /// </summary>
        public static void Draw()
        {
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            foreach (Element element in Elements)
            {
                element.Draw();
            }

            Game1.spriteBatch.End();
        }

        /// <summary>
        /// Changes which UI <see cref="Element"/> instances load in which <see cref="GameState"/>.
        /// </summary>
        public static void RefreshList()
        {
            if (Elements != null)
            Elements.Clear();

            switch (Game1.GameState)
            {
                case GameState.Game:
                    Elements = new List<Element>();

                    Player.CreateHUD();
                    break;

                case GameState.Menu:
                    Elements = new List<Element>
                        {
                            new Element(new Vector2(50, 50), 1f, "title", "Time Drifter Deluxe"),

                            new Button(new Rectangle((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8), 
                            (Options.ScreenHeight / 3), 
                            (Options.ScreenWidth / 4),
                            (Options.ScreenHeight / 10)), 
                            LoadManager.Sprites["Button"], "playButton", "Play"),

                            new Button(new Rectangle((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8), 
                            (Options.ScreenHeight / 2), 
                            (Options.ScreenWidth / 4), 
                            (Options.ScreenHeight / 10)), 
                            LoadManager.Sprites["Button"], "optionsButton", "Options"),

                            new Button(new Rectangle((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8), 
                            (Options.ScreenHeight * 2 / 3), 
                            (Options.ScreenWidth / 4),
                            (Options.ScreenHeight / 10)), 
                            LoadManager.Sprites["Button"], "exitButton", "Exit")
                        };
                    break;

                case GameState.Options:
                    Elements = new List<Element>
                        {
                            new Element(new Vector2(50, 50), 1f, "optionsTitle", "Options"),

                            new Button(new Rectangle(
                                (Options.ScreenWidth / 4) - (Options.ScreenWidth / 8),
                                (Options.ScreenHeight / 3),
                                (Options.ScreenWidth / 4),
                                (Options.ScreenHeight / 10)),
                                LoadManager.Sprites["Button"], "fullScreen", "FullScreen"),

                            new Button(new Rectangle(
                                (Options.ScreenWidth * 3 / 4) - (Options.ScreenWidth / 8),
                                (Options.ScreenHeight / 3),
                                (Options.ScreenWidth / 4),
                                (Options.ScreenHeight / 10)),
                                LoadManager.Sprites["Button"], "muteButton", "Mute"),

                            new Button(new Rectangle(
                                (Options.ScreenWidth / 2) - (Options.ScreenWidth / 8), 
                                (Options.ScreenHeight * 5 / 6), 
                                (Options.ScreenWidth / 4), 
                                (Options.ScreenHeight / 10)), 
                                LoadManager.Sprites["Button"], "backButton", "Back")
                        };
                    break;

                case GameState.Pause:
                    Elements = new List<Element>
                        {
                            new Element(new Vector2(50, 50), 1f, "pauseTitle", "Pause"),

                            new Button(new Rectangle(
                                (Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                                (Options.ScreenHeight / 3), 
                                (Options.ScreenWidth / 4), 
                                (Options.ScreenHeight / 10)), 
                                LoadManager.Sprites["Button"], "resumeButton", "Resume"),

                            new Button(new Rectangle(
                                (Options.ScreenWidth / 2) - (Options.ScreenWidth / 8), 
                                (Options.ScreenHeight / 2), 
                                (Options.ScreenWidth / 4), 
                                (Options.ScreenHeight / 10)), 
                                LoadManager.Sprites["Button"], "menuButton", "Exit to Menu")
                        };
                    break;

                case GameState.GameOver:
                    Elements = new List<Element>
                        {
                            new Element(new Vector2(50, 50), 1f, "deathTitle", "Death"),
                            new Element(new Vector2(400, 200), 0.25f, "playerScore", "Score: "),
                            new Element(new Vector2(400, 300), 0.25f, "playerScore", "High Scores:"),
                            new Button(new Rectangle((Options.ScreenWidth / 2) - 200, 550, 400, 80), LoadManager.Sprites["Button"], "backButton", "Back")
                        };

                    StreamReader sr;
                    sr = new StreamReader(@"..\..\..\..\Content\Leaderboard.txt");

                    for (int i = 1; i < 6; i++)
                    {
                        Elements.Add(new Element(new Vector2(400, 300 + 30*i), 0.25f, "playerScore", "#" + i + ": " + sr.ReadLine()));
                    }

                    // Open Leaderboard txt for reading
                    // Make String array
                    // Close leaderboard

                    // if(score is better than top 5)
                    // Update leaderboard string array
                    // Open Leaderboard for writing 
                    // save leaderboard
                    // Close leaderboard

                    break;

                default:
                    throw new NotImplementedException("The current GameState is not supported by this method.");
            }

            if (Elements != null)
            {
                foreach (Element element in Elements)
                {
                    if (element is Button button)
                    {
                        button.Click += ButtonClick;
                    }
                }
            }
        }

        /// <summary>
        /// Determines what happens when a button is clicked.
        /// </summary>
        private static void ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Name)
                {
                    case "playButton":
                        Elements.Clear();
                        GameMaster.Start();
                        Game1.GameState = GameState.Game;
                        RefreshList();
                        break;

                    case "resumeButton":
                        Elements.Clear();
                        Game1.GameState = GameState.Game;
                        RefreshList();
                        break;

                    case "menuButton":
                        Elements.Clear();
                        Game1.GameState = GameState.Menu;
                        RefreshList();
                        break;

                    case "muteButton":
                        AudioManager.ToggleMute();
                        break;

                    case "optionsButton":
                        Elements.Clear();
                        Game1.GameState = GameState.Options;
                        RefreshList();
                        break;

                    case "backButton":
                        Elements.Clear();
                        GameMaster.ClearAll();
                        Game1.GameState = GameState.Menu;
                        RefreshList();
                        break;

                    case "exitButton":
                        Program.game.Exit();
                        break;

                    case "fullScreen":
                        Options.ToggleFullscreen();
                        break;
                }
            }
        }

        /// <summary>
        /// Adds an <see cref="Element"/> instance into the list.
        /// </summary>
        public static void Add(Element element)
        {
            if (element != null)
            {
                Elements.Add(element);
            }
            else
            {
                throw new Exception("You cannot add a null element into the list.");
            }
        }
        #endregion
    }
}
