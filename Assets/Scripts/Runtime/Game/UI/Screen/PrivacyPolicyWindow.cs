using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class PrivacyPolicyWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;

        public event Action OnBackPressed;
        
        public void Setup()
        {
            Sub();
        }

        private void Sub()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }
    }
}