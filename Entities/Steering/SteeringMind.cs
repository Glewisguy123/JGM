using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Events.MouseEvent;
using Engine.Managers.CamManage;
using ADS.Utilities;
using Engine.Managers.EntityRelated;
using Engine.Entities;
using Engine;
using ADS.Collision.SAT;
using Engine.Events.CollisionEvent;
using Engine.Managers.Collision;
using ADS.Grid.Generation.New;

namespace ADS.Entities.Steering
{
    class SteeringMind : Engine.Entities.Mind
    {

        //Steering behaviour weighting for the alignment
        private float alignmentWeight = 0.1f;
        //Steering behaviour weighting for the cohesion
        private float cohesionWeight = 0.1f;
        //steering behaviour weighting for the seperation
        private float seperationWeight = 0.1f;

        //Player Texture
        private Texture2D tex;
        private Vector2 direction;
        //private Vector2 velocity;
        private Vector2 DesiredVelocity;
        private Vector2 steeringForce;
        private Vector2 position;
        private SpriteBatch sb;
       // private Player target;
        private float rotationAngle;
        private Vector2 origin = Vector2.Zero;
        private float MaxVelocity = 2f;
        private float MaxForce = 0.5f;
        private float mass = 35f;
        private int counter = 0;
        int neighbourCount = 0;

        Vector2 vel;

        IEntity current;

        Vector2 mousPos;

        /// <summary>
        /// A list of all
        /// </summary>
        private static List<SteeringMind> neighbours = new List<SteeringMind>();
        private Vector2 wonderTarget;
        private bool wonderSet = false;
        private int wanderRange = 300;
        private bool flockset;
        private Vector2 flockGoal;

        public List<Node> Pathway { get; private set; }

        public override void Initialize(Vector2 Position)
        {
            texPath = "AntiBody";
            //Allow other steering entities to acknowledge this entity
            neighbours.Add(this);
            Locator.Instance.getService<MouseHandler>().MouseMoved += OnMouseMoved;

            current = Locator.Instance.getService<EntityManager>().getCamEntity("Player");
            position = Position;

            Mass = 0.1f;
            Restitution = 0.00001f;
            Damping = 0.97f;
            //Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y + 3), 16, 7, 45, this));
            //Hits.Add(new Hitbox(new Vector2(Position.X + 16, Position.Y + 3), 16, 7, -45, this));
            //Hits.Add(new Hitbox(new Vector2(Position.X + 12, Position.Y + 16), 8, 17, 0, this));

            base.Initialize(Position);
        }

        public void Wander()
        {

            int ran = Constants.r.Next(50, 130);

            if (!wonderSet)
            {

                wonderTarget = new Vector2(Constants.r.Next(0, wanderRange), Constants.r.Next(0, wanderRange));
                wonderSet = true;
            }
            counter++;

            if (counter >= ran)
            {
                wonderTarget = new Vector2(Constants.r.Next(0, wanderRange), Constants.r.Next(0, wanderRange));
                counter = 0;
            }
            Seek(wonderTarget);

        }

        public void flock()
        {
            Random random = new Random();
            int ran = 1000;

            if (!flockset)
            {

                flockGoal = new Vector2(random.Next(0, wanderRange), random.Next(0, wanderRange));
                flockset = true;
            }
            counter++;

            if (counter >= ran)
            {
                flockGoal = new Vector2(random.Next(0, wanderRange), random.Next(0, wanderRange));
                counter = 0;
            }

            Seek(flockGoal);


        }

        public void setPath(List<Node> path)
        {
            Pathway = path;
        }

        public void followWayPoints()
        {

        }

        public void OnMouseMoved(object sender, MouseEventArgs k)
        {
            mousPos = Locator.Instance.getService<CameraManager>().getWorldPosition(new Vector2(k.X, k.Y));
        }

        public override void Update(GameTime gameTime)
        {
            //Find way to "Find" the player via is name (hint entity manager lel)
            // Seek(current.Position);
            //Seek(current.Position);
            velocity = Velocity;
            position = Position;
            velocity = flockBehaviour(velocity);

            Seek(current.Position);

            // position = _pos;
            // Pursue(current.Position,current.Velocity);
            // Evade(current.Position, current.Velocity);

            //to do, add pathfinding code
            //Make it generic
            //Add manager
            velocity.Normalize();

            Velocity = velocity;
            Position = position;

            // _pos = position;

           // UpdatePhysics();

            


            base.Update(gameTime);
        }

