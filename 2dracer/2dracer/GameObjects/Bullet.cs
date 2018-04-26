using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using _2dracer.Managers;

namespace _2dracer
{
    public class Bullet : Mover
    {
        // Properties
        public bool Delete { get; private set; }

        // Constructor
        public Bullet(Vector2 position, float angle)
            : base(new GameObject(position, angle, LoadManager.Sprites["Bullet"], new Vector2(20), 0.25f), Vector2.Zero, 0)
        {
            Delete = false;

            Update(); Update();
        }

        public override void Update()
        {
            // Only move bullets on screen, else, mark them for deletion
            if (position.X > -MapElements.Map.TileSize / 2 || 
                position.X < -MapElements.Map.TileSize / 2 + MapElements.Map.Size.X ||
                position.Y > -MapElements.Map.TileSize / 2 ||
                position.Y < -MapElements.Map.TileSize / 2 + MapElements.Map.Size.Y)
            {
                Player player = (Player)GameMaster.GameObjects[1];

                float speed = player.TopSpeed * 1.5f;

                velocity.X = (float)Math.Cos(rotation) * speed;
                velocity.Y = (float)Math.Sin(rotation) * speed;

                base.Update();
            }
            else
            {
                Delete = true;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        /// <summary>
        /// Sets the position of the bullet
        /// </summary>
        /// <param name="position">Where to place the bullet</param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// Sets the rotation of the bullet
        /// </summary>
        /// <param name="rotation">Rotation to face</param>
        public void SetRotation(float rotation)
        {
            this.rotation = rotation;
        }
    }
}

// Niko Procopi
