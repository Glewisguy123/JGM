using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Entities.Steering
{
    abstract class SteeringBehaviour : ISteeringBehaviour
    {
        protected SteeringMind Host;
        protected Vector2 direction;
        protected Vector2 DesiredVelocity;
        protected Vector2 steeringForce;
        protected Vector2 position;
        protected float MaxVelocity = 2f;
        protected float MaxForce = 0.5f;
        protected float mass = 35f;
        protected Vector2 velocity;
  
        protected IEntity current;

        public SteeringBehaviour(SteeringMind e)
        {
            Host = e;
            current = e.current;
           
        }

        public virtual Vector2[] ApplyBehaviour()
        {
            Vector2[] returns = new Vector2[3];
            returns[0] = position;
            returns[1] = velocity;
            returns[2] = steeringForce;
           
            return returns;
        }

        public virtual void Update()
        {
            position = Host.Position;
            velocity = Host.Velocity;
            steeringForce = Host.steeringForce;
        }

     

        public Vector2 CheckMax(Vector2 v, float maxValue)
        {
            float x = maxValue / v.Length();

            //Check if X is loswer than the maximum value
            if (x < 1.0)
            {
                x = 1.0f;
            }


            v.X = v.X + x;
            v.Y = v.Y + x;
            return v;
        }


    }
}
