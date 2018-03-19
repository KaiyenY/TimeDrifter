using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _2dracer.Helpers
{
    class AABB : Collider
    {
        // fields
        protected float halfWidth;
        protected float halfHeight;

        // properties
        public float HalfWidth { get { return halfWidth; } }
        public float HalfHeight { get { return halfHeight; } }

        // constructors
        public AABB(Vector2 posOffset, float width, float height, GameObject parent)
             : base(posOffset, parent)
        {
            halfWidth = Math.Abs(width * 0.5f);
            halfHeight = Math.Abs(height * 0.5f);
        }

        public AABB(float width, float height, GameObject parent)
             : this(Vector2.Zero, width, height, parent) { }

        // methods
        public override float CalculateMoment(float mass)
        {
            // lots of math to calculate the moment of inertia of a rectangular plate
            return (float)((1 / 12) * mass * (Math.Pow(halfHeight * 2, 2) + Math.Pow(halfWidth * 2, 2)) + mass * posOffset.LengthSquared());
        }

        public override AABB GetAABB()
        {
            return this;
        }

        /// <summary>
        /// Returns the OBB representation of this AABB
        /// </summary>
        public OBB GetOBB()
        {
            return new OBB(posOffset, 0f, halfWidth * 2, halfHeight * 2, parent);
        }
    }
}

// Matthew Soriano