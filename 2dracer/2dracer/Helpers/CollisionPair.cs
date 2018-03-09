using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dracer.Helpers
{
    class CollisionPair
    {
        // fields
        Collider colliderA;
        Collider colliderB;

        // constructor
        public CollisionPair(Collider colliderA, Collider colliderB)
        {
            this.colliderA = colliderA;
            this.colliderB = colliderB;
        }
    }
}

// Matthew Soriano