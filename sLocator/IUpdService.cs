using ADS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Managers
{
    interface IUpdService : IService
    {
        void Update(GameTime gameTime);
    }
}
