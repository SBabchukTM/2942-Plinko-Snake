using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.UI;
using Runtime.Game.Services.UserData;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class SettingsScreenState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly UserInformationHelper _userInformationHelper;
        private readonly ISoundService _soundService;

        private SettingsWindow _window;

        private float _origSound;
        private float _origMusic;
        
        private float _modifiedSound;
        private float _modifiedMusic;

        public SettingsScreenState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper,
            UserInformationHelper userInformationHelper, ISoundService soundService) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _userInformationHelper = userInformationHelper;
            _soundService = soundService;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.SettingsWindow);
        }

        private void CreateScreen()
        {
            var setData = _userInformationHelper.GetSerializedData().SettingsData;
            
            _modifiedMusic = _origMusic = setData.MusicVolume;
            _modifiedSound = _origSound = setData.SoundVolume;
            
            _window = _userInterfaceHelper.RetrieveWindow<SettingsWindow>(WindowNames.SettingsWindow);
            _window.Setup(setData);
            _window.Reveal().Forget();
        }

        private void SubscribeToEvents()
        {
            _window.OnBackPressed += async () =>
            {
                Undo();
                await SwitchTo<MenuWindow>();
            };
            
            _window.OnSavePressed += async () =>
            {
                Save();
                await SwitchTo<MenuWindow>();
            };
            
            _window.OnSoundChanged += UpdateSound;
            _window.OnMusicChanged += UpdateMusic;
        }

        private void Undo()
        {
            _soundService.SetVolume(AudioType.Sound, _origSound);
            _soundService.SetVolume(AudioType.Music, _origMusic);
        }
        
        private void Save()
        {
            var setData = _userInformationHelper.GetSerializedData().SettingsData;
            setData.MusicVolume = _modifiedMusic;
            setData.SoundVolume = _modifiedSound;
        }

        private void UpdateMusic(float volume)
        {
            _modifiedMusic = volume;
            _soundService.SetVolume(AudioType.Music, volume);
        }

        private void UpdateSound(float volume)
        {
            _modifiedSound = volume;
            _soundService.SetVolume(AudioType.Sound, volume);
        }
    }
}