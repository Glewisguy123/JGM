using ADS;
using ADS.Entities.Platformer;
using ADS.Entities.Steering;
using Engine;
using Engine.Entities;
using Engine.Managers.EntityRelated;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class SATDemo : BaseScreen
    {
        public override void Initialize()
        {
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<pEntity>(Vector2.Zero);

            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<TestEntity>(new Vector2(50, 50));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<TestEntity>(new Vector2(100, 50));
            Locator.Instance.getService<EntityManager>().createEntityCamDrawable<TestEntity>(new Vector2(150, 50));
            base.Initialize();
        }

        
    }
}
