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
    abstract class Collider
    {
        // fields
        protected Vector2 posOffset;

        protected GameObject parent;

        // properties
        public Vector2 PosOffset { get { return posOffset; } }
        public Vector2 WorldPos { get { return parent.Position + RotateVector(posOffset, parent.Rotation); } }
        public GameObject Parent { get { return parent; } }

        // constructor
        public Collider(Vector2 posOffset, GameObject parent)
        {
            this.posOffset = posOffset;
            this.parent = parent;
        }

        public Collider(GameObject parent)
                 : this(Vector2.Zero, parent) { }

        // methods

        // calculates the moment of inertia of this collider shape based on a given mass and using the posOffset
        public abstract float CalculateMoment(float mass);

        // returns the AABB that fully encompasses this collider
        public abstract AABB GetAABB();

        // static helper methods

        public static Vector2 RotateVector(Vector2 vector, float rotation)
        {
            // formula for rotating a vector
            return new Vector2(vector.X * (float)Math.Cos(rotation) - vector.Y * (float)Math.Sin(rotation),
                               vector.X * (float)Math.Sin(rotation) + vector.Y * (float)Math.Cos(rotation));
        }

        // Calculates the trivial case of AABB vs AABB and returns only whether they are colliding or not
        // Used for broad collision detection to form collision pairs for closer inspection
        public static bool IsCollideBroad(Collider a, Collider b)
        {
            AABB aBox = a.GetAABB();
            AABB bBox = b.GetAABB();
            return (aBox.WorldPos.X + aBox.HalfWidth > bBox.WorldPos.X - bBox.HalfWidth
                        && aBox.WorldPos.X - aBox.HalfWidth < bBox.WorldPos.X + bBox.HalfWidth)
                && (aBox.WorldPos.Y + aBox.HalfHeight > bBox.WorldPos.Y - bBox.HalfHeight
                        && aBox.WorldPos.Y - aBox.HalfHeight < bBox.WorldPos.Y + bBox.HalfHeight);
        }

        // Calls the appropriate collision algorithm based on the colliders
        // Used in narrow collision detection and manifold generation for collision pairs
        public static CollisionManifold IsCollideNarrow(Collider a, Collider b)
        {
            // AABB vs AABB
            if(a is AABB && b is AABB)
            {
                OBB newA = ((AABB)a).GetOBB();
                OBB newB = ((AABB)b).GetOBB();
                return OBBVsOBB(newA, newB);
            }

            // OBB vs OBB
            if(a is OBB && b is OBB)
            {
                return OBBVsOBB((OBB)a, (OBB)b);
            }
            else if(a is OBB && b is AABB)
            {
                return OBBVsOBB((OBB)a, ((AABB)b).GetOBB());
            }
            else if(a is AABB && b is OBB)
            {
                return OBBVsOBB(((AABB)a).GetOBB(), (OBB)b);
            }

            // OBB vs Circle
            if(a is OBB && b is Circle)
            {
                return OBBVsCircle((OBB)a, (Circle)b);
            }
            else if(a is Circle && b is OBB)
            {
                return OBBVsCircle((OBB)b, (Circle)a);
            }
            else if (a is AABB && b is Circle)
            {
                return OBBVsCircle(((AABB)a).GetOBB(), (Circle)b);
            }
            else if (a is Circle && b is AABB)
            {
                return OBBVsCircle(((AABB)b).GetOBB(), (Circle)a);
            }

            // Circle vs Circle
            if(a is Circle && b is Circle)
            {
                return CircleVsCircle((Circle)a, (Circle)b);
            }

            // All nine possibilities are represented so you shouldn't be able to get here
            throw new Exception();
        }

        public static CollisionManifold OBBVsOBB(OBB a, OBB b)
        {
            float bestDist = float.NegativeInfinity;        // this will be the penetration depth
            Vector2 resAxis = Vector2.UnitX;                // this will be the resolution axis
            OBB referenceOBB = a;                           // the collider who "owns" the resolution axis
            OBB incidentOBB = b;                            // the collider whose corner has contacted the reference collider

            // check OBB A's normals
            Vector2[] worldNormalsA = a.WorldNormals;
            for(int i = 0; i < worldNormalsA.Length; i++)
            {
                float dist = Vector2.Dot(worldNormalsA[i], b.GetSupportPoint(-worldNormalsA[i]) - a.GetSupportPoint(worldNormalsA[i]));

                if(dist > 0)
                {
                    // we found a seperating axis so no collision detected
                    return new CollisionManifold();
                }
                else if(dist > bestDist)
                {
                    bestDist = dist;
                    resAxis = worldNormalsA[i];
                    referenceOBB = a;
                    incidentOBB = b;
                }
            }

            // check OBB B's normals
            Vector2[] worldNormalsB = b.WorldNormals;
            for (int i = 0; i < worldNormalsB.Length; i++)
            {
                float dist = Vector2.Dot(worldNormalsB[i], a.GetSupportPoint(-worldNormalsB[i]) - b.GetSupportPoint(worldNormalsB[i]));

                if (dist > 0)
                {
                    // we found a seperating axis so no collision detected
                    return new CollisionManifold();
                }
                else if (dist > bestDist)
                {
                    bestDist = dist;
                    resAxis = worldNormalsB[i];
                    referenceOBB = b;
                    incidentOBB = a;
                }
            }

            bestDist = Math.Abs(bestDist);          // penetration depth should be positive

            // if we got this far that means no seperating axis has been found and collision is detected
            // find contact points
            Vector2 incidentContact = incidentOBB.GetSupportPoint(-resAxis) - incidentOBB.WorldPos;
            Vector2 referenceContact = incidentOBB.WorldPos - referenceOBB.WorldPos + incidentContact;
            referenceContact += bestDist * Vector2.Normalize(resAxis);

            // the new "A" in the manifold should always be the reference face
            return new CollisionManifold(true, bestDist, resAxis, referenceOBB, referenceContact, incidentOBB, incidentContact);
        }

        public static CollisionManifold OBBVsCircle(OBB a, Circle b)
        {
            float bestDist = float.NegativeInfinity;        // the penetration depth
            Vector2 resAxis = Vector2.UnitX;                // the resolution axis

            Vector2 posDif = b.WorldPos - a.WorldPos;       // find the vector between them in world coordinates
            posDif = RotateVector(posDif, -a.WorldRot);     // convert to local coordinates of the OBB

            // check if the posDif is inside the OBB
            if(posDif.X < a.HalfWidth && posDif.X > -a.HalfWidth && posDif.Y < a.HalfHeight && posDif.Y > -a.HalfHeight)
            {
                // find the shortest distance from the center of the circle to one of the sides
                for(int i = 0; i < a.Normals.Length; i++)
                {
                    float dist = Vector2.Dot(a.Normals[i], posDif - a.GetSupportPoint(a.Normals[i]));

                    if(dist > 0)
                    {
                        // HOW????      no collision detected
                        return new CollisionManifold();
                    }
                    else if (dist > bestDist)
                    {
                        bestDist = dist;
                        resAxis = a.WorldNormals[i];    // resolution axis should be in world coords
                    }
                }

                bestDist -= b.Radius;           // the penetration depth shouldn't just be from center to side
            }
            else
            {
                // clamp the posDif so it is a point on the OBB
                Vector2 closestPoint = posDif;
                if(closestPoint.X > a.HalfWidth)
                {
                    closestPoint.X = a.HalfWidth;
                }
                else if(closestPoint.X < -a.HalfWidth)
                {
                    closestPoint.X = -a.HalfWidth;
                }
                if (closestPoint.Y > a.HalfHeight)
                {
                    closestPoint.Y = a.HalfHeight;
                }
                else if (closestPoint.Y < -a.HalfHeight)
                {
                    closestPoint.Y = -a.HalfHeight;
                }

                bestDist = (posDif - closestPoint).Length() - b.Radius;
                resAxis = Vector2.Normalize(posDif - closestPoint);
            }

            if (bestDist < 0)
            {
                // collision detected
                // penetration depth should be positive
                bestDist = Math.Abs(bestDist);

                // find contact points
                Vector2 contactPointB = b.Radius * -resAxis;
                Vector2 contactPointA = b.WorldPos - a.WorldPos + contactPointB;
                contactPointA += bestDist * Vector2.Normalize(resAxis);

                return new CollisionManifold(true, bestDist, resAxis, a, contactPointA, b, contactPointB);
            }

            // you shouldn't ever get here, but just in case we'll say no collision found
            return new CollisionManifold();
        }

        public static CollisionManifold CircleVsCircle(Circle a, Circle b)
        {
            float distance = (b.WorldPos - a.WorldPos).Length();
            float penetrationDepth = a.Radius + b.Radius - distance;

            if(penetrationDepth > 0)
            {
                // find contact points
                Vector2 contactPointA = a.Radius * Vector2.Normalize(b.WorldPos - a.WorldPos);
                Vector2 contactPointB = b.Radius * Vector2.Normalize(a.WorldPos - b.WorldPos);

                // generate manifold
                CollisionManifold manifold = new CollisionManifold(true, penetrationDepth, Vector2.Normalize(b.WorldPos - a.WorldPos), a, contactPointB, b, contactPointA);

                return manifold;
            }
            else
            {
                // returns no collision found
                return new CollisionManifold();
            }
        }
    }
}

// Matthew Soriano