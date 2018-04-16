using LevelDesigner.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LevelDesigner.UI
{
    /// <summary>
    /// Defines a <see cref="Button"/> UI element.
    /// </summary>
    public class Button
    {
        #region Fields
        private Color color;
        private Rectangle rect;
        private Texture2D sprite;
        #endregion

        #region Properties
        public Rectangle Rect { get { return rect; } }

        public bool Enabled { get; set; }
        #endregion

        #region Constructor
        public Button(Rectangle rect, Texture2D sprite, bool enabled)
        {
            color = Color.White;
            this.rect = rect;
            this.sprite = sprite;
            Enabled = enabled;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Draws this <see cref="Button"/>.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(sprite, rect, color);
            }
        }

        /// <summary>
        /// Determines if this <see cref="Button"/> is clicked.
        /// </summary>
        public bool IsClicked()
        {
            if (IsHovered())
            {
                if (Input.MouseHold(MouseButton.Left))
                {
                    color = Color.Gray;
                }

                if (Input.MouseReleased(MouseButton.Left))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if this <see cref="Button"/> is being hovered over.
        /// </summary>
        /// <returns>True if this <see cref="Button"/> is being hovered over.</returns>
        private bool IsHovered()
        {
            if (rect.Contains(Input.MousePos()))
            {
                color = Color.LightGray;
                return true;
            }
            else
            {
                color = Color.White;
                return false;
            }
        }
        #endregion
    }
}
