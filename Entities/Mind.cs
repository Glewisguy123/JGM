using ADS.Collision.SAT;
using Engine.Events.CollisionEvent;
using Engine.Managers.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADS;

namespace Engine.Entities
{
    public abstract class Mind : IMind, ICollidable
    {
        //String to the texture 

        //Minds unique ID (Always set to be the same as the entity its possessing)
        public int UniqueID { get; set; }
        //Check whether the object is active or not for updating and drawing
        public bool Active { get; set; }
        //Instance variable for the entity
        protected IEntity e;

        //Rate of change of position
        protected Vector2 velocity;

        public Vector2 Velocity {get{ return velocity; } set { velocity = value; } }

        protected Vector2 acceleration = new Vector2(0, 0);
        public Vector2 Acceleration { get { return acceleration; } set { acceleration = value; } }

        protected float iMass;
        public float Mass { get { return iMass; } set { iMass = value; } }

        protected float restitution;
        public float Restitution { get { return restitution; } set { restitution = value; } }

        protected float damping;
        public float Damping { get { return damping; } set { damping = value; } }

        protected List<Hitbox> hits = new List<Hitbox>();
        public List<Hitbox> Hits { get { return hits; } set { hits = value; } }

        public bool isColliding { get; set; }


        protected Vector2 _pos = new Vector2();
        public Vector2 Position { get { return _pos; } set { _pos = value; } }

        //Rate of change of velocity
      
        protected string texPath = "";

        public Rectangle Bounds { get { return new Rectangle((int)Position.X, (int)Position.Y, e.Texture.Width, e.Texture.Height); } }

        public bool isCollidable { get; set; }


        public Mind()
        {
            
        }

        /// <summary>
        /// Returns the minds possessed IEntity 
        /// </summary>
        /// <returns></returns>
        public IEntity getEntity()
        {
            return e;
        }

        /// <summary>
        /// Returns itself as an ICollidable (For collision management)
        /// </summary>
        /// <returns></returns>
        public ICollidable getCollidable()
        {
            return this;
        }

        public virtual void Initialize( Vector2 Position,string t)
        {
            //this.e = E;
            UniqueID = e.UniqueID;
            setTexture(t);
            e.Position = Position;
            e.isVisible = true;
            Active = true;

        }

        public virtual void Initialize(Vector2 Position)
        {

            Locator.Instance.getService<DetectionManger>().OnSATCollision += OnSATCollision;

            //this.e = E;
            UniqueID = e.UniqueID;
            if(texPath != null)
            {
                setTexture(texPath);
            }
                e.Position = Position;
            _pos = Position;
            e.isVisible = true;
            Active = true;

        }

        public virtual void OnSATCollision(object sender, CollisionEventArgs e)
        {
            if(e.A== this)
            velocity += (e.mtvRet * iMass);
            else if (e.B == this)
                velocity -= (e.mtvRet * iMass);

        }

        public virtual void Unload()
        {

        }
        public void setTexture(string t)
        {
            e.Texture =  Locator.Instance.getService<IResourceLoader>().GetTex(t);
        }


        public virtual void Update(GameTime gameTime)
        {
           e.Position = _pos;
            if(!Active)
            {
                e.isVisible = false;
            }

         

            for (int i = 0; i < Hits.Count; i++)
            {
                Hits[i].Velocity = Velocity;
            }

            for (int i = 0; i < Hits.Count; i++)
            {
                Hits[i].UpdatePoint(Velocity);
            }
        }

        public void Link(IEntity e)
        {
            this.e = e;
        }

        #region GrantsMethods
        public void ApplyForce(Vector2 force)
        {
            Acceleration += (force * Mass);
        }

        public void UpdatePhysics()
        {
            Velocity *= Damping;
            Velocity += Acceleration;
            _pos += Velocity;
            Acceleration = Vector2.Zero;
        }

        public void ApplyImpulse(Vector2 cVelocity)
        {
            cVelocity *= Restitution;
            Acceleration += ((cVelocity * Mass) * Damping);
        }
        #endregion
    }
}
