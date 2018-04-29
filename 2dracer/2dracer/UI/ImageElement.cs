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
            switch (name)
            {
                case "healthNeedle":
                    origin = new Vector2(size.X / 64, size.Y * 9 / 48);
                    break;

                case "timeNeedle":
                    origin = new Vector2(size.X / 64, size.Y * 9 / 48);
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Methods
        public override void Update()
        {
            switch (name)
            {
                case "healthNeedle":
                    rotation = -MathHelper.Pi * Player.Health / 100 - MathHelper.PiOver4 * 5;
                    break;

                case "timeNeedle":
                    rotation = MathHelper.Pi * (float)Player.TimeJuice / 10 + MathHelper.PiOver4;
                    break;
            }
        }
        #endregion
    }
}
