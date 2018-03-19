using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2dracer.Helpers
{
    class CollisionManifold
    {
        // fields
        public bool isColliding;
        
        public float penetrationDepth;
        public Vector2 resolutionAxis;

        public Collider colliderA;
        public Vector2 contactPointA;

        public Collider colliderB;
        public Vector2 contactPointB;

        // constructor
        public CollisionManifold(bool isColliding, float penetrationDepth, Vector2 resolutionAxis,
               Collider colliderA, Vector2 contactPointA, Collider colliderB, Vector2 contactPointB)
        {
            this.isColliding = isColliding;
            this.penetrationDepth = penetrationDepth;
            this.resolutionAxis = resolutionAxis;
            this.colliderA = colliderA;
            this.contactPointA = contactPointA;
            this.colliderB = colliderB;
            this.contactPointB = contactPointB;
        }

        public CollisionManifold()
                          : this(false, 0f, Vector2.Zero, null, Vector2.Zero, null, Vector2.Zero) { }
    }
}

// Matthew Soriano