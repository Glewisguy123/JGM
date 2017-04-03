using Engine.Entities;
using Engine.Events.CollisionEvent;
using Engine.Managers.EntityRelated;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Managers;
using ADS.Entities;
using ADS.Managers.High_Tier.Collision;
using ADS.Collision.SAT;
using ADS.Events.CollisionEvent;

namespace Engine.Managers.Collision
{
    public class DetectionManger : IUpdService
    {

        public delegate void CollisionEventHandler(object sender, CollisionEventArgs e);

        public event CollisionEventHandler OnCollision;

        public event CollisionEventHandler OnSATCollision;

        //The object we will be using for our SAT checks
        private ADS.Collision.SAT.SATcheck satTest = new ADS.Collision.SAT.SATcheck();
        private bool hasMoved = false;

        //List of collidable entities
        private List<ICollidable> collision = new List<ICollidable>();
        //Tile map

     

        /// <summary>
        /// Adds an ICollidable interface to the collision list ready to check for collisions
        /// </summary>
        /// <param name="obj"></param>
        public void addCollidable(ICollidable obj)
        {
            collision.Add(obj);
            Console.WriteLine("Object Added to Collision list" + obj.GetType());
        }

        public void removeCollidable(ICollidable obj)
        {

        }

      




        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < collision.Count; i++)
            {
                for (int x = 1; x < collision.Count; x++)
                {
                    if (collision[x] != collision[i])
                    {
                        CheckSAT(collision[i], collision[x]);
                    }
                }
            }
            hasMoved = false;
            // doCollision();
        }

        /// <summary>
        /// Checks Collision between dynamic entities.
        /// </summary>
        public void doCollision()
        {
            if (collision.Count >= 2)
            {
                //Iterate through the list twice, counting one object above the first each time
                for (int i = 0; i < collision.Count; i++)
                {
                    for (int k = i + 1; k < collision.Count; k++)
                    {
                        //Check that they're not equal objects
                        if (collision[i] != collision[k])
                        {
                            var A = collision[i];
                            var B = collision[k];

                            Vector2 distance = B.Position - A.Position;
                            if (distance.Length() > 50)
                                continue;
                            else
                            //Checks if there's a collision, but also checks if A is a controller character, if so then it will move A around, otherwise tiles may have problems
                            if (AABB.Collision(A.Bounds, B.Bounds))
                            {
                                //Find minimum translation distance
                                // A.Velocity = new Vector2(A.Velocity.X, 0);
                                Vector2 mtd = TranslationVector.GetMinimumTranslation(A, B);
                                Console.WriteLine(mtd);
                                //Apply it to the source collidable
                                A.Position += mtd;
                            }
                        }
                    }
                }
            }
        }
         

    

  


        /// <summary>
        /// Tells any entities that care about colliding with other entities that they have collided, and what entity they have collided with.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        public void OnACollision(ICollidable A, ICollidable B)
        {
            if (OnCollision != null)
            {
                OnCollision(this, new CollisionEventArgs() { A = A, B = B });
            }
        }

        public void CallSAT(ICollidable A, ICollidable B, Vector2 mtv)
        {
            if(OnSATCollision != null)
            {
                OnSATCollision(this, new CollisionEventArgs() { A = A, B = B, mtvRet = mtv});
            }
        }

        #region GrantsMethods
        public void CheckSAT(ICollidable a, ICollidable b)
        {


            for (int i = 0; i < a.Hits.Count; i++)
            {
                for (int x = 0; x < b.Hits.Count; x++)
                {
                    if (satTest.SATCollision(a.Hits[i], b.Hits[x], a.Hits[i].Velocity, b.Hits[x].Velocity))
                    {
                        //  a.Hits[i].Mind.Velocity += (satTest.mtvRet() * a.Hits[i].Mind.Mass);
                        // b.Hits[x].Mind.Velocity -= (satTest.mtvRet() * b.Hits[x].Mind.Mass);

                        CallSAT(a, b, -satTest.mtvRet());
                        CallSAT(a, b, ImpulseApplication(a.Hits[i], b.Hits[x]));
                        //minimumMovement(a.Hits[i], b.Hits[x]);
                        //a.ApplyImpulse(-ImpulseApplication(a.Hits[i], b.Hits[x]));
                        //b.ApplyImpulse(ImpulseApplication(a.Hits[i], b.Hits[x]));
                        hasMoved = true;
                    }
                }
            }
        }

        public void minimumMovement(Hitbox a, Hitbox b)
        {
            Vector2 minim = satTest.mtvRet();
            Vector2 cNormal = a.Centre - b.Centre;
            a.Mind.Velocity -= 0.5f * minim * cNormal;
            b.Mind.Velocity += 0.5f * minim * cNormal;
        }

        public Vector2 ImpulseApplication(Hitbox a, Hitbox b)
        {
            Vector2 minim = satTest.mtvRet();
            Vector2 minim2 = satTest.mtvRet();

            //Vector2 cNormal = a.Centre - b.Centre; //might work

            //Vector2 cNormal = a.Centre - b.Centre;
            Vector2 combVel = a.Velocity - b.Velocity;
            Vector2 cNormal = Vector2.Normalize(combVel);
            float cVelocity = Vector2.Dot(a.Velocity - b.Velocity, cNormal);
            cNormal *= cVelocity;

            return cNormal;
        }

        #endregion
    }
}