using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    /// <summary>
    /// Controls UI elements
    /// </summary>
    public class UIElement
    {
        #region Properties
        public Color Color { get; protected set; }                  // Color of the element sprite
        public Rectangle Rect { get; protected set; }               // Position of element
        public SpriteFont Font { get; set; }                        // The font of the element
        public Texture2D Sprite { get; set; }                       // Sprite of element
        public GameState State { get; protected set; }              // The state this element will be updated/drawn in
        public float Rotation { get; protected set; }               // Rotation of element
        public string SpritePath { get; protected set; }            // Path to the sprite of element
        public string Tag { get; protected set; }                   // Additional information on element
        public string Text { get; protected set; }                  // The text of this element
        #endregion

        #region Constructors
        public UIElement(Color color, Rectangle rect, SpriteFont font, GameState state, float rotation, string spritePath, string tag, string text)
        {
            // TODO : Take the position of the center of the element

            Color = color;
            Rect = rect;
            Font = font;
            State = state;
            Rotation = rotation;
            SpritePath = spritePath;
            Tag = tag;
            Text = text;
        }
        
        public UIElement(Rectangle rect, SpriteFont font, GameState state, string spritePath, string tag, string text)
            : this(Color.White, rect, font, state, 0, spritePath, tag, text) { }

        public UIElement(Rectangle rect, GameState state, string spritePath, string tag, string text)
            : this(rect, null, state, spritePath, tag, text) { }

        public UIElement(Rectangle rect, GameState state, string tag, string text)
            : this(rect, state, "Textures/Square", tag, text) { }

        public UIElement(Rectangle rect, GameState state, float rotation, string spritePath, string tag)
            : this(Color.White, rect, null, state, rotation, spritePath, tag, null) { }

        /// <summary>
        /// Draws text to screen, automatically centers it
        /// </summary>
        public UIElement(Point position, SpriteFont font, GameState state, string text)
            : this(new Rectangle(), font, state, null, null, text)
        {
            Color = Color.Transparent;

            Point size = new Point((int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
            position = new Point(position.X - (size.X / 2), position.Y - (size.Y / 2));

            Rect = new Rectangle(position, size);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Update element on screen
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Draw element to the screen
        /// </summary>
        public virtual void Draw()
        {
            if (Sprite != null)
            {
                Game1.spriteBatch.Draw(Sprite, Rect, Color);
            }

            if (Text != null)
            {
                Game1.spriteBatch.DrawString(
                    Font,
                    Text,
                    new Vector2(
                        Rect.X + (Rect.Width / 2) - (int)Font.MeasureString(Text).X / 2,
                        Rect.Y + (Rect.Height / 2) - (int)Font.MeasureString(Text).Y / 2),
                    Color.White);
            }
        }
        #endregion
    }
}

// Ruben && Genoah