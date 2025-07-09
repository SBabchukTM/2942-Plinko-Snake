using System.Collections.Generic;
using Hellmade.Sound;
using Runtime.Core.Audio;
using Runtime.Core.Extensions;
using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;
using AudioType = Runtime.Core.Audio.AudioType;

namespace Runtime.Game.Services.Audio
{
    public class SoundService : ISoundService
    {
        private readonly IConfiguratioGetter _staticSettingsService;

        public SoundService(IConfiguratioGetter staticSettingsService)
        {
            _staticSettingsService = staticSettingsService;
        }

        public void PlayMusic(AudioClip clip) => EazySoundManager.PlayMusic(clip);

        public void PlaySound(string clipId)
        {
            var audioSettings = _staticSettingsService.Get<AudioConfig>();
            var clip = audioSettings.GetData(clipId);
            if (clip)
                EazySoundManager.PlaySound(clip);
        }

        public void SetVolume(AudioType audioType, float volume)
        {
            switch (audioType)
            {
                case AudioType.Music:
                    EazySoundManager.GlobalMusicVolume = volume;
                    break;
                case AudioType.Sound:
                    EazySoundManager.GlobalSoundsVolume = volume;
                    break;
                default:
                    throw new KeyNotFoundException($"{nameof(SoundService)}: {audioType} handler not found");
            }
        }
    }
}