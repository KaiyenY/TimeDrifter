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

            textPosition = location.ToVector2();
        }
        #endregion
    }
}
