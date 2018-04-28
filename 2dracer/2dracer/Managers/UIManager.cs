using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _2dracer.Managers;

using _2dracer.UI;

namespace _2dracer.Managers
{
    /// <summary>
    /// Controls the user interface
    /// </summary>
    public static class UIManager
    {
        #region Fields
        private static GameState prevState;
        private static GameState prevOptionsState;
        #endregion

        #region Properties
        private static List<UIElement> Elements { get; set; }
        #endregion

        #region Constructor
        static UIManager()
        {
            RefreshList();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates every element
        /// </summary>
        public static void Update()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].Update();

                if (Game1.GameState == GameState.Game)
                {
                    if (Player.slowMo && Elements[i].Name == "timeEffect")
                    {
                        Elements[i].Enabled = true;
                    }
                    else if (!Player.slowMo && Elements[i].Name == "timeEffect")
                    {
                        Elements[i].Enabled = false;
                    }
                }
            }

            if (prevState != Game1.GameState)
            {
                RefreshList();
            }
            
            prevState = Game1.GameState;
        }

        /// <summary>
        /// Draws every element.
        /// </summary>
        public static void Draw()
        {
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None);

            foreach (UIElement element in Elements)
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
                    Elements = new List<UIElement>
                    {
                        new ImageElement(new Point(0, 0), new Point((Options.ScreenWidth / 6)),
                            LoadManager.Sprites["HealthGauge"], true, 0.0f, "healthGauge"),

                        new ImageElement(new Point((Options.ScreenWidth * 5 / 6), 0), new Point((Options.ScreenWidth / 6)),
                            LoadManager.Sprites["TimeGauge"], true, 0.0f, "timeGauge"),

                        // Add score odometer

                        new ImageElement(Point.Zero, new Point(Options.ScreenWidth, Options.ScreenHeight), LoadManager.Sprites["TimeEffect"],
                            false, 0.0f, "timeEffect")
                    };

                    Player.CreateHUD();
                    break;

                case GameState.Menu:
                    Elements = new List<UIElement>
                    {
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight / 12)), 
                            true, 1.0f, "title", "Time Drifter Deluxe"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight / 3)),
                            true, "playButton", "Play"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight / 2)),
                            true, "optionsButton", "Options"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 2 / 3)),
                            true, "instructionsButton", "Instructions"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 5 / 6)),
                            true, "exitButton", "Exit")
                    };
                    break;

                case GameState.Instructions:
                    Elements = new List<UIElement>
                    {
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight / 56)), 
                            true, 1.0f, "title", "Instructions"),

                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight / 7)), 
                            true, 0.5f, "i1", "Press W for gas and Space for break."),
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight * 2 / 7)), 
                            true, 0.5f, "i2", "Press A and D to steer."),
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight * 3 / 7)), 
                            true, 0.5f, "i3", "Press E to slow down time."),
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight * 4 / 7)), 
                            true, 0.5f, "i4", "Move the mouse to aim."),
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight * 5 / 7)), 
                            true, 0.5f, "i5", "Click to shoot."),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 5 / 6)), true, "menuButton", "Back"),
                    };
                    break;

                case GameState.Options:
                    Elements = new List<UIElement>
                    {
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight / 12)), 
                        true, 1.0f, "optionsTitle", "Options"),

                        new TextElement(new Point((Options.ScreenWidth * 5 / 24), (Options.ScreenHeight / 3)),
                            true, 0.5f, "masterVolume", "Master Volume"),
                        new Slider(new Point((Options.ScreenWidth * 15 / 24) - (Options.ScreenWidth / 4),
                            (Options.ScreenHeight / 3)), true, "masterVolume"),

                        new TextElement(new Point((Options.ScreenWidth * 5 / 24), (Options.ScreenHeight / 2)),
                            true, 0.5f, "musicVolume", "Music Volume"),
                        new Slider(new Point((Options.ScreenWidth * 15 / 24) - (Options.ScreenWidth / 4),
                            (Options.ScreenHeight / 2)), true, "musicVolume"),

                        new TextElement(new Point((Options.ScreenWidth * 5 / 24), (Options.ScreenHeight * 2 / 3)),
                            true, 0.5f, "soundVolume", "Sound Volume"),
                        new Slider(new Point((Options.ScreenWidth * 15 / 24) - (Options.ScreenWidth / 4),
                            (Options.ScreenHeight * 2 / 3)), true, "soundVolume"),

                        new Button(new Point((Options.ScreenWidth / 6) - (Options.ScreenWidth / 8),
                            Options.ScreenHeight * 5 / 6), true, "fullScreen", "Fullscreen"),
                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 5 / 6)), true, "muteButton", "Mute"),
                        new Button(new Point((Options.ScreenWidth * 5 / 6) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 5 / 6)), true, "backButton", "Back")
                    };
                    break;

                case GameState.Pause:
                    Elements = new List<UIElement>
                    {
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight / 12)),
                            true, 1.0f, "pauseTitle", "Pause"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight / 3)), true, "resumeButton", "Resume"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight / 2)), true, "optionsButton", "Options"),

                        new Button(new Point((Options.ScreenWidth / 2) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 2 / 3)), true, "menuButton", "Exit to Menu")
                    };
                    break;

                case GameState.GameOver:
                    Elements = new List<UIElement>
                    {
                        new TextElement(new Point((Options.ScreenWidth / 2), (Options.ScreenHeight / 12)),
                            true, 1.0f, "title", "Game Over"),

                        new TextElement(new Point(400, 200), true, 0.25f, "playerScore", "Score : "),

                        new TextElement(new Point(400, 300), true, 0.25f, "playerScore", "High Scores : "),

                        new Button(new Point((Options.ScreenWidth / 4) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 5 / 6)), true, "menuButton", "Exit to Menu"),

                        new Button(new Point((Options.ScreenWidth * 3 / 4) - (Options.ScreenWidth / 8),
                            (Options.ScreenHeight * 5 / 6)), true, "playButton", "Play Again")
                    };

                    StreamReader sr;
                    sr = new StreamReader(@"..\..\..\..\Content\Leaderboard.txt");

                    for (int i = 1; i < 6; i++)
                    {
                        // Elements.Add(new Element(new Vector2(400, 300 + 30*i), 0.25f, "playerScore", "#" + i + ": " + sr.ReadLine()));
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
                foreach (UIElement element in Elements)
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
                        Audio.ToggleMute();
                        break;

                    case "instructionsButton":
                        Elements.Clear();
                        Game1.GameState = GameState.Instructions;
                        RefreshList();
                        break;

                    case "optionsButton":
                        prevOptionsState = Game1.GameState;
                        Elements.Clear();
                        Game1.GameState = GameState.Options;
                        RefreshList();
                        break;

                    case "backButton":
                        Elements.Clear();

                        if (prevOptionsState == GameState.Pause)
                        {
                            Game1.GameState = GameState.Pause;
                        }
                        else if (prevOptionsState == GameState.Menu)
                        {
                            GameMaster.ClearAll();
                            Game1.GameState = GameState.Menu;
                        }

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
        /// Adds an <see cref="UIElement"/> instance into the list.
        /// </summary>
        public static void Add(UIElement element)
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