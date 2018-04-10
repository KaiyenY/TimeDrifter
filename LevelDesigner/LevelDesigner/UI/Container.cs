using LevelDesigner.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LevelDesigner.UI
{
    /// <summary>
    /// Creates a container for elements
    /// </summary>
    public class Container
    {
        #region Fields
        private Rectangle rect;
        private Texture2D sprite;
        #endregion

        #region Properties
        public bool Enabled { get; set; }
        public Point Position { get { return rect.Location; } }
        public Point Size { get { return rect.Size; } }
        #endregion

        #region Constructor
        public Container(Rectangle rect, bool enabled)
        {
            this.rect = rect;
            Enabled = enabled;
            sprite = Designer.square;
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Enabled)
            {
                spriteBatch.Draw(sprite, rect, Color.White);
            }
        }

        public void MouseClicked()
        {
            if (Input.MouseReleased(MouseButton.Left))
            {
                
            }
        }
        #endregion
    }
}
