using Cysharp.Threading.Tasks;

namespace Runtime.Core
{
    public interface IInitializer
    {
        UniTask Initialize();
    }
}