using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class MenuWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _termsOfUseButton;
        [SerializeField] private Button _privacyButton;

        public event Action OnBackPressed;
        public event Action OnSettingsPressed;
        public event Action OnProfilePressed;
        public event Action OnTermsOfUsePressed;
        public event Action OnPrivacyPressed;
        
        public void Setup()
        {
            Sub();
        }

        private void Sub()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsPressed?.Invoke());
            _profileButton.onClick.AddListener(() => OnProfilePressed?.Invoke());
            _termsOfUseButton.onClick.AddListener(() => OnTermsOfUsePressed?.Invoke());
            _privacyButton.onClick.AddListener(() => OnPrivacyPressed?.Invoke());
        }
    }
}