using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.Core.Infrastructure.ObjectGetter
{
    public interface IObjectGetterService : IInitializer
    {
        UniTask<T> Load<T>(string address) where T : class;
        UniTask<GameObject> Create(string address);
    }
}