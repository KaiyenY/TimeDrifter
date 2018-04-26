using _2dracer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2dracer
{
    public class GameObject
    {
        // fields
        protected Vector2 position;     // position of center in world space
        protected float rotation;       // rotation in degrees from +x axis (remember y is flipped)

        protected Texture2D sprite;     // static sprite
        protected Color color;          // color tint of the sprite
        protected Vector2 origin;       // shifts the texture of this object with the given vector
        protected Vector2 size;         // fixed standard size (width and height) of objects of this type
        protected Vector2 scale;        // the scaling factor for this particular object
        protected float layerDepth;     // determines the drawing layer of this object

        protected bool isEnabled;       // if this object should be updated and drawn

        // properties
        public Vector2 Position { get { return position; } }
        public float Rotation { get { return rotation; } }
        public Texture2D Sprite { get { return sprite; } set { sprite = value; } }
        public Color Color { get { return color; } }
        public Vector2 Origin { get { return origin; } }
        public Vector2 Size { get { return size; } }
        public Vector2 Scale { get { return scale; } }
        public float LayerDepth { get { return layerDepth; } }
        public bool IsEnabled { get { return isEnabled; } }

        // constructors
        public GameObject(GameObject g)
                   : this(g.Position, g.Rotation, g.Sprite, g.Color, g.Size, g.Scale, g.LayerDepth, g.IsEnabled) { }
        
        public GameObject(Vector2 position, float rotation, Texture2D sprite, Color color, Vector2 size, Vector2 scale, float layerDepth, bool startEnabled)
        {
            this.position = position;
            this.rotation = rotation;

            this.sprite = sprite;
            this.color = color;
            this.size = size;
            this.scale = scale;
            this.layerDepth = layerDepth;

            origin = new Vector2(sprite.Width, sprite.Height) / 2;

            isEnabled = startEnabled;
        }

        public GameObject(Vector2 position, float rotation, Texture2D sprite, Color color, Vector2 size, Vector2 scale, float layerDepth)
                   : this(position, rotation, sprite, color, size, scale, layerDepth, true) { }

        public GameObject(Vector2 position, float rotation, Texture2D sprite, Vector2 size, Vector2 scale, float layerDepth)
                   : this(position, rotation, sprite, Color.White, size, scale, layerDepth) { }

        public GameObject(Vector2 position, float rotation, Texture2D sprite, Vector2 size, float layerDepth)
                   : this(position, rotation, sprite, size, Vector2.One, layerDepth) { }

        public GameObject(Vector2 position, float rotation, Vector2 size, float layerDepth)
                   : this(position, rotation, LoadManager.Sprites["Square"], size, layerDepth) { }

        public GameObject(Vector2 size)
                   : this(Vector2.Zero, 0f, size, 0f) { }

        // methods
        /// <summary>
        /// Updates logic for this game object every frame
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Draws this object's texture to the screen
        /// MUST call spriteBatch.Begin first
        /// </summary>
        public virtual void Draw()
        {
            origin = new Vector2(sprite.Width, sprite.Height) / 2;
            Vector2 appliedScale = new Vector2((size.X * scale.X) / sprite.Width, (size.Y * scale.Y) / sprite.Height);
            Game1.spriteBatch.Draw(sprite, position, null, color, rotation, origin, appliedScale, SpriteEffects.None, layerDepth);
        }
    }
}

// Matthew Soriano