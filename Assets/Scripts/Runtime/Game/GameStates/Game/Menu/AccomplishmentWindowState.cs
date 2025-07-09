using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Achievements;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class AccomplishmentWindowState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly AchievementsFactory _achievementsFactory;

        private AchievementsWindow _window;

        public AccomplishmentWindowState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, AchievementsFactory achievementsFactory) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _achievementsFactory = achievementsFactory;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            MakeWindow();
            Sub();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.AchievementsWindow);
        }

        private void MakeWindow()
        {
            _window = _userInterfaceHelper.RetrieveWindow<AchievementsWindow>(WindowNames.AchievementsWindow);
            _window.Setup(_achievementsFactory.GetAchievements());
            _window.Reveal().Forget();
        }

        private void Sub()
        {
            _window.OnBackPressed += async () => await SwitchTo<MainWindowState>();
            _window.OnMenuPressed += async () => await SwitchTo<MenuWindow>();
        }
    }
}