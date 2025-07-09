using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Gameplay.Items;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Gameplay.Spawning.Pools
{
    public class ObstaclePool : BaseItemPool<Obstacle>
    {
        private const int PoolSize = 10;
        
        private readonly SpritesProvider _spritesProvider;
        
        public ObstaclePool(IObjectGetterService objectGetterService, GameObjectFactory gameObjectFactory, SpritesProvider spritesProvider, ManagerOfSystems managerOfSystems) : 
            base(objectGetterService, gameObjectFactory, managerOfSystems, PrefabNames.ObstaclePrefab, PoolSize)
        {
            _spritesProvider = spritesProvider;
        }

        public override Obstacle GetItem()
        {
            var item = base.GetItem();
            item.SetSkin(_spritesProvider.GetLongObstacleSkin());
            return item;
        }

        public override Vector3 GetItemSize() => new(5, 15, 0);
    }
}