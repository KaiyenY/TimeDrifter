using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer
{
    public class GameObject
    {
        // fields
        protected Vector2 position;     // position of center in world space
        protected float rotation;       // rotation in degrees from +x axis (remember y is flipped)

        protected Texture2D sprite;     // static sprite
        protected string spritePath;    // Will hold path to sprite (helps when loading textures)
        protected Color color;          // color tint of the sprite
        protected Vector2 size;         // fixed standard size (width and height) of objects of this type
        protected Vector2 scale;        // the scaling factor for this particular object

        protected bool isEnabled;       // if this object should be updated and drawn

        // properties
        public Vector2 Position { get { return position; } }
        public float Rotation { get { return rotation; } }
        public Texture2D Sprite { get { return sprite; } set { sprite = value; } }
        public string SpritePath { get { return spritePath; } }
        public Color Color { get { return color; } }
        public Vector2 Size { get { return size; } }
        public Vector2 Scale { get { return scale; } }
        public bool IsEnabled { get { return isEnabled; } }

        // constructors
        public GameObject(GameObject g)
                   : this(g.Position, g.Rotation, g.SpritePath, g.Color, g.Size, g.Scale, g.IsEnabled) { }
        
        public GameObject(Vector2 position, float rotation, string spritePath, Color color, Vector2 size, Vector2 scale, bool startEnabled)
        {
            this.position = position;
            this.rotation = rotation;

            this.spritePath = spritePath;
            this.color = color;
            this.size = size;
            this.scale = scale;

            isEnabled = startEnabled;
        }

        public GameObject(Vector2 position, float rotation, string spritePath, Color color, Vector2 size, Vector2 scale)
                   : this(position, rotation, spritePath, color, size, scale, true) { }

        public GameObject(Vector2 position, float rotation, string spritePath, Vector2 size, Vector2 scale)
                   : this(position, rotation, spritePath, Color.White, size, scale) { }

        public GameObject(Vector2 position, float rotation, string spritePath, Vector2 size)
                   : this(position, rotation, spritePath, size, Vector2.One) { }

        public GameObject(Vector2 position, float rotation, Vector2 size)
                   : this(position, rotation, "Textures/Square", size) { }

        public GameObject(Vector2 size)
                   : this(Vector2.Zero, 0f, size) { }

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
            Vector2 appliedScale = new Vector2((size.X * scale.X) / sprite.Width, (size.Y * scale.Y) / sprite.Height);
            Vector2 origin = new Vector2((sprite.Width) / 2, (sprite.Height) / 2);
            Game1.spriteBatch.Draw(sprite, position, null, color, rotation, origin, appliedScale, SpriteEffects.None, 0f);
        }
    }
}

// Matthew Soriano