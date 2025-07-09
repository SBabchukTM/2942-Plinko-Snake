using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Services.ScreenOrientation
{
    [CreateAssetMenu(fileName = "ScreenOrientationConfig", menuName = "Config/ScreenOrientationConfig")]
    public sealed class ScreenOrientationConfig : BaseConfigSO
    {
        public ScreenOrientationTypes ScreenOrientationTypes;
        public bool EnableScreenOrientationPopup;
    }
}