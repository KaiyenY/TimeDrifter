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

        // Constructor
        public Player(Texture2D sprite, Texture2D bulletSprite, Texture2D turretSprite, Vector2 position) 
            : base(new GameObject(position, 0, sprite, new Vector2(64, 128)))
        {
            turret = new Turret(turretSprite, bulletSprite);
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


            // Update turret
            turret.Update(gameTime, position);
        }

        public override void Draw()
        {
            rotation += (float)Math.PI/2;
            base.Draw();
            rotation -= (float)Math.PI / 2;

            // Draw turret
            turret.Draw();
        }
    }
}

// Niko Procopi
