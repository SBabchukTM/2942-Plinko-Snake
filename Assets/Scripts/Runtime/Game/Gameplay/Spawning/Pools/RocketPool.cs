using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Gameplay.Items;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Gameplay.Spawning.Pools
{
    public class RocketPool : BaseItemPool<Rocket>
    {
        private const int PoolSize = 5;
        public RocketPool(IObjectGetterService objectGetterService, GameObjectFactory gameObjectFactory, ManagerOfSystems managerOfSystems) : 
            base(objectGetterService, gameObjectFactory, managerOfSystems, PrefabNames.RocketPrefab, PoolSize)
        {
        }
        
        public override Vector3 GetItemSize() => Vector3.one * 6f;
    }
}