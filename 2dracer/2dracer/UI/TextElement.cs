using Microsoft.Xna.Framework;
using System;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Determines text element 
    /// </summary>
    public class TextElement : UIElement
    {
        #region Properties
        public string Text { get { return text; } set { text = value; } }
        #endregion

        #region Constructor
        public TextElement(Point location, bool enabled, float size, string name, string text)
            : base(location, enabled, name, text)
        {
            textScale = size;

            // textPosition = Vector2.Subtract(location.ToVector2(), font.MeasureString(text) * textScale / 2);
            textPosition = new Vector2(location.X - font.MeasureString(text).X * textScale / 2,
                location.Y);
        }
        #endregion
    }
}
