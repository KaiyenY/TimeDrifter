using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dracer
{
    class Tank : Enemy
    {

        //Constructor
        public Tank(Vector2 Position)
            : base("", Position) //TODO: Get a sprite for the Tank
        {
            
        }
    }
}
