using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Gameplay.Spawning.Pools
{
    public class NodesPool : BaseItemPool<Node>
    {
        private const int PoolSize = 10;

        private readonly SpritesProvider _spritesProvider;
    
        private Node _head;
    
        public NodesPool(IObjectGetterService objectGetterService, GameObjectFactory gameObjectFactory, 
            SpritesProvider spritesProvider, ManagerOfSystems managerOfSystems) : 
            base(objectGetterService, gameObjectFactory, managerOfSystems, PrefabNames.NodePrefab, PoolSize)
        {
            _spritesProvider = spritesProvider;
        }

        public override async void Initialize()
        {
            base.Initialize();
            _head = GameObjectFactory.Create<Node>(await ObjectGetterService.Load<GameObject>(PrefabNames.SnakeControllerPrefab));
            ReturnSnakeHead();
        }

        public Node GetSnakeHead()
        {
            _head.SetSkin(_spritesProvider.GetBallSkin());
            _head.gameObject.SetActive(true);
            return _head;
        }

        public void ReturnSnakeHead()
        {
            _head.gameObject.SetActive(false);
        }
    
        public override Node GetItem()
        {
            var node = base.GetItem();
            node.SetSkin(_spritesProvider.GetBallSkin());
            return node;
        }
    
        public override Vector3 GetItemSize() => Vector3.one * 6f;
    }
}
