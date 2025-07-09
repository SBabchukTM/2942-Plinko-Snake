using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Leaderboard;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class ScoresWindowState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly RecordsFactory _recordsFactory;

        private LeaderboardWindow _window;

        public ScoresWindowState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, RecordsFactory recordsFactory) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _recordsFactory = recordsFactory;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            MakeWindow();
            Sub();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.LeaderboardWindow);
        }

        private void MakeWindow()
        {
            _window = _userInterfaceHelper.RetrieveWindow<LeaderboardWindow>(WindowNames.LeaderboardWindow);
            _window.Setup(_recordsFactory.CreateRecords());
            _window.Reveal().Forget();
        }

        private void Sub()
        {
            _window.OnBackPressed += async () => await SwitchTo<MainWindowState>();
            _window.OnMenuPressed += async () => await SwitchTo<MenuWindow>();
        }
    }
}