using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class MainWindowState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;

        private MainWindow _window;

        public MainWindowState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper) : base(debugger)
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
            await _userInterfaceHelper.HideWindow(WindowNames.MainWindow);
        }

        private void CreateScreen()
        {
            _window = _userInterfaceHelper.RetrieveWindow<MainWindow>(WindowNames.MainWindow);
            _window.Setup();
            _window.Reveal().Forget();
        }

        private void SubscribeToEvents()
        {
            _window.OnMenuPressed += async () => await SwitchTo<MenuWindow>();
            _window.OnAchPressed += async () => await SwitchTo<AccomplishmentWindowState>();
            _window.OnDailyPressed += async () => await SwitchTo<BonusWindowState>();
            _window.OnLeaderPressed += async () => await SwitchTo<ScoresWindowState>();
            _window.OnShopPressed += async () => await SwitchTo<ShopScreenState>();
            _window.OnStartPressed += async () => await SwitchTo<GameplayWindowState>();
            _window.OnHelpPressed += async () => await _userInterfaceHelper.ShowPopup(ProjectPopupNames.HelpPopupName);
        }
    }
}