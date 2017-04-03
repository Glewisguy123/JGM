using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS
{
    interface IServiceLocator
    {

       IServiceLocator Instance { get; }

        T getService<T>();

        void InitializeServices(ContentManager c, SpriteBatch sb);

        void UpdateServices(GameTime gameTime);

    }
}
