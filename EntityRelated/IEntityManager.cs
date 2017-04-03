using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Managers.High_Tier.EntityRelated
{
    interface IEntityManager
    {
        List<IEntity> getList();
        List<IEntity> getCamList();
        IEntity getPlayer();
        void addEntity(IEntity e);
        void addCamEntity(IEntity e);
        IEntity createEntity<T>(Vector2 Position) where T : IEntity, new();
        IEntity createEntityCamDrawable<T>(Vector2 Position) where T : IEntity, new();
        IEntity createEntityDrawable<T>(Vector2 Position) where T : IEntity, new();

        void tempCamClear();
        void getEntity(string name);
        IEntity getCamEntity(string name);
        void clearList();
        void removeEntity(int entityID);
        void removeCamEntity(int entityID);
        void Update(GameTime gameTime);
    }
}
