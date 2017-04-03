using Engine.Managers.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Events.CollisionEvent
{
    class SATEventArgs
    {
        //entity
        public ICollidable A { get; set; }
        //Object in which the entity is colliding with
        public ICollidable B { get; set; }

        public Vector2 mtvRet { get; set; }

    }
}
