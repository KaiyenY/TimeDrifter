using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dracer.Managers
{
    static class CollisionManager
    {
        // fields
        private static List<Helpers.CollisionPair> collisionPairs;
        private static List<Helpers.CollisionManifold> collisionManifolds;

        // constructor
        static CollisionManager()
        {
            collisionPairs = new List<Helpers.CollisionPair>();
            collisionManifolds = new List<Helpers.CollisionManifold>();
        }

        // methods
        public static void Update(/* list of objects with colliders*/)
        {
            // Broad phase (generate collision pairs)
            // Narrow phase (generate collision manifolds)
            // Response (resolve manifolds using impulse method)
        }
    }
}

// Matthew Soriano