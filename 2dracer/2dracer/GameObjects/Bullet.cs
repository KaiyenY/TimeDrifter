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
    class Bullet : Mover
    {
        public Bullet(Texture2D sprite, Vector2 position, float angle)
            : base(new GameObject(position, angle, sprite, new Vector2(20)), Vector2.Zero, 0)
        {
            //bullet position = gun position
            //we want bullet to start at tip of gun
            //move bullet to tip of gun

            //advance bullet by 4 frames
            for (int i = 0; i < 4; i++)
                Update();
        }

        public override void Update()
        {
            // only move bullet if it is close enough to matter
            if (Math.Abs(position.X) < 100000 || Math.Abs(position.Y) < 100000)
            {
                float speed = 400;

                velocity.X = (float)Math.Cos(rotation) * speed;
                velocity.Y = (float)Math.Sin(rotation) * speed;

                base.Update();
            }
        }

        public override void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.Draw();
            rotation -= (float)Math.PI / 2;
        }
    }
}

// Niko Procopi
