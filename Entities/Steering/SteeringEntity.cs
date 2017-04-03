using Engine.Entities;
using Engine.Managers.Behaviour;
using Engine.Managers.CamManage;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Entities.Steering
{
    class SteeringEntity : Entity
    {

        public override void Initialize(Vector2 Pos)
        {
            mind = Locator.Instance.getService<BehaviourManager>().Create<SteeringMind>(this);
            base.Initialize(Pos);
          //  CameraManager.Instance.getCam().setEntity(this, "Locked");

            this.Name = "Steering";
        }
    }
}
