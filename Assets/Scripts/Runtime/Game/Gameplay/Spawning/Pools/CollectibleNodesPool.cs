using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Gameplay.Items;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Gameplay.Spawning.Pools
{
    public class CollectibleNodesPool : BaseItemPool<CollectibleNode>
    {
        private const int PoolSize = 10;
        private readonly SpritesProvider _spritesProvider;
        
        public CollectibleNodesPool(IObjectGetterService objectGetterService, 
            GameObjectFactory gameObjectFactory, SpritesProvider spritesProvider,ManagerOfSystems managerOfSystems) : 
            base(objectGetterService, gameObjectFactory, managerOfSystems, PrefabNames.CollectibleNodePrefab, PoolSize)
        {
            _spritesProvider = spritesProvider;
        }

        public override CollectibleNode GetItem()
        {
            var node = base.GetItem();
            node.SetSkin(_spritesProvider.GetBallSkin());
            return node;
        }
        
        public override Vector3 GetItemSize() => Vector3.one * 6f;
    }
}