using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2dracer.Managers;

namespace _2dracer.UI
{
    /// <summary>
    /// Creates an element used in the interface.
    /// </summary>
    public abstract class UIElement
    {
        #region Fields
        /// <summary>
        /// Holds the current color of this element.
        /// </summary>
        protected Color color;

        /// <summary>
        /// Determines the size and location of the element on the screen.
        /// </summary>
        protected Rectangle rect;

        /// <summary>
        /// Stores the font of this element.
        /// </summary>
        protected SpriteFont font;

        /// <summary>
        /// Stores the sprite of this element.
        /// </summary>
        protected Texture2D sprite;

        /// <summary>
        /// Determines the position of the text within this element.
        /// </summary>
        protected Vector2 textPosition;

        /// <summary>
        /// Determines if this element is updating / drawing.
        /// </summary>
        protected bool enabled;

        /// <summary>
        /// Determines the rotation of this element.
        /// </summary>
        protected float rotation;

        /// <summary>
        /// Determines the size of the text when it draws.
        /// </summary>
        protected float textScale;

        /// <summary>
        /// Gives a unique name to this element.
        /// </summary>
        protected string name;

        /// <summary>
        /// Stores the text of this element.
        /// </summary>
        protected string text;
        #endregion

        #region Properties
        /// <summary>
        /// Returns the location of this element.
        /// </summary>
        public Point Location { get { return rect.Location; } }

        /// <summary>
        /// Returns the size of this element.
        /// </summary>
        public Point Size { get { return rect.Size; } }

        /// <summary>
        /// Determines if this element is updating / drawing.
        /// </summary>
        public bool Enabled { get { return enabled; } set { enabled = value; } }

        /// <summary>
        /// Gives a unique name to this element.
        /// </summary>
        public string Name { get { return name; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="UIElement"/> instance.
        /// </summary>
        /// <param name="location">Defines the position of this <see cref="UIElement"/> on the screen.</param>
        /// <param name="size">Defines the size of this <see cref="UIElement"/> on the screen.</param>
        /// <param name="sprite">The sprite of this <see cref="UIElement"/>.</param>
        /// <param name="enabled">Determines if this <see cref="UIElement"/> draws / updates.</param>
        /// <param name="name">The name of this <see cref="UIElement"/>.</param>
        /// <param name="text">The text within this <see cref="UIElement"/>.</param>
        public UIElement(Point location, Point size, Texture2D sprite, bool enabled, float rotation, string name, string text)
        {
            rect = new Rectangle(location, size);
            this.sprite = sprite;
            this.enabled = enabled;
            this.rotation = rotation;
            this.name = name;
            this.text = text;

            color = Color.White;

            if (text != null)
            {
                font = LoadManager.Fonts["Connection"];

                if (size != Point.Zero)
                {
                    textScale = (rect.Height - Options.ScreenHeight / 24) / font.MeasureString(text).Y;

                    textPosition = new Vector2(rect.X + (rect.Width - (font.MeasureString(text).X * textScale)) / 2,
                        rect.Y + (rect.Height - (font.MeasureString(text).Y * textScale)) / 2);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="UIElement"/> instance.
        /// </summary>
        /// <param name="location">Defines the position of this <see cref="UIElement"/> on the screen.</param>
        /// <param name="size">Defines the size of this <see cref="UIElement"/> on the screen.</param>
        /// <param name="sprite">The sprite of this <see cref="UIElement"/>.</param>
        /// <param name="enabled">Determines if this <see cref="UIElement"/> draws / updates.</param>
        /// <param name="name">The name of this <see cref="UIElement"/>.</param>
        public UIElement(Point location, Point size, Texture2D sprite, bool enabled, float rotation, string name) 
            : this(location, size, sprite, enabled, rotation, name, null) { }

        /// <summary>
        /// Creates a new <see cref="UIElement"/> instance.
        /// </summary>
        /// <param name="location">Defines the position of this <see cref="UIElement"/> on the screen.</param>
        /// <param name="enabled">Determines if this <see cref="UIElement"/> draws / updates.</param>
        /// <param name="name">The name of this <see cref="UIElement"/>.</param>
        /// <param name="text">The text within this <see cref="UIElement"/>.</param>
        public UIElement(Point location, bool enabled, string name, string text)
            : this(location, Point.Zero, null, enabled, 0.0f, name, text) { }
        #endregion

        #region Methods
        /// <summary>
        /// Draws this element onto the screen.
        /// </summary>
        public virtual void Draw()
        {
            if (enabled)
            {
                if (sprite != null)
                {
                    Game1.spriteBatch.Draw(
                        sprite,
                        rect,
                        new Rectangle(Point.Zero, new Point(sprite.Width, sprite.Height)),
                        color,
                        rotation,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0.9f);
                }
                
                if (text != null)
                {
                    Game1.spriteBatch.DrawString(
                        font, 
                        text, 
                        textPosition, 
                        Color.White, 
                        rotation,
                        Vector2.Zero,
                        textScale, 
                        SpriteEffects.None, 
                        1.0f);
                }
            }
        }

        /// <summary>
        /// Updates the logic behind this element.
        /// </summary>
        public virtual void Update() { }
        #endregion
    }
}