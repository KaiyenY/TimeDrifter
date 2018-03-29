using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dracer
{
    public class Player : Mover
    {
        // Fields
        private Turret turret;

        public static bool slowMo = false;

        // Properties
        public double TimeJuice { get; private set; }
        public int Health { get; private set; }

        // Constructor
        public Player(Texture2D sprite, Texture2D bulletSprite, Texture2D turretSprite, Vector2 position) 
            : base(new GameObject(position, 0, sprite, new Vector2(64, 128)), 
                  new Vector2(0,0), new Vector2(0,0), 0, 0, 1)
        {
            turret = new Turret(turretSprite, bulletSprite);
            Health = 100;
            TimeJuice = 0;
        }

        // Methods
        public override void Update()
        {
            float xAxis = Input.GetAxisRaw(Axis.X);

            // turn car
            AddTorque(xAxis * 1);
            angularVelocity *= 0.99f;


            // move car
            float yAxis = Input.GetAxisRaw(Axis.Y);
            float horsepower = yAxis * 300.0f;

            Vector2 force = new Vector2();
            force.X += (float)Math.Cos(rotation) * horsepower;
            force.Y += (float)Math.Sin(rotation) * horsepower;
            AddForce(force);

            // sliding friction?
            AddForce(velocity * -1);

            if (Input.KeyTap(Keys.P))
                slowMo = !slowMo;

            if (!slowMo && TimeJuice < 10)
                TimeJuice += Game1.gameTime.ElapsedGameTime.TotalMilliseconds/1000;

            if (slowMo)
                TimeJuice -= Game1.gameTime.ElapsedGameTime.TotalMilliseconds / 500;

            if (TimeJuice <= 0)
            {
                TimeJuice = 0;
                slowMo = false;
            }
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
    }
}

// Niko Procopi
