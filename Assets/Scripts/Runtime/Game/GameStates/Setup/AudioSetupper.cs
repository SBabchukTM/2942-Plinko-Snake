using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.Controllers;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.UserData;
using UnityEngine;
using AudioType = Runtime.Core.Audio.AudioType;

namespace Runtime.Game.GameStates.Setup
{
    public class AudioSetupper : BaseController
    {
        private readonly ISoundService _soundService;
        private readonly IConfiguratioGetter _staticSettingsService;
        private readonly UserInformationHelper _userInformationHelper;

        private CancellationTokenSource _cancellationTokenSource;

        public AudioSetupper(ISoundService soundService, IConfiguratioGetter staticSettingsService, UserInformationHelper userInformationHelper)
        {
            _soundService = soundService;
            _staticSettingsService = staticSettingsService;
            _userInformationHelper = userInformationHelper;
        }

        public override UniTask Perform(CancellationToken cancellationToken)
        {
            base.Perform(cancellationToken);

            _cancellationTokenSource = new CancellationTokenSource();
            var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cancellationTokenSource.Token);

            LoadVolumeSettings();
            SetupMusic(linkedTokenSource.Token).Forget();
            return UniTask.CompletedTask;
        }

        public override UniTask Return()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            return base.Return();
        }

        private async UniTask SetupMusic(CancellationToken cancellationToken)
        {
            var allMusicAudioData = Create1(out var allMusicClips);

            foreach (var audioData in allMusicAudioData)
                allMusicClips.Add(audioData.Clip);

            await PlayMusic(cancellationToken, allMusicClips);
        }

        private async Task PlayMusic(CancellationToken cancellationToken, List<AudioClip> allMusicClips)
        {
            var clipsCount = allMusicClips.Count;

            var clipIndex = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var clipDuration = (int)allMusicClips[clipIndex].length * 1000 + 1000;
                _soundService.PlayMusic(allMusicClips[clipIndex]);
                await UniTask.Delay(clipDuration, cancellationToken: cancellationToken);
                clipIndex++;
                if (clipIndex >= clipsCount)
                    clipIndex = 0;
            }
        }

        private List<AudioData> Create1(out List<AudioClip> allMusicClips)
        {
            var audioSettings = _staticSettingsService.Get<AudioConfig>();
            var allMusicAudioData = audioSettings.Audio.FindAll(x => x.AudioType == AudioType.Music);
            allMusicClips = new List<AudioClip>(allMusicAudioData.Count);
            return allMusicAudioData;
        }

        private void LoadVolumeSettings()
        {
            var soundVolume = _userInformationHelper.GetSerializedData().SettingsData.SoundVolume;
            _soundService.SetVolume(AudioType.Sound, soundVolume);

            var musicVolume = _userInformationHelper.GetSerializedData().SettingsData.MusicVolume;
            _soundService.SetVolume(AudioType.Music, musicVolume);
        }
    }
}