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
            halfWidth = width * 0.5f;
            halfHeight = height * 0.5f;
        }

        public AABB(float width, float height, GameObject parent)
             : this(Vector2.Zero, width, height, parent) { }

        // methods
        public override float CalculateMoment(float mass)
        {
            throw new NotImplementedException();
        }

        public override AABB GetAABB()
        {
            throw new NotImplementedException();
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