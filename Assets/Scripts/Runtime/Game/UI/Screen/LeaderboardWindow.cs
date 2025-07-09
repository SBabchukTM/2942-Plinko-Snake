using System;
using System.Collections.Generic;
using Runtime.Game.Leaderboard;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class LeaderboardWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private RectTransform _parent;

        public event Action OnBackPressed;
        public event Action OnMenuPressed;
        
        public void Setup(List<RecordDisplay> records)
        {
            Sub();

            foreach (var record in records)
            {
                record.transform.SetParent(_parent, false);
            }
        }

        private void Sub()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _menuButton.onClick.AddListener(() => OnMenuPressed?.Invoke());
        }
    }
}