using System;
using Runtime.Game.Services.UserData.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class SettingsWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        
        public event Action OnBackPressed;
        public event Action OnSavePressed;
        
        public event Action<float> OnSoundChanged;
        public event Action<float> OnMusicChanged;
        
        public void Setup(SettingsData settings)
        {
            Sub();

            _soundSlider.value = settings.SoundVolume;
            _musicSlider.value = settings.MusicVolume;
            
            Sub32();
        }

        private void Sub32()
        {
            _soundSlider.onValueChanged.AddListener((value) => OnSoundChanged?.Invoke(value));
            _musicSlider.onValueChanged.AddListener((value) => OnMusicChanged?.Invoke(value));
        }

        private void Sub()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _saveButton.onClick.AddListener(() => OnSavePressed?.Invoke());
        }
    }
}