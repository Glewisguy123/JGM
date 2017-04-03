using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Collision.SAT
{
    class SATcheck
    {
        public Vector2 MinimumTranslationVector;

        public bool SATCollision(Hitbox shape1, Hitbox shape2, Vector2 velocity, Vector2 velocity2)
        {
            bool Intersect = true; //assume the shape is intersecting until calculated
            bool WillIntersect = true; //Assume the shape will intersect until calculated

            int edgelist1 = shape1.Edges.Count; //Create an integer of how many edges are in shape 1
            int edgelist2 = shape2.Edges.Count; //Create an integer of how many edges are in shape 2
            float _mtv = float.PositiveInfinity; //Minimum translation vector is set to infinity
            Vector2 trans_axis = new Vector2(); //The axis that we will push the shape along if its colliding
            Vector2 edge; //The current edge we are checking

            // Loop through all the edges of both polygons
            for (int e = 0; e < edgelist1 + edgelist2; e++) //For every edge on each shape
            {
                if (e < edgelist1)
                {
                    edge = shape1.Edges[e]; //If e is less than the first integer, we are checking each edge of the first shape
                }
                else
                {
                    edge = shape2.Edges[e - edgelist1]; //Same for shape 2 
                }

                // Find the axis perpendicular to the current edge to project on 
                Vector2 axis = new Vector2(-edge.Y, edge.X); //This maths works. I don't have room to write a comment explaining it.
                axis.Normalize(); // Keeping the vector pointing the same direction, adjust it so it's length is 1

                // Find the projection of the polygon on the current axis
                float min1 = 0; float min2 = 0; float max1 = 0; float max2 = 0; //These are initialised to 0 and adjusted in the method below
                Projection(axis, shape1, ref min1, ref max1); //Project polygon onto axis
                Projection(axis, shape2, ref min2, ref max2); //Project polygon 2 onto axis

                // Check if the polygon projections are currentlty intersecting
                if (IntervalDistance(min1, max1, min2, max2) > 0)
                {
                    Intersect = false; //This if statement will only run if the first condition of the method is met, therefore we know they aren't intersecting
                }

                // Project the velocity on the current axis
                //As per the nature of programming the + 1 is simply a speed offset that works for a simple reason
                //We're not going to tell you that reason
                //ggwp Marc Price
                float _vel_proj = Vector2.Dot(axis, velocity) + 0.1f;
                //float _vel_proj2 = Vector2.Dot(axis, velocity2) + 1;

                // Get the projection of polygon A during the movement
                if (_vel_proj < 0)
                {
                    min1 += _vel_proj;
                }
                else
                {
                    max1 += _vel_proj;
                }

                // Do the same test as above for the new projection
                float dist = IntervalDistance(min1, max1, min2, max2);
                if (dist > 0)
                {
                    WillIntersect = false; //Same as above but this time using a hypothetical movement
                }
                // If the polygons are not intersecting and won't intersect, exit the loop
                if (!Intersect && !WillIntersect)
                {
                    MinimumTranslationVector = Vector2.Zero;
                    return false;
                }
                // Check if the current interval distance is the minimum one. If so store
                // the interval distance and the current distance.
                // This will be used to calculate the minimum translation vector
                dist = Math.Abs(dist); //Find how far from 0 the distance is
                if (dist < _mtv) //If the minimum translation vector is not the smallest possible movement
                {
                    _mtv = dist; //Set the vector to the smallest distance!
                    trans_axis = axis; //We need to move the object on the axis we projected

                    Vector2 d = shape1.Centre - shape2.Centre; //We need to find which object is on the left hand side of the other
                    /*if (Vector2.Dot(d, trans_axis) < 0) //If we project the translation axis onto the current vector between the two shapes and it's negative, we are moving in the wrong direction
                    {
                        trans_axis = -trans_axis; //so we reverse the axis we're moving on!
                    }*/
                    if (d.Length() < 0)
                    {
                        trans_axis *= -1;
                    }
                }
            }

            // The minimum translation vector
            // can be used to push the polygons apart.
            if (WillIntersect) //If the objects are colliding
            {
                MinimumTranslationVector = trans_axis * _mtv; //Set the property to be the translation axis multiplied by the mtv
                return true;
            }

            return false;
        }

        public void Projection(Vector2 axis, Hitbox myEnt, ref float min, ref float max)
        {
            // To project a point on an axis use the dot product
            float d = Vector2.Dot(axis, myEnt.Points[0]);
            min = d;
            max = d;
            for (int i = 0; i < myEnt.Points.Count; i++)
            {
                d = Vector2.Dot(myEnt.Points[i], axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }

        public float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            if (minA < minB)
            {
                return minB - maxA;
            }
            else
            {
                return minA - maxB;
            }

        }

        public Vector2 mtvRet()
        {
            //Console.WriteLine("mtv is " + MinimumTranslationVector);
            return MinimumTranslationVector;
        }
    }
}
