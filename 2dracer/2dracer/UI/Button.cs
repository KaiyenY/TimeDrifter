using Microsoft.Xna.Framework;
using System;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates a button element
    /// </summary>
    public class Button : UIElement
    {
        #region Events
        /// <summary>
        /// Invokes when this button is clicked.
        /// </summary>
        public event EventHandler<EventArgs> Click;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new button instance.
        /// </summary>
        /// <param name="location">Determines where this element is on the screen.</param>
        /// <param name="enabled">Determines if this element draws / updates.</param>
        /// <param name="name">Gives a unique name to this element.</param>
        /// <param name="text">Stores the text within this element.</param>
        public Button(Point location, bool enabled, string name, string text)
            : base(location, new Point(Options.ScreenWidth / 4, Options.ScreenHeight / 10), LoadManager.Sprites["Button"], enabled, 0.0f, name, text)
        {

        }
        #endregion

        #region Methods
        public override void Update()
        {
            DetectClick();
        }

        /// <summary>
        /// Detects if this element is clicked.
        /// </summary>
        private void DetectClick()
        {
            if (Hover(Input.MousePos()))
            {
                if (Input.MouseHold(MouseButton.Left))
                {
                    color = Color.DarkRed;
                }
                
                if (Input.MouseReleased(MouseButton.Left))
                {
                    AudioManager.PlaySound("Click");
                    Click.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Determines if a position is hovering over this button.
        /// </summary>
        private bool Hover(Point pos)
        {
            if (rect.Contains(pos))
            {
                color = Color.Red;
                return true;
            }

            color = Color.White;
            return false;
        }
        #endregion
    }
}

// - Genoah Martinelli