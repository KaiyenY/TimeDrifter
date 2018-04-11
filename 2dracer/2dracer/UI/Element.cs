using _2dracer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2dracer.UI
{
    public class Element
    {
        #region Fields
        protected Color color;
        protected Rectangle rect;
        protected SpriteFont font;
        protected Texture2D sprite;
        protected Vector2 textPosition;
        protected float textScale;
        protected string name;
        protected string text;
        #endregion

        #region Properties
        public Point Position { get { return rect.Location; } }
        public Point Size { get { return rect.Size; } }
        public string Name { get { return name; } }
        public string Text { get { return text; } set { text = value; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="Element"/> instance.
        /// </summary>
        /// <param name="rect">Defines where and what size this <see cref="Element"/> is.</param>
        /// <param name="font">The font of the text inside this <see cref="Element"/>.</param>
        /// <param name="sprite">The sprite of this <see cref="Element"/>.</param>
        /// <param name="name">The name of this <see cref="Element"/>.</param>
        /// <param name="text">The text inside of this <see cref="Element"/>.</param>
        public Element(Rectangle rect, Texture2D sprite, string name, string text)
        {
            color = Color.White;
            this.rect = rect;
            this.sprite = sprite;
            this.name = name;
            this.text = text;

            font = LoadManager.Fonts["Connection"];

            textScale = (rect.Height - 30) / font.MeasureString(text).Y;

            textPosition = new Vector2(rect.X + (rect.Width - (font.MeasureString(text).X * textScale)) / 2,
                rect.Y + (rect.Height - (font.MeasureString(text).Y * textScale)) / 2);
        }

        /// <summary>
        /// Creates a new <see cref="Element"/> instance.
        /// </summary>
        /// <param name="rect">Defines where and what size this <see cref="Element"/> is.</param>
        /// <param name="sprite">The sprite of this <see cref="Element"/>.</param>
        /// <param name="name">The name of this <see cref="Element"/>.</param>
        public Element(Rectangle rect, Texture2D sprite, string name)
        {
            color = Color.White;
            this.rect = rect;
            this.sprite = sprite;
            this.name = name;

            font = null;
            text = null;
            textPosition = Vector2.Zero;
        }

        /// <summary>
        /// Creates a new <see cref="Element"/> instance.
        /// </summary>
        /// <param name="font">The font of the text inside this <see cref="Element"/>.</param>
        /// <param name="textPosition">The position of the text.</param>
        /// <param name="size">The size of the text.</param>
        /// <param name="name">The name of this <see cref="Element"/>.</param>
        /// <param name="text">The text inside of this <see cref="Element"/>.</param>
        public Element(Vector2 textPosition, float size, string name, string text)
        {
            color = Color.White;
            this.textPosition = textPosition;
            textScale = size;
            this.name = name;
            this.text = text;

            font = LoadManager.Fonts["Connection"];
        
            rect = Rectangle.Empty;
            sprite = null;
        }
        #endregion

        #region Methods
        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            if (sprite != null)
            {
                Game1.spriteBatch.Draw(sprite, rect, color);
            }

            if (text != null)
            {
                Game1.spriteBatch.DrawString(
                    font, 
                    text, 
                    textPosition, 
                    Color.White,
                    0f,
                    Vector2.Zero,
                    textScale,
                    SpriteEffects.None,
                    1.0f);
            }
        }
        #endregion
    }
}
