using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core.Infrastructure.ObjectGetter;
using UnityEngine;
using Zenject;

namespace Runtime.Core.Factory
{
    public class GameObjectFactory
    {
        private readonly Dictionary<string, GameObject> _cachedAddressables;
        private readonly DiContainer _container;
        private readonly IObjectGetterService _objectGetterService;

        public GameObjectFactory(DiContainer container, IObjectGetterService objectGetterService)
        {
            _cachedAddressables = new Dictionary<string, GameObject>();
            _container = container;
            _objectGetterService = objectGetterService;
        }

        public TComponent Create<TComponent>(GameObject prototype) where TComponent : Component
        {
            return _container.InstantiatePrefabForComponent<TComponent>(prototype);
        }

        public TComponent Create<TComponent>(GameObject prototype, Transform parent) where TComponent : Component
        {
            return _container.InstantiatePrefabForComponent<TComponent>(prototype, parent);
        }
    }
}