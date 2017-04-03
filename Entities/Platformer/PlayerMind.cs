using ADS;
using ADS.Collision.SAT;
using ADS.Entities;
using ADS.Managers.High_Tier.Collision;
using Engine.Events.CollisionEvent;
using Engine.Events.KeyboardEvent;
using Engine.Events.MouseEvent;
using Engine.Managers.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    //This is the blueprint for medications player
    public class PlayerMind : Mind
    {
        //Player Stats
        //Timer for bullets
        int bulletTimer = 0;
        //Dunno why this is here atm
        GameTime GameTime;
        //Check for input
        bool input = true;

        float maxSpeed = 3f;
        float moveSpeed = 0.2f;

        private Vector2 friction = new Vector2(0.1f, 0.1f);

        bool left = true, right = true, up = true, down = true;

        public PlayerMind()
        {
            
            //Create player stats
            isCollidable = true;
            //Sign up to handlers
            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseDown;
            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
            Locator.Instance.getService<KeyHandler>().KeyHeld += OnKeyHeld;
            Locator.Instance.getService<DetectionManger>().OnCollision += OnCollision;
            
        }

      

        public override void Initialize(Vector2 Position)
        {
            texPath = "AntiBody";

            Mass = 0.5f;
            Restitution = 1f;
            Damping = 0.97f;
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y + 3), 16, 6, 45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 16, Position.Y + 3), 16, 6, -45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 12, Position.Y + 16), 4, 16, 0, this));
            base.Initialize(Position);
        }


        public override void Update(GameTime gameTime)
        {

            if (bulletTimer > 0)
                bulletTimer = bulletTimer-1;
            GameTime = gameTime;
            //  applyVelocityRules();
            // Friction();
            /*
            RenderManager.Instance.addString(new ADS.Utilities.GameText("EXP = " + stats.EXP.ToString(), "Snap", new Vector2(0, 75), Color.Green, 1.1f));
            RenderManager.Instance.addString(new ADS.Utilities.GameText("Level = " + stats.LEVEL.ToString(), "Snap", new Vector2(0, 100), Color.Green, 1.1f));

            RenderManager.Instance.addString(new ADS.Utilities.GameText("HP = " + stats.HP.ToString(), "Snap", new Vector2(0, 0), Color.Green, 1.1f));
            RenderManager.Instance.addString(new ADS.Utilities.GameText("DMG = " + stats.DMG.ToString(), "Snap", new Vector2(0, 250), Color.Green, 1.1f));
            RenderManager.Instance.addString(new ADS.Utilities.GameText("aSPD = " + stats.aSPD.ToString(), "Snap", new Vector2(0, 300), Color.Green, 1.1f));
            RenderManager.Instance.addString(new ADS.Utilities.GameText("mSPD = " + stats.mSPD.ToString(), "Snap", new Vector2(0, 350), Color.Green, 1.1f));
            */
            UpdatePhysics();
            // _pos += velocity;
            base.Update(gameTime);

        }


        public override void Unload()
        {
            Locator.Instance.getService<KeyHandler>().KeyDown -= OnKeyDown;
            Locator.Instance.getService<KeyHandler>().KeyHeld -= OnKeyHeld;
            Locator.Instance.getService<MouseHandler>().MouseClick -= OnMouseDown;
        }

        public void Friction()
        {
            //I have no idea how this works, but it simulates friction
            Velocity -= Velocity * new Vector2(.1f, .1f);
        }



        public void OnKeyDown(object sender, KeyEventArgs m)
        {
       
          




            //pseudo
            // EntityManager.Instance.createProjectileCamDrawable<bulletEntity>(Position, "bullet", this.DIRECTION);


        }

        public void OnKeyHeld(object sender, KeyEventArgs m)
        {
            if (input)
            {

                Keys[] keys = m.keyState.GetPressedKeys();

                if (m.key == Keys.D && right)
                {
                    velocity.X += moveSpeed;

                    //  e.Position = new Vector2(e.Position.X + stats.mSPD, e.Position.Y);// *(float)GameTime.ElapsedGameTime.TotalMilliseconds;
                    //  velocity.X += Acceleration.X *(float)GameTime.ElapsedGameTime.TotalMilliseconds;
                }
                if (m.key == Keys.A && left)
                {
                    velocity.X -= moveSpeed;

                    // e.Position = new Vector2(e.Position.X - stats.mSPD, e.Position.Y);// *(float)GameTime.ElapsedGameTime.TotalMilliseconds;
                    //   velocity.X -= Acceleration.X * (float)GameTime.ElapsedGameTime.TotalMilliseconds;

                }
                if (m.key == Keys.S)
                {
                    velocity.Y += moveSpeed;

                    //  e.Position = new Vector2(e.Position.X , e.Position.Y+ stats.mSPD);// *(float)GameTime.ElapsedGameTime.TotalMilliseconds;
                    //  velocity.Y += Acceleration.Y * (float)GameTime.ElapsedGameTime.TotalMilliseconds;

                }

                if (m.key == Keys.W)
                {
                      velocity.Y -= moveSpeed;
                   // e.Position = new Vector2(e.Position.X, e.Position.Y - stats.mSPD);// *(float)GameTime.ElapsedGameTime.TotalMilliseconds;

                    //  velocity.Y -= Acceleration.Y * (float)GameTime.ElapsedGameTime.TotalMilliseconds;
                }

                if ((m.keyState.IsKeyDown(Keys.Up) && m.keyState.IsKeyDown(Keys.Space)))
                {

                    

                  

                
                }

             

                    if ((m.keyState.IsKeyDown(Keys.Down) && m.keyState.IsKeyDown(Keys.Space)))
                {
                    
                }


                if ((m.keyState.IsKeyDown(Keys.Left) && m.keyState.IsKeyDown(Keys.Space)))
                {
                    
                    

                }


                if ((m.keyState.IsKeyDown(Keys.Right) && m.keyState.IsKeyDown(Keys.Space)))
                {
                    
                }
            //    velocity *= friction;
             //   _pos.Y += velocity.Y;


                ////Console.WriteLine(bulletTimer);
                //if (bulletTimer == 0)
                //{
                //    switch (m.key)
                //    {
                //        case Keys.Left:
                //            bulletTimer = stats.aSPD;
                //            EntityManager.Instance.createProjectile<Projectile>(new Vector2(this.Bounds.Center.X, this.Bounds.Center.Y), "bullet1", ADS.Entities.Direction.left);
                //            break;
                //        case Keys.Right:
                //            bulletTimer = stats.aSPD;
                //            EntityManager.Instance.createProjectile<Projectile>(new Vector2(this.Bounds.Center.X, this.Bounds.Center.Y), "bullet1", ADS.Entities.Direction.right);
                //            break;
                //        case Keys.Up:
                //            bulletTimer = stats.aSPD;
                //            EntityManager.Instance.createProjectile<Projectile>(new Vector2(this.Bounds.Center.X, this.Bounds.Center.Y), "bullet1", ADS.Entities.Direction.up);
                //            break;
                //        case Keys.Down:
                //            bulletTimer = stats.aSPD;
                //            EntityManager.Instance.createProjectile<Projectile>(new Vector2(this.Bounds.Center.X, this.Bounds.Center.Y), "bullet1", ADS.Entities.Direction.down);
                //            break;





                //    }

                //}







            }

        }

        public void OnMouseDown(object sender, MouseEventArgs m)
        {
        }

        public void OnCollision(object sender, CollisionEventArgs cae )
        {
           
            //Position
            if (cae.A == this)
            {

                isColliding = true;
                _pos += TranslationVector.GetMinimumTranslation(cae.A, cae.B);
                
                

            }

            


        }

        


        public void applyVelocityRules()
        {
            if (velocity.X > maxSpeed)
            {
                velocity.X = maxSpeed;
            }

            if (velocity.X < -maxSpeed)
            {
                velocity.X = -maxSpeed;
            }

            if (velocity.Y > maxSpeed)
            {
                velocity.Y = maxSpeed;
            }

            if (velocity.Y < -maxSpeed)
            {
                velocity.Y = -maxSpeed;
            }
        }
    }
}
              
    


