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
    class Player : Mover
    {
        // Fields
        private Turret turret;
        private SpriteFont font;
        private int health = 100;
        private double timeJuice = 0;

        // Constructor
        public Player(Texture2D sprite, Texture2D bulletSprite, Texture2D turretSprite, Vector2 position, SpriteFont s) 
            : base(new GameObject(position, 0, sprite, new Vector2(64, 128)))
        {
            turret = new Turret(turretSprite, bulletSprite);
            font = s;
        }

        // Methods
        public override void Update(GameTime gameTime)
        {
            // turn car
            rotation += Input.GetAxisRaw(Axis.X) * 0.04f;

            // move car
            float speed = Input.GetAxisRaw(Axis.Y) * 3;
            position.X += (float)Math.Cos(rotation) * speed;
            position.Y += (float)Math.Sin(rotation) * speed;

            if (timeJuice < 10)
                timeJuice += gameTime.ElapsedGameTime.TotalMilliseconds/1000;

            // Update turret
            turret.Update(gameTime, position);
        }

        public override void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.Draw();
            rotation -= (float)Math.PI / 2;

            // Draw turret
            turret.Draw();
        }

        public void DrawHUD()
        {
            Game1.spriteBatch.DrawString(font, "Health: " + health, new Vector2(50, 100), Color.White);
            Game1.spriteBatch.DrawString(font, "Time Juice: " + (int)timeJuice, new Vector2(250, 100), Color.White);
        }
    }
}

// Niko Procopi
