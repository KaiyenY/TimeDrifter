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

        public Enemy(Texture2D sprite, Vector2 position) :
            base(position, 0, sprite, new Vector2(64, 128))
        {
            currentDestination = new Node(position.ToPoint()); //initialize current destination to where it begins
        }

        private float Speed()
        {
            //use all physics here

            // use acceleration
            // friction
            // collision detection

            // to return the velocity at any given time
            return 3;
        }

        public override void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.Draw();
            Game1.spriteBatch.DrawString(Game1.comicSans, "GOING TO " + currentDestination.Location, new Vector2(this.Position.X + 10, this.Position.Y - 10), Color.Red);
            rotation -= (float)Math.PI / 2;
        }


        public void UpdatePositionTowardsNextNode() //Moves the car a little along its current route
        {
            if(Route != null) //Don't do anything if there's no Route assigned
            {
                // set range to 10
                // cop should not touch the point before going to the next
                if (withinRange(10, currentDestination) && Route.Count != 0)
                {
                    currentDestination = new Node(this.Route.Dequeue()); //If reached current target node, fetch next one from the Queue
                }

                Vector2 toNode = new Vector2(currentDestination.Location.X - this.Position.X, currentDestination.Location.Y - this.Position.Y); //Vector to the target 
                

                // rot is the rotation that the player SHOULD be moving in
                // it is not the rotation that the player IS moving in
                rotation = (float)Math.Atan2(toNode.Y, toNode.X);
                toNode.Normalize(); //turn to unit vector
           
                // if this is not here, then current rotation may be 359 degrees
                // and the required rotation may be 2 degrees, and then
                // car will turn in the wrong directoin
                // this if-statement fixes it, trust me
                // comment it out and see what happens
                //if (Math.Abs(rot - rotation) > Math.PI)
                  //  rot += 2 * (float)Math.PI;

                // if player's rotation is not the correct rotation
                // then slowly turn the player
                //if (rot > rotation)
                  //  rotation += 0.02f;

                //if (rot < rotation)
                  //  rotation -= 0.02f;

                //Apply movement with the current rotation of the car
                position.X += (float)Math.Cos(rotation) * Speed();
                position.Y += (float)Math.Sin(rotation) * Speed();
            }
        }

        public bool withinRange(int offset, Node origin) //Creates an acceptable area to check when to get the next target
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
//Ruben Young