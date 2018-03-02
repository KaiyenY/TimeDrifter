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
    /// <summary>
    /// Creates the player object *FIX INHERITANCE*
    /// </summary>
    public class Player
    {
        // Fields
        private float moveSpeed;
        private float rotSpeed;

        private Texture2D c;
        public float posX;
        public float posY;

        private float dirX = 0;
        private float dirY = 0;
        private float angle = 0;

        // Properties
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        // Constructor
        public Player(Texture2D tex, float x, float y)
        {
            moveSpeed = 3f;
            rotSpeed = 2f;

            posX = x;
            posY = y;
            c = tex;
        }

        public void Update(Input input)
        {
            // Move the player
            input.MovePlayer(this, moveSpeed, rotSpeed);
        }

        public void Draw()
        {
            // Draw car
            Game1.spriteBatch.Draw(c,
                new Rectangle((int)posX, (int)posY, c.Width / 8, c.Height / 8),
                null,
                Color.White,
                (float)((angle + 90) * 3.14159 / 180),
                new Vector2(c.Width / 2, c.Height / 2),
                SpriteEffects.None, 0f);
        }
    }
}
