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

                                // This can be a list
        Vector2 getDestination(Vector2 PlayerPos)
        {
            // if the cop is not close enough to touch the player
            // return a point that will bring cop closer to player

                // if point is within a certain distance of the cop
                // if point brings cop closer to the player
                // return that point

            // These points can be generated automatically by the level editor
           
            // if cop is very close to the player
            // return player and crash into them
            return PlayerPos;
        }

       

        void driveToPoint(Vector2 destination)
        {
            // turn car
            Vector2 toPlayer = destination - Position; //Get Vector to the player
            toPlayer.Normalize(); //Turn to unit Vector
            
            //Update Position
            position.X += toPlayer.X * Speed();
            position.Y += toPlayer.Y * Speed();

            rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);
        }
        //---------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------

        // in the future, we can pass
        // a list of Vector2 points that cops can
        // drive to, such as intersections or curbs
        public void Update(Vector2 PlayerPos)
        {
                                                // pass List of desstinations
            Vector2 destination = getDestination(PlayerPos);

            driveToPoint(destination);
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

        public void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.DrawRect(4);
            Game1.spriteBatch.DrawString(Game1.comicSans, "GOING TO " + currentDestination.Location, new Vector2(this.Position.X + 10, this.Position.Y - 10), Color.Red);
            rotation -= (float)Math.PI / 2;
        }


        public void UpdatePositionTowardsNextNode() //Moves the car a little along its current route
        {
            if(Route != null) //Don't do anything if there's no Route assigned
            {
                if (withinRange(5, currentDestination) && Route.Count != 0)
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
