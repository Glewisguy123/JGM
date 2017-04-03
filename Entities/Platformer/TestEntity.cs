using Engine.Entities;
using Engine.Managers.Behaviour;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Entities.Platformer
{
    class TestEntity : Entity
    {
        public override void Initialize(Vector2 Pos)
        {
            mind = Locator.Instance.getService<BehaviourManager>().Create<TestMind>(this);
            base.Initialize(Pos);
       
            this.Name = "TestEntity";
        }
    }
}
