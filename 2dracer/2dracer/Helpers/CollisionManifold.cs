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
        public float penetrationDepth;
        public Vector2 resolutionAxis;

        public Collider colliderA;
        public List<Vector2> contactPointsA;

        public Collider colliderB;
        public List<Vector2> contactPointsB;

        // constructor
        public CollisionManifold(float penetrationDepth, Vector2 resolutionAxis,
               Collider colliderA, List<Vector2> contactPointsA, Collider colliderB, List<Vector2> contactPointsB)
        {
            this.penetrationDepth = penetrationDepth;
            this.resolutionAxis = resolutionAxis;
            this.colliderA = colliderA;
            this.contactPointsA = contactPointsA;
            this.colliderB = colliderB;
            this.contactPointsB = contactPointsB;
        }

        public CollisionManifold(float penetrationDepth, Vector2 resolutionAxis, Collider colliderA, Collider colliderB)
                          : this(penetrationDepth, resolutionAxis, colliderA, new List<Vector2>(), colliderB, new List<Vector2>()) { }
    }
}

// Matthew Soriano