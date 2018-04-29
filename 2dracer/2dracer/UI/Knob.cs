using Microsoft.Xna.Framework;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates an element that slides along a slider.
    /// </summary>
    public class Knob : UIElement
    {
        #region Fields
        /// <summary>
        /// The maximum x position this knob can go to.
        /// </summary>
        private float xMax;

        /// <summary>
        /// The minimum x position this knob can go to.
        /// </summary>
        private float xMin;
        #endregion

        #region Constructor
        public Knob(Point location, bool enabled, float xMax, float xMin, string name)
            : base(location, new Point(Options.ScreenHeight / 14), LoadManager.Sprites["Knob"], enabled, 0.0f, name)
        {
            this.xMax = xMax;
            this.xMin = xMin;
        }
        #endregion

        #region Methods
        #endregion
    }
}
