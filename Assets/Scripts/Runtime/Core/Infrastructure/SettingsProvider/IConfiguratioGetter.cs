using Cysharp.Threading.Tasks;

namespace Runtime.Core.Infrastructure.SettingsProvider
{
    public interface IConfiguratioGetter
    {
        UniTask Initialize();
        T Get<T>() where T : BaseConfigSO;
    }
}