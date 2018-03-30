using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.Managers;

namespace _2dracer
{
    public class Turret : GameObject
    {
        // Fields
        private float timer;            // Fire rate
        private int bulletIndex;        // Next bullet to fire

        // Properties

        // Constructor
        public Turret() : 
            base(new Vector2(0,0), 0, "Textures/Turret", new Vector2(25, 50))
        {
            timer = 0;
            bulletIndex = 0;
        }
        
        public override void Update()
        {
            // update timer
            timer += (float)Game1.gameTime.ElapsedGameTime.TotalMilliseconds;

            // change position of turret, depending on where car is
            position = GameMaster.GameObjects[0].Position;

            // get angle that the turret should be facing
            if (Input.ControlConnected())
            {
                // Use controller input (should add setting for this later)
                rotation = Input.ControlAngle();
            }
            else
            {
                // Use mouse input
                rotation = Input.MouseAngle(this);
            }

            // Call method Bullet.Fire instead of making the turret do it
            // a bullet fires every 0.15 seconds
            if ((Input.MouseHold(MouseButton.Left) || Input.ControlHold(Buttons.LeftShoulder)) && timer >= 150)
            {
                // Reset timer
                timer = 0;

                // Set bullet position
                GameMaster.Bullets[bulletIndex].SetPosition(position);

                // Set bullet rotation
                GameMaster.Bullets[bulletIndex].SetRotation(rotation);
                
                // Increment bullet index
                bulletIndex++;
                if (bulletIndex >= GameMaster.Bullets.Count)
                {
                    bulletIndex = 0;
                }
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
