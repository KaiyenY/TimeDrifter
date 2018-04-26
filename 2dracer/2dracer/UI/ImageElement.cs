using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates an image on the screen.
    /// </summary>
    public class ImageElement : UIElement
    {
        #region Constructor
        public ImageElement(Point location, Point size, Texture2D sprite, bool enabled, float rotation, string name)
            : base(location, size, sprite, enabled, rotation, name)
        {

        }
        #endregion

        #region Methods
        public override void Update()
        {
            switch (name)
            {
                case "healthNeedle":
                    break;

                case "timeNeedle":
                    break;
            }
        }
        #endregion
    }
}
