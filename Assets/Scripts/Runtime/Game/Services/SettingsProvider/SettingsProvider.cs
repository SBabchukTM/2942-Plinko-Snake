using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.ScreenOrientation;
using Runtime.Game.ShopSystem;

namespace Runtime.Game.Services.SettingsProvider
{
    public class SettingsProvider : IConfiguratioGetter
    {
        private readonly IObjectGetterService _objectGetterService;

        private Dictionary<Type, BaseConfigSO> _settings = new Dictionary<Type, BaseConfigSO>();

        public SettingsProvider(IObjectGetterService objectGetterService)
        {
            _objectGetterService = objectGetterService;
        }

        public async UniTask Initialize()
        {
            var screenOrientationConfig = await _objectGetterService.Load<ScreenOrientationConfig>(ConfigNames.ScreenOrientationConfig);
            var audioConfig = await _objectGetterService.Load<AudioConfig>(ConfigNames.AudioConfig);
            var shopConfig = await _objectGetterService.Load<ShopConfig>(ConfigNames.ShopConfig);

            Set(screenOrientationConfig);
            Set(audioConfig);
            Set(shopConfig);
        }

        public T Get<T>() where T : BaseConfigSO
        {
            if (_settings.ContainsKey(typeof(T)))
            {
                var setting = _settings[typeof(T)];
                return setting as T;
            }

            throw new Exception("No setting found");
        }

        public void Set(BaseConfigSO config)
        {
            if (_settings.ContainsKey(config.GetType()))
                return;

            _settings.Add(config.GetType(), config);
        }
    }
}