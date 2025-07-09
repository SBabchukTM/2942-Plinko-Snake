using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.GameStates.Game.Menu;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Popup;
using UnityEngine;

namespace Runtime.Game.GameStates.Game
{
    public class PausePopupState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
    
        public PausePopupState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
        }

        public override async UniTask Switch(CancellationToken cancellationToken = default)
        {
            Time.timeScale = 0;
        
            PausePopup popup = await _userInterfaceHelper.ShowPopup(ProjectPopupNames.PausePopupName) as PausePopup;

            popup.OnHomePressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await SwitchTo<MainWindowState>();
            };

            popup.OnReturnPressed += () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
            };
        }
    }
}
