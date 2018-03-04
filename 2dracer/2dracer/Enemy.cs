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
        public Enemy(Texture2D tex, Vector2 v) :
            base(v, 0, tex) { }

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

        float Speed()
        {
            // use all physics here

            // use acceleration
            // friction
            // collision detection

            // to return the velocity at any given time
            return 2;
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
        
        public void Draw()
        {
            rotation += (float)Math.PI / 2;
            base.DrawRect(8);
            rotation -= (float)Math.PI / 2;
        }
    }
}
