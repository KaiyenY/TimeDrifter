using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates a button element
    /// </summary>
    public class Button : UIElement
    {
        #region Constructors
        public Button(Color color, Point position, SpriteFont font, GameState state, float rotation, string tag, string text)
            : base(color, new Rectangle(position, new Point(200, 75)), font, state, rotation, "Textures/UI/Button", tag, text) { }

        public Button(Point position, SpriteFont font, GameState state, string tag, string text)
            : this(Color.White, position, font, state, 0, tag, text) { }

        public Button(Point position, GameState state, string tag, string text)
            : this(position, Game1.comicSans, state, tag, text) { }
        #endregion

        #region Methods
        /// <summary>
        /// Update the button
        /// </summary>
        public override void Update()
        {
            if (ButtonClick())
            {
                switch (Tag)
                {
                    case "Game":
                        Game1.GameState = GameState.Game;
                        break;

                    case "Menu":
                        Game1.GameState = GameState.Menu;
                        break;

                    case "Options":
                        Game1.GameState = GameState.Options;
                        break;

                    case "Pause":
                        Game1.GameState = GameState.Pause;
                        break;

                    case "Exit":
                        Program.game.Exit();
                        break;

                    default:
                        throw new Exception("Error : Is button.Name wrong?");
                }
            }
        }

        /// <summary>
        /// Draw the button
        /// </summary>
        public override void Draw()
        {
            base.Draw();
        }

        /// <summary>
        /// Detects if button is clicked
        /// </summary>
        public bool ButtonClick()
        {
            return ButtonHover() && Input.MouseReleased(MouseButton.Left);
        }

        /// <summary>
        /// Detects if mouse is over the button
        /// </summary>
        public bool ButtonHover()
        {
            if (Rect.Contains(Input.MousePos()))
            {

                if (Input.MouseHold(MouseButton.Left))
                {
                    // Mouse is held on it
                    Color = Color.DarkRed;
                }
                else
                {
                    // Mouse is hovering over
                    Color = Color.Red;
                }

                return true;
            }
            else
            {
                // Mouse is not hovering over it
                Color = Color.White;
                return false;
            }
        }

        /// <summary>
        /// Determines what happens to a state button
        /// </summary>
        public void StateButton()
        {
            
        }
        #endregion
    }
}

// - Genoah Martinelli