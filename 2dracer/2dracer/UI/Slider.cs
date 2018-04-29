using Microsoft.Xna.Framework;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates an element that holds a knob for sliding.
    /// </summary>
    public class Slider : UIElement
    {
        #region Fields
        /// <summary>
        /// An element that slides along this one.
        /// </summary>
        private Knob knob;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Slider(Point location, bool enabled, string name)
            : base(location, new Point(Options.ScreenWidth / 2, Options.ScreenHeight / 10), LoadManager.Sprites["Slider"], enabled, 0.0f, name)
        {
            UIManager.Add(knob = new Knob(new Point(location.X + 10, location.Y), enabled, location.X + 500f, location.X, name + "Knob"));
        }
        #endregion

        #region Methods
        #endregion
    }
}
