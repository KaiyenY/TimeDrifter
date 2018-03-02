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
    /// Creates the player object
    /// </summary>
    public class Player : Mover
    {
        // Fields
        private Turret turret;

        private float angle;
        private float moveSpeed;
        private float rotSpeed;
        private float posX;
        private float posY;

        // Properties
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        public float PosX
        {
            get { return posX; }
            set { posX = value; }
        }
        public float PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        // Constructor
        public Player(Texture2D sprite, Texture2D gun, Texture2D bullet, float x, float y)
        {
            angle = 0;
            moveSpeed = 3f;
            rotSpeed = 2f;
            posX = x;
            posY = y;
            this.sprite = sprite;

            turret = new Turret(gun, bullet);
        }

        // Methods
        public void Update(GameTime gameTime, Input input)
        {
            // Move the player
            input.MovePlayer(this, moveSpeed, rotSpeed);

            // Update turret ( maybe put in update manager )
            turret.Update(gameTime, posX, posY);
        }

        public override void Draw()
        {
            Game1.spriteBatch.Draw(sprite,
                new Rectangle((int)posX, (int)posY, sprite.Width / 8, sprite.Height / 8),
                null,
                Color.White,
                MathHelper.ToRadians(angle + 90),
                new Vector2(sprite.Width / 2, sprite.Height / 2),
                SpriteEffects.None, 0f);


            turret.Draw();
            base.Draw();
        }
    }
}
