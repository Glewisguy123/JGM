using ADS.Collision.SAT;
using Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers.Collision
{
    public interface ICollidable
    {
       Rectangle Bounds { get; }

       Vector2 Position { get; set; }

     //  Vector2 Velocity { get; set; }

       bool isCollidable { get; set; }

       bool isColliding { get; set; }

        void ApplyImpulse(Vector2 cVelocity);
        List<Hitbox> Hits { get; set; }




    }
}
