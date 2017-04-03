using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Entities.Steering
{
    class Arrival : SteeringBehaviour
    {

        Seek Seeker;

        public Arrival(SteeringMind e) : base(e)
        {
            Seeker = new Seek(e);
        }

        public override void Update()
        {
            DoArrival(Host.current.Position, 150f); base.Update();

            Seeker.Update();

        }

        public override Vector2[] ApplyBehaviour()
        {

            Vector2[]returns = new Vector2[3];
            returns[0] = Seeker.ApplyBehaviour()[0];
            returns[1] = Seeker.ApplyBehaviour()[1];
            returns[2] = Seeker.ApplyBehaviour()[2];

            return returns;
        }

        private void DoArrival(Vector2 Target, float radius)
        {

            //Find distance from player to missle
            Vector2 distance = Target - position;
            var T = distance.Length();
            Console.WriteLine(T);
            if (T < radius)
            {
                
                Seeker.DoSeek(Truncate(Target + steeringForce * T, MaxVelocity));


                // Pursue(Target, current.Velocity);
            }
            else
            {
                Seeker.DoSeek(Target);
               


                //Seek(Target);
            }
        }



        private Vector2 Truncate(Vector2 A, float scale)
        {

            float i = scale / A.Length();
            i = i < 1.0f ? i : 1.0f;

            Vector2 result = new Vector2(Matrix.CreateScale(i).Translation.X, Matrix.CreateScale(i).Translation.Y);
            return result;
        }
    }
}
