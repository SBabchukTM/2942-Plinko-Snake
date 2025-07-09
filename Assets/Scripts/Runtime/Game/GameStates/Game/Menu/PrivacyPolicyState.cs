using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class PrivacyPolicyState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;

        private PrivacyPolicyWindow _window;

        public PrivacyPolicyState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper) : base(debugger)
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
            await _userInterfaceHelper.HideWindow(WindowNames.PrivacyPolicyWindow);
        }

        private void CreateScreen()
        {
            _window = _userInterfaceHelper.RetrieveWindow<PrivacyPolicyWindow>(WindowNames.PrivacyPolicyWindow);
            _window.Setup();
            _window.Reveal().Forget();
        }

        private void SubscribeToEvents()
        {
            _window.OnBackPressed += async () => await SwitchTo<MenuWindow>();
        }
    }
}