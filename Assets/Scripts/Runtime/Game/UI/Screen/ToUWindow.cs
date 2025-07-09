using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class ToUWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;

        public event Action OnBackPressed;
        
        public void Setup()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }
    }
}