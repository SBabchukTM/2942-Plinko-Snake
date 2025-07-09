using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Popup;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.Services.UI
{
    public interface IUserInterfaceHelper
    {
        UniTask Setup();
        T RetrieveWindow<T>(string id) where T : BaseWindow;
        UniTask ShowWindow(string id, CancellationToken cancellationToken = default);
        UniTask HideWindow(string id, CancellationToken cancellationToken = default);
        UniTask<BasePopup> ShowPopup(string id, CancellationToken cancellationToken = default);
        T GetPopup<T>(string id) where T : BasePopup;
    }
}