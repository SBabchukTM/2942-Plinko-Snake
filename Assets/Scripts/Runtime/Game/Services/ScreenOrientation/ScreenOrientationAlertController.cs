using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Controllers;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.Pause;
using Runtime.Game.Services.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Runtime.Game.Services.ScreenOrientation
{
    public class ScreenOrientationAlertController : BaseController, ITickable
    {
        private readonly IUserInterfaceHelper _uiService;
        private readonly IConfiguratioGetter _settingProvider;

        private ScreenOrientationAlertPopup _alertPopup;
        private ScreenOrientationConfig _config;
        private bool _isInitialized;

        public ScreenOrientationAlertController(IUserInterfaceHelper uiService, IConfiguratioGetter settingProvider)
        {
            _uiService = uiService;
            _settingProvider = settingProvider;
        }

        public override UniTask Perform(CancellationToken cancellationToken)
        {
            Init();
            
            return base.Perform(cancellationToken);
        }

        public void Tick()
        {
            if(!_isInitialized)
                return;

            if(!_config || !_config.EnableScreenOrientationPopup)
                return;

            CheckScreenOrientation();
        }

        private void CheckScreenOrientation()
        {
            var currentScreenMode = Screen.orientation;

            if(IsSameScreenMode(currentScreenMode))
            {
                if(!_alertPopup.gameObject.activeSelf)
                    return;

                _alertPopup.Hide();
                PauseHelper.TakePause(GameState.Running);

                return;
            }

            PauseHelper.TakePause(GameState.Paused);

            if(!_alertPopup.gameObject.activeSelf)
            {
                HideKeyboard();
                _alertPopup.Show();
            }
        }

        private static void HideKeyboard()
        {
            EventSystem.current?.SetSelectedGameObject(null);
        }

        private bool IsSameScreenMode(UnityEngine.ScreenOrientation currentScreenMode)
        {
            if(_config.ScreenOrientationTypes == ScreenOrientationTypes.Portrait)
            {
                if(currentScreenMode is UnityEngine.ScreenOrientation.Portrait or UnityEngine.ScreenOrientation.PortraitUpsideDown)
                {
                    return true;
                }
            }

            if(_config.ScreenOrientationTypes != ScreenOrientationTypes.Landscape)
                return (int)currentScreenMode == (int)_config.ScreenOrientationTypes;

            return currentScreenMode is UnityEngine.ScreenOrientation.LandscapeLeft or UnityEngine.ScreenOrientation.LandscapeRight;
        }

        private void Init()
        {
            _config = _settingProvider.Get<ScreenOrientationConfig>();

            if(!_config || !_config.EnableScreenOrientationPopup)
                return;

            _alertPopup = _uiService.GetPopup<ScreenOrientationAlertPopup>(ProjectPopupNames.ScreenOrientationAlertPopup);
            _alertPopup.Hide();

            _isInitialized = true;
        }
    }
}