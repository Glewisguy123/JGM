﻿using Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ADS.Collision.SAT;

namespace ADS.Entities.Platformer
{
    class TestMind : Mind
    {

        public override void Initialize(Vector2 Position)
        {
            texPath = "AntiBody";

            Mass = 0.3f;
            Restitution = 0.00001f;
            Damping = 0.97f;
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y + 3), 16, 7, 45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 16, Position.Y + 3), 16, 7, -45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 12, Position.Y + 16), 8, 17, 0, this));

            base.Initialize(Position);
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePhysics();
            base.Update(gameTime);
        }
    }
}