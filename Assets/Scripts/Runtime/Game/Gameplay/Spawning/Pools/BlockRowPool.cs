using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Gameplay.Spawning.BlockRows;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Gameplay.Spawning.Pools
{
    public class BlockRowPool : BaseItemPool<RowOfBlocks>
    {
        private const int PoolSize = 5;
        
        private readonly SpritesProvider _spritesProvider;
        
        public BlockRowPool(IObjectGetterService objectGetterService, GameObjectFactory gameObjectFactory, ManagerOfSystems managerOfSystems, SpritesProvider spritesProvider) : 
            base(objectGetterService, gameObjectFactory,  managerOfSystems, PrefabNames.BlockRowPrefab, PoolSize)
        {
            _spritesProvider = spritesProvider;
        }

        public override RowOfBlocks GetItem()
        {
            var item = base.GetItem();
            item.Initialize(_spritesProvider.GetBlockSkin());
            return item;
        }

        public override Vector3 GetItemSize() => new (30, 8f, 0);
    }
}