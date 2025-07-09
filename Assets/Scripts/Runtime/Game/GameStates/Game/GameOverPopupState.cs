using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Gameplay;
using Runtime.Game.GameStates.Game.Menu;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Popup;
using UnityEngine;

namespace Runtime.Game.GameStates.Game
{
    public class GameOverPopupState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly GameData _gameData;
    
        public GameOverPopupState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, GameData gameData) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _gameData = gameData;
        }

        public override async UniTask Switch(CancellationToken cancellationToken = default)
        {
            Time.timeScale = 0;
            GameOverPopup popup = await _userInterfaceHelper.ShowPopup(ProjectPopupNames.LosePopupName) as GameOverPopup;

            popup.SetData(_gameData.CoinsCollected, _gameData.GameLevel);
        
            popup.OnHomeButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await SwitchTo<MainWindowState>();
            };

            popup.OnRetryButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await SwitchTo<GameplayWindowState>();
            };
        }
    }
}
