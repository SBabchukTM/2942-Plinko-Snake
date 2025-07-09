using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.GameStates.Game;
using Runtime.Game.Services.ScreenOrientation;
using Runtime.Game.Services.UI;
using Runtime.Game.Services.UserData;
using UnityEngine;
using GameState = Runtime.Game.GameStates.Game.GameState;

namespace Runtime.Game.GameStates.Setup
{
    public class SetupperState : State
    {
        private readonly IObjectGetterService _objectGetterService;
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly IConfiguratioGetter _configuratioGetter;
        private readonly UserInformationHelper _userInformationHelper;
        private readonly AudioSetupper _audioSetupper;
        private readonly ScreenOrientationAlertController _screenOrientationAlertController;

        public SetupperState(IObjectGetterService objectGetterService,
            IUserInterfaceHelper userInterfaceHelper,
            IDebugger debugger,
            IConfiguratioGetter configuratioGetter,
            UserInformationHelper userInformationHelper,
            AudioSetupper audioSetupper,
            ScreenOrientationAlertController screenOrientationAlertController) : base(debugger)
        {
            _objectGetterService = objectGetterService;
            _userInterfaceHelper = userInterfaceHelper;
            _configuratioGetter = configuratioGetter;
            _userInformationHelper = userInformationHelper;
            _audioSetupper = audioSetupper;
            _screenOrientationAlertController = screenOrientationAlertController;
        }

        public override async UniTask Switch(CancellationToken cancellationToken)
        {
            Input.multiTouchEnabled = false;

            _userInformationHelper.Initialize();
            await _objectGetterService.Initialize();
            await _userInterfaceHelper.Setup();
            await _configuratioGetter.Initialize();
            await _screenOrientationAlertController.Perform(CancellationToken.None);
            _userInterfaceHelper.ShowWindow(WindowNames.SplashWindowName, cancellationToken).Forget();
            await _audioSetupper.Perform(CancellationToken.None);

            SwitchTo<GameState>().Forget();
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.SplashWindowName);
        }
    }
}