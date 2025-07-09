using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class MainWindow : BaseWindow
    {
        [SerializeField] private Button _helpButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _dailyButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _leaderButton;
        [SerializeField] private Button _achButton;
        [SerializeField] private Button _startButton;

        public event Action OnHelpPressed;
        public event Action OnMenuPressed;
        public event Action OnDailyPressed;
        public event Action OnShopPressed;
        public event Action OnLeaderPressed;
        public event Action OnAchPressed;
        public event Action OnStartPressed;
        
        public void Setup()
        {
            Sub();
        }

        private void Sub()
        {
            _helpButton.onClick.AddListener(() => OnHelpPressed?.Invoke());
            _menuButton.onClick.AddListener(() => OnMenuPressed?.Invoke());
            _dailyButton.onClick.AddListener(() => OnDailyPressed?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopPressed?.Invoke());
            _leaderButton.onClick.AddListener(() => OnLeaderPressed?.Invoke());
            _achButton.onClick.AddListener(() => OnAchPressed?.Invoke());
            _startButton.onClick.AddListener(() => OnStartPressed?.Invoke());
        }
    }
}