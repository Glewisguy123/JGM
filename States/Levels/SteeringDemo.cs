using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine.Managers.EntityRelated;
using Engine.Entities;
using ADS.Entities.Steering;
using Engine.Events.KeyboardEvent;
using ADS;

namespace Engine
{
    public enum DemoState { Seek, Flee, Arrival, Persuit, Evade }

    class SteeringDemo : BaseScreen
    {

        private bool flock = false;
        public override void Initialize()
        {
           
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<pEntity>(Vector2.Zero);

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-300, -300));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-450, -450));

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-600, -300));

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-700, -450));

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-800, -550));

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-370, -480));

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<SteeringEntity>(new Vector2(-100, -500));

            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyPressed;

            base.Initialize();
        }

        public void OnKeyPressed(object source, KeyEventArgs e)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
