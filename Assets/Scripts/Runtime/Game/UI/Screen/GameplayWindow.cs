using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class GameplayWindow : BaseWindow
    {
        [SerializeField] private Button _pauseButton;

        public event Action OnPausePressed;
        
        public void Setup() => _pauseButton.onClick.AddListener(() => OnPausePressed?.Invoke());
    }
}