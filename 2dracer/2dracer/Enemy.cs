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
    class Enemy : GameObject
    {
        public Queue<Node> Route { get; set; } //The path the enemy will take
        private Node currentDestination; //The node within the path that the car will currently go towards

        public Enemy(Texture2D tex, Vector2 v) :
            base(v, 0, tex)
        {
            currentDestination = new Node(v.ToPoint()); //initialize current destination to where it begins
        }

        public void Update(Vector2 PlayerPos)
        {
            // turn car
            //rotation += 0.04f;
            Vector2 toPlayer = PlayerPos - Position; //Get Vector to the player
            toPlayer.Normalize(); //Turn to unit Vector
            
            //Update Position
            position.X += toPlayer.X;
            position.Y += toPlayer.Y;
            // move car
            //float speed = 3;
            //position.X += (float)Math.Cos(rotation) * speed;
            //position.Y += (float)Math.Sin(rotation) * speed;
        }

        private float Speed()
        {
            //use all physics here

            // use acceleration
            // friction
            // collision detection

            // to return the velocity at any given time
            return 5;
        }

        public void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.DrawRect(8);
            rotation -= (float)Math.PI / 2;
        }


        public void UpdatePositionTowardsNextNode() //Moves the car a little along its current route
        {
            if(Route != null) //Don't do anything if there's no Route assigned
            {
                if (withinRange(20, currentDestination) && Route.Count != 0)
                {
                    Node nextPlace = new Node(this.Route.Dequeue());
                    currentDestination = nextPlace; //If reached current target node, fetch next one from the Queue
                }

                Vector2 toNode = new Vector2(currentDestination.Location.X - this.Position.X, currentDestination.Location.Y - this.Position.Y); //Vector to the target 
                toNode.Normalize(); //turn to unit vector

                //Apply movement
                position.X += toNode.X * Speed();
                position.Y += toNode.Y * Speed();

                rotation = (float)Math.Atan2(toNode.Y, toNode.X);
            }
        }

        private bool withinRange(int offset, Node origin) //Creates an acceptable area to check when to get the next target
        {
            Rectangle acceptableArea = new Rectangle(origin.Location.X - offset, origin.Location.Y - offset, 2 * offset, 2 * offset);

            if (acceptableArea.Contains(this.Position))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
