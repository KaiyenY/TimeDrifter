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
            : base(new GameObject(position, 0, sprite, new Vector2(64, 128)), 
                  new Vector2(0,0), new Vector2(0,0), 0, 0, 1)
        {
            turret = new Turret(turretSprite, bulletSprite);
            font = s;
        }

        // Methods
        public override void Update()
        {
            // Please move some of this code into methods
            // instead of having it all in here

            float xAxis = Input.GetAxisRaw(Axis.X);
            float yAxis = Input.GetAxisRaw(Axis.Y);

            // turn car
            AddTorque(xAxis * 0.4f);
            if(xAxis == 0) AddTorque(angularAccel * -1);

            // move car
            float horsepower = yAxis * 30.0f;

            Vector2 force = new Vector2();
            force.X += (float)Math.Cos(rotation) * horsepower;
            force.Y += (float)Math.Sin(rotation) * horsepower;

            AddForce(force);
            if(yAxis == 0) AddForce(velocity * -1);

            if (timeJuice < 10)
                timeJuice += Game1.gameTime.ElapsedGameTime.TotalMilliseconds/1000;

            // Update turret
            turret.Update(position);
            base.Update();
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
