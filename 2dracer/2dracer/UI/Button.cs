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
    public class Button : Element
    {
        #region Events
        public event EventHandler<EventArgs> Click;     // Determines what happens when this button is clicked
        #endregion

        #region Constructor
        public Button(Rectangle rect, Texture2D sprite, string name, string text)
            : base(rect, sprite, name, text)
        {

        }
        #endregion

        #region Methods
        public override void Update()
        {
            if (rect.Contains(Input.MousePos()))
            {
                color = Color.LightGray;

                if (Input.MouseHold(MouseButton.Left))
                {
                    color = Color.Gray;
                }

                if (Input.MouseReleased(MouseButton.Left))
                {
                    Click.Invoke(this, new EventArgs());
                    AudioManager.PlaySound("Click", 1.0f);
                }
            }
            else
            {
                color = Color.White;
            }
        }
        #endregion
    }
}

// - Genoah Martinelli