        #region Behaviours

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector2 flockBehaviour(Vector2 v)
        {
            v += calcAlignment() * alignmentWeight + calcCohesion() * cohesionWeight + calcSeperation() * seperationWeight;
            return v;
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        public void Seek(Vector2 Target)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        public void Flee(Vector2 Target)
        {
            //Find the direction
            direction = Vector2.Normalize(position - Target);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="TargetsVelocity"></param>
        public void Pursue(Vector2 Target, Vector2 TargetsVelocity)
        {
            //Find distance from player to missle
            Vector2 distance = Target - position;
            //Time until missle hits target. Will be used to steer closer into the target when gap is close. The further away, we try to steer in front of the target
            float T = distance.Length() / MaxVelocity;
            Vector2 futurePosition = Target + TargetsVelocity * T;

            Seek(futurePosition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="TargetsVelocity"></param>
        public void Evade(Vector2 Target, Vector2 TargetsVelocity)
        {
            //Find distance from player to missle
            Vector2 distance = position - Target;
            //Time until missle hits target. Will be used to steer closer into the target when gap is close. The further away, we try to steer in front of the target
            float T = distance.Length() / MaxVelocity;
            Vector2 futurePosition = Target + TargetsVelocity * T;

            Flee(futurePosition);
        }


        public void Arrival(Vector2 Target, float radius)
        {
            
            //Find distance from player to missle
            Vector2 distance = Target - position;
            var T = distance.Length();
            if(T < radius)
            {
                Seek(Truncate(Target + steeringForce * T, MaxVelocity ));
                                Console.WriteLine("Inside");

                // Pursue(Target, current.Velocity);
            }
            else
            {
                Seek(Target);
                //Seek(Target);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 calcSeperation()
        {
            
            foreach (SteeringMind m in neighbours)
            {
                if (m != this)
                {
                    if (Vector2.Distance(position, m.position) < 50)
                    {
                        velocity += m.getPosition() - position;
                        neighbourCount++;
                    }
                }
            }

            if (neighbourCount == 0)
            {
                return velocity;
            }

            velocity.X /= neighbourCount;
            velocity.Y /= neighbourCount;
            velocity.X *= -1;
            velocity.Y *= -1;
            velocity.Normalize();
            return velocity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 calcCohesion()
        {
            foreach (SteeringMind m in neighbours)
            {
                if (m != this)
                {
                    if (Vector2.Distance(position, m.position) < 50)
                    {
                        velocity += m.getPosition();
                        neighbourCount++;
                    }
                }
            }

            if (neighbourCount == 0)
                return velocity;

            velocity.X /= neighbourCount;
            velocity.Y /= neighbourCount;

            velocity = new Vector2(velocity.X - position.X, velocity.Y - position.Y);
            velocity.Normalize();
            return velocity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 calcAlignment()
        {
            foreach (SteeringMind m in neighbours)
            {
                if (m != this)
                {
                    if (Vector2.Distance(position, m.position) < 50)
                    {
                        velocity += m.getVelocity();
                        neighbourCount++;
                    }
                }
            }

            if (neighbourCount == 0)
                return velocity;

            velocity.X /= neighbourCount;
            velocity.Y /= neighbourCount;
            velocity.Normalize();
            return velocity;
        }

        public void setNeighbourhood(List<SteeringMind> list)
        {
            neighbours = list;
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

        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
            return;
        }
        public Vector2 getVelocity()
        {
            return velocity;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getNormalPos()
        {
            return Vector2.Normalize(position);
        }

        public Vector2 getNormalVel()
        {
            return Vector2.Normalize(velocity);

        }

        public Vector2 getNormalDesVel()
        {
            return Vector2.Normalize(DesiredVelocity);
        }

        public Vector2 getNormalSteering()
        {
            return Vector2.Normalize(steeringForce);
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
