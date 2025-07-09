using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.GameStates.Game.Menu;

namespace Runtime.Game.GameStates.Game
{
    public class GameState : State
    {
        private readonly StateController _stateController;

        private readonly AccomplishmentWindowState _accomplishmentWindowState;
        private readonly BonusWindowState _bonusWindowState;
        private readonly GameplayWindowState _gameplayWindowState;
        private readonly ScoresWindowState _scoresWindowState;
        private readonly MainWindowState _mainWindowState;
        private readonly MenuWindow _menuWindow;
        private readonly PrivacyPolicyState _privacyPolicyState;
        private readonly ProfileState _profileState;
        private readonly SettingsScreenState _settingsScreenState;
        private readonly ShopScreenState _shopScreenState;
        private readonly ToUWindow _toUWindow;
        private readonly ApplicationStateEventsHeloer _applicationStateEventsHeloer;
        private readonly RewardPopupState _rewardPopupState;
        private readonly GameOverPopupState _gameOverPopupState;
        private readonly PausePopupState _pausePopupState;

        public GameState(IDebugger debugger,
            AccomplishmentWindowState accomplishmentWindowState,
            BonusWindowState bonusWindowState,
            GameplayWindowState gameplayWindowState,
            ScoresWindowState scoresWindowState,
            MainWindowState mainWindowState,
            MenuWindow menuWindow,
            PrivacyPolicyState privacyPolicyState,
            ProfileState profileState,
            SettingsScreenState settingsScreenState,
            ShopScreenState shopScreenState,
            ToUWindow toUWindow,
            RewardPopupState rewardPopupState,
            GameOverPopupState gameOverPopupState,
            PausePopupState pausePopupState,
            StateController stateController,
            ApplicationStateEventsHeloer applicationStateEventsHeloer) : base(debugger)
        {
            _stateController = stateController;
            _accomplishmentWindowState = accomplishmentWindowState;
            _bonusWindowState = bonusWindowState;
            _gameplayWindowState = gameplayWindowState;
            _scoresWindowState = scoresWindowState;
            _mainWindowState = mainWindowState;
            _menuWindow = menuWindow;
            _privacyPolicyState = privacyPolicyState;
            _profileState = profileState;
            _settingsScreenState = settingsScreenState;
            _shopScreenState = shopScreenState;
            _toUWindow = toUWindow;
            _rewardPopupState = rewardPopupState;
            _gameOverPopupState = gameOverPopupState;
            _pausePopupState = pausePopupState;
            _applicationStateEventsHeloer = applicationStateEventsHeloer;
        }

        public override async UniTask Switch(CancellationToken cancellationToken)
        {
            await _applicationStateEventsHeloer.Perform(default);

            _stateController.Setup(_accomplishmentWindowState, _bonusWindowState, 
                _gameplayWindowState, _scoresWindowState,
                _mainWindowState, _menuWindow, _privacyPolicyState,
                _profileState, _settingsScreenState,
                _shopScreenState, _toUWindow, _rewardPopupState,
                _gameOverPopupState, _pausePopupState);
            
            _stateController.EnterState<MainWindowState>().Forget();
        }
    }
}