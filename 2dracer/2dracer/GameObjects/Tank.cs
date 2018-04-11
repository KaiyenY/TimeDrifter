using _2dracer.Managers;
using Microsoft.Xna.Framework;

namespace _2dracer
{
    class Tank : Enemy
    {

        //Constructor
        public Tank(Vector2 Position)
            : base(LoadManager.Sprites["Tank"], Position) //TODO: Get a sprite for the Tank
        {
            
        }
    }
}
