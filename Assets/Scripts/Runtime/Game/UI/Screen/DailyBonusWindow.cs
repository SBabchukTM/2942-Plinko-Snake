using System;
using Runtime.Game.Dailies;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class DailyBonusWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _spinButton;
        [SerializeField] private RectTransform _rouletteTransform;
        [SerializeField] private GameObject _errorText;
        [SerializeField] private DailyRouletteSlotDisplay[] _slots;
        
        public event Action OnBackPressed;
        public event Action OnSpinPressed;
        
        public RectTransform RouletteTransform => _rouletteTransform;
        public DailyRouletteSlotDisplay[] Slots => _slots;
        
        public void Setup()
        {
            Sub();
        }

        private void Sub()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _spinButton.onClick.AddListener(() => OnSpinPressed?.Invoke());
        }

        public void DisableSpinning()
        {
            _spinButton.gameObject.SetActive(false);
            _errorText.SetActive(true);
        }

        public void DisableInteractibleObjects()
        {
            _spinButton.interactable = false;
            _backButton.interactable = false;
        }
    }
}