using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class MenuWindow : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;

        private UI.Screen.MenuWindow _window;

        public MenuWindow(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.MenuWindow);
        }

        private void CreateScreen()
        {
            _window = _userInterfaceHelper.RetrieveWindow<UI.Screen.MenuWindow>(WindowNames.MenuWindow);
            _window.Setup();
            _window.Reveal().Forget();
        }

        private void SubscribeToEvents()
        {
            _window.OnBackPressed += async () => await SwitchTo<MainWindowState>();
            _window.OnPrivacyPressed += async () => await SwitchTo<PrivacyPolicyState>();
            _window.OnProfilePressed += async () => await SwitchTo<ProfileState>();
            _window.OnSettingsPressed += async () => await SwitchTo<SettingsScreenState>();
            _window.OnTermsOfUsePressed += async () => await SwitchTo<ToUWindow>();
        }
    }
}