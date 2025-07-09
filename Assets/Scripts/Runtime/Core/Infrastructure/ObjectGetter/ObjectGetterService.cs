using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Runtime.Core.Infrastructure.ObjectGetter
{
    public class ObjectGetterService : IObjectGetterService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles =
            new Dictionary<string, List<AsyncOperationHandle>>();

        public async UniTask Initialize()
        {
            await Addressables.InitializeAsync();
        }

        public async UniTask<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), cacheKey: address);
        }

        public UniTask<GameObject> Create(string address) => Addressables.InstantiateAsync(address).ToUniTask();

        private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle => { _completedCache[cacheKey] = completeHandle; };

            AddHandle<T>(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            var resourceHandles = Process1<T>(key);

            resourceHandles.Add(handle);
        }

        private List<AsyncOperationHandle> Process1<T>(string key) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            return resourceHandles;
        }
    }
}