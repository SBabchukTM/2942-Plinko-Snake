using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Achievements;
using Runtime.Game.Gameplay;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.Leaderboard;
using Runtime.Game.Services.Audio;
using Runtime.Game.Services.UI;
using Runtime.Game.ShopSystem;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class GameplayWindowState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly GameData _gameData;
        private readonly InventoryHelper _inventoryHelper;
        private readonly GameSetupController _gameSetupController;
        private readonly ScoresService _scoresService;
        private readonly GameOverPopupState _gameOverPopupState;
        private readonly PausePopupState _pausePopupState;

        private readonly ISoundService _soundService;
        
        private GameplayWindow _window;

        public GameplayWindowState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, GameSetupController gameSetupController,
            InventoryHelper inventoryHelper, GameData gameData, ScoresService scoresService,
            GameOverPopupState gameOverPopupState, NodeManager nodeManager,
            PausePopupState pausePopupState, ISoundService soundService) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _inventoryHelper = inventoryHelper;
            _gameSetupController = gameSetupController;
            _gameData = gameData;
            _scoresService = scoresService;
            _gameOverPopupState = gameOverPopupState;
            _pausePopupState = pausePopupState;
            _soundService = soundService;

            nodeManager.OnSnakeDestroyed += ProcessGameEnd;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            StartGame();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            _gameSetupController.End();
            await _userInterfaceHelper.HideWindow(WindowNames.GameplayWindow);
        }

        public void OnCollision() => ProcessGameEnd();

        private void CreateScreen()
        {
            _window = _userInterfaceHelper.RetrieveWindow<GameplayWindow>(WindowNames.GameplayWindow);
            _window.Setup();
            _window.Reveal().Forget();
        }

        private void SubscribeToEvents()
        {
            _window.OnPausePressed += () => _pausePopupState.Switch().Forget();
        }

        private void StartGame()
        {
            _gameSetupController.Setup();
        }

        private void ProcessGameEnd()
        {
            _inventoryHelper.AddCoins(_gameData.CoinsCollected);
            _scoresService.RecordScore(_gameData.GameLevel);
            _gameOverPopupState.Switch().Forget();
            
            _soundService.PlaySound(ConstAudioNames.LoseSound);
            AccomplishmentsEventInvoker.InvokeOnGameFinished();
        }
    }
}