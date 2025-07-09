using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.UI;
using Runtime.Game.Services.UserData.Data;
using Runtime.Game.UI.Screen;
using Runtime.Game.UserAccountSystem;
using UnityEngine;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class ProfileState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly GalleryPickerService _galleryPickerService;
        private readonly ProfileService _profileService;

        private ProfileWindow _window;

        private ProfileData _modifiedData;
        
        public ProfileState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, GalleryPickerService galleryPickerService, ProfileService profileService) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _galleryPickerService = galleryPickerService;
            _profileService = profileService;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            _modifiedData = _profileService.GetProfileDataCopy();
            
            CreateScreen();
            SubscribeToEvents();
            return UniTask.CompletedTask;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.ProfileWindow);
        }

        private void CreateScreen()
        {
            _window = _userInterfaceHelper.RetrieveWindow<ProfileWindow>(WindowNames.ProfileWindow);
            _window.Setup();
            _window.Reveal().Forget();
            _window.SetName(_modifiedData.PlayerName);
            _window.SetAvatar(_profileService.GetUsedAvatarSprite());
        }

        private void SubscribeToEvents()
        {
            _window.OnBackPressed += async () => await SwitchTo<MenuWindow>();
            _window.OnSavePressed += async () =>
            {
                _profileService.SaveAccountData(_modifiedData);
                await SwitchTo<MenuWindow>();
            };
            
            _window.OnNameChanged += ValidateName;
            _window.OnAvatarChangePressed += ChangeAvatar;
        }

        private void ValidateName(string value)
        {
            if (value.Length < 3)
            {
                _window.SetName(_modifiedData.PlayerName);
                return;
            }

            if (!Char.IsLetter(value[0]))
            {
                _window.SetName(_modifiedData.PlayerName);
                return;
            }
            
            _modifiedData.PlayerName = value;
        }

        private async void ChangeAvatar()
        {
            Sprite newAvatar = await _galleryPickerService.PickFromGallery(1024);
            if (newAvatar)
            {
                _window.SetAvatar(newAvatar);
                _modifiedData.AvatarAsString = _profileService.ConvertToBase64(newAvatar);
            }
        }
    }
}