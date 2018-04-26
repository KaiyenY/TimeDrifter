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
        private float minX;
        private float maxX;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Slider(Point location, bool enabled, string name)
            : base(location, new Point(Options.ScreenWidth / 2, Options.ScreenHeight / 10), LoadManager.Sprites["Slider"], enabled, 0.0f, name)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
