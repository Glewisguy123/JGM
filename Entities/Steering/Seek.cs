using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Entities.Steering
{
    class Seek : SteeringBehaviour
    {
public Seek(SteeringMind e) : base(e)
        {

        }
        public void DoSeek(Vector2 Target)
        {
            //Find the direction
            direction = Vector2.Normalize(Target - position);
            //Find the desired Velocity
            DesiredVelocity = direction * MaxVelocity;
            //calculate the force we want to apply 
            steeringForce = DesiredVelocity - velocity;
            //Check that the steering force hasnt exceeded the max force
            steeringForce = CheckMax(steeringForce, MaxForce);
            //Apply mass
            steeringForce = steeringForce / mass;
            //Adjust velocity with the steering force
            velocity += steeringForce;

            //Apply velocity to pos
            position += velocity;

        }

        public override void Update()
        {
            base.Update();

            DoSeek(Host.current.Position); 
          
        }
    }
}
