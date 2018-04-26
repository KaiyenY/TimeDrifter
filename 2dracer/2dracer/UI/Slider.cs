using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates an element that holds a knob for sliding.
    /// </summary>
    public class Slider : UIElement
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Slider(Point location, Texture2D sprite, bool enabled, string name)
            : base(location, new Point(Options.ScreenWidth / 4, Options.ScreenHeight / 10), LoadManager.Sprites["Slider"], enabled, 0.0f, name)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
