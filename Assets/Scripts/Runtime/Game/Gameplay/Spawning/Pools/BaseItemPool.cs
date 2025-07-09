using System.Collections.Generic;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Gameplay.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Spawning.Pools
{
    public abstract class BaseItemPool<T> : ICleanup, IInitializable where T : Component
    {
        protected readonly IObjectGetterService ObjectGetterService;
        protected readonly GameObjectFactory GameObjectFactory;
    
        private GameObject _itemPrefab;
        private string _prefabName;
        private int _poolSize;

        private List<T> _itemPool;

        public BaseItemPool(IObjectGetterService objectGetterService, GameObjectFactory gameObjectFactory, ManagerOfSystems managerOfSystems, string prefabName, int poolSize)
        {
            ObjectGetterService = objectGetterService;
            GameObjectFactory = gameObjectFactory;
            _prefabName = prefabName;
            _poolSize = poolSize;
        
            managerOfSystems.Register(this);
        }

        public virtual async void Initialize()
        {
            _itemPrefab = await ObjectGetterService.Load<GameObject>(_prefabName);

            Setup();
        }

        private void Setup()
        {
            _itemPool = new (_poolSize);

            for (int i = 0; i < _poolSize; i++)
                _itemPool.Add(MakeItem());
        }

        public virtual T GetItem()
        {
            T item = null;

            if (_itemPool.Count > 0)
            {
                item = _itemPool[0];
                _itemPool.RemoveAt(0);
            }
            else
                item = MakeItem();
        
            item.gameObject.SetActive(true);
        
            return item;
        }

        public void Cleanup() => FindAndReturnAll();

        private void FindAndReturnAll()
        {
            T[] itemsFound = Object.FindObjectsOfType<T>(false);

            foreach (var item in itemsFound)
                ReturnToPool(item);
        }
    
        public virtual void ReturnToPool(T item)
        {
            item.gameObject.SetActive(false);
            _itemPool.Add(item);
        }

        private T MakeItem()
        {
            var item = GameObjectFactory.Create<T>(_itemPrefab);
            item.gameObject.SetActive(false);
            return item;
        }

        public abstract Vector3 GetItemSize();
    }
}
