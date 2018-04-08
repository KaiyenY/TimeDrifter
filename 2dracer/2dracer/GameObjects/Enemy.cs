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
    public class Enemy : Mover
    {
        // Fields
        protected Queue<Node> Route { get; set; } //The path the enemy will take
        protected Node currentDestination; //The node within the path that the car will currently go towards
        protected Node mostRecent; //This node holds the center node of the tile the car just stepped on. Used for A* calculations
        private float prevRotation;
        private bool enableDurp;

        // Constructor
        public Enemy(string spritePath, Vector2 position) 
            : base(new GameObject(position, 0, spritePath, new Vector2(64, 128)), Vector2.Zero, 0)
        {
            currentDestination = new Node(base.position.ToPoint()); //initialize current destination to where it begins
            prevRotation = rotation;
        }

        // Methods
        public override void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.Draw();
            Game1.spriteBatch.DrawString(Game1.comicSans, "GOING TO " + currentDestination.Location, new Vector2(this.Position.X + 10, this.Position.Y - 10), Color.Red);
            rotation -= (float)Math.PI / 2;
        }

        public override void Update()
        {
            UpdatePositionTowardsNextNode();

            base.Update();
        }

        /// <summary>
        /// Finds route to the Node the Player just stepped on. 
        /// </summary>
        /// <param name="Destination">Pass in the target</param>
        public void FindRoute(Node Destination)
        {
            //Use the AI class' A* logic
            this.Route = AI.Pathfind(mostRecent, Destination);
        }

        public void UpdatePositionTowardsNextNode() //Moves the car a little along its current route
        {
            if(Route != null) //Don't do anything if there's no Route assigned
            {
                // set range to 100
                // cop should not touch the point before going to the next
                if (WithinRange(100, currentDestination) && Route.Count != 0)
                {
                    currentDestination = new Node(this.Route.Dequeue()); //If reached current target node, fetch next one from the Queue
                }

                Vector2 toNode = new Vector2(currentDestination.Location.X - this.Position.X, currentDestination.Location.Y - this.Position.Y); //Vector to the target 
                

                // rot is the rotation that the player SHOULD be moving in
                // it is not the rotation that the player IS moving in
                float rot = (float)Math.Atan2(toNode.Y, toNode.X);
                toNode.Normalize(); //turn to unit vector
           
                // if this is not here, then current rotation may be 359 degrees
                // and the required rotation may be 2 degrees, and then
                // car will turn in the wrong directoin
                // this if-statement fixes it, trust me
                // comment it out and see what happens

                if (Math.Abs(rot - rotation) > Math.PI)
                    rot += 2 * (float)Math.PI;

                // if player's rotation is not the correct rotation
                // then slowly turn the player
                if (rot > rotation)
                    rotation += 0.02f;

                if (rot < rotation)
                    rotation -= 0.02f;

                //Apply movement with the current rotation of the car

                double totalVelocity = Math.Sqrt(velocity.X * velocity.X + velocity.Y + velocity.Y);

                // cops slam on breaks if they go too fast
                if (Math.Abs(totalVelocity) > 100)
                {
                    velocity.X /= 2;
                    velocity.Y /= 2;
                }


                velocity.X += (float)Math.Cos(rotation) * 3;
                velocity.Y += (float)Math.Sin(rotation) * 3;

                if (Input.KeyTap(Keys.I))
                {
                    enableDurp = !enableDurp;
                }

                if (!enableDurp)
                {
                    if (prevRotation != rotation)
                    {
                        float rotDiff = prevRotation - rotation;

                        velocity = new Vector2(
                            (float)(velocity.X * Math.Cos(-rotDiff) - velocity.Y * Math.Sin(-rotDiff)),
                            (float)(velocity.X * Math.Sin(-rotDiff) + velocity.Y * Math.Cos(-rotDiff)));


                        prevRotation = rotation;
                    }
                }
            }
        }

        public bool WithinRange(int offset, Node center) //Creates an acceptable area to check when to get the next target
        {
            Rectangle acceptableArea = new Rectangle(center.Location.X - offset, center.Location.Y - offset, 2 * offset, 2 * offset);
            
            if (acceptableArea.Contains(this.Position))
            {
                this.mostRecent = center; //Hold the fact that this node was just stepped on. 
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