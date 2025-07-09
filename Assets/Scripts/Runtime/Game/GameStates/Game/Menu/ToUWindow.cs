using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class ToUWindow : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;

        private UI.Screen.ToUWindow _screen;

        public ToUWindow(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            MakeWindow();
            Sub();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.TermsOfUseWindow);
        }

        private void MakeWindow()
        {
            _screen = _userInterfaceHelper.RetrieveWindow<UI.Screen.ToUWindow>(WindowNames.TermsOfUseWindow);
            _screen.Setup();
            _screen.Reveal().Forget();
        }

        private void Sub()
        {
            _screen.OnBackPressed += async () => await SwitchTo<MenuWindow>();
        }
    }
}