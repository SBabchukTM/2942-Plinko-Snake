using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Game.ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class ShopWindow : BaseWindow
    {
        private const float FadeInTime = 0.5f;
        private const float FadeOutTime = 1.5f;
        
        [SerializeField] private Button _backButton;
        [SerializeField] private ShopSectionButton _ballsButton;
        [SerializeField] private ShopSectionButton _bgsButton;
        [SerializeField] private TextMeshProUGUI _errorText;
        public event Action OnBackPressed;
        
        public void Initialize(List<ShopItemView> skinDisplayList, List<ShopItemView> bgDisplayList)
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());

            foreach (var itemDisplay in skinDisplayList)
                itemDisplay.transform.SetParent(_ballsButton.Parent, false);
            
            foreach (var itemDisplay in bgDisplayList)
                itemDisplay.transform.SetParent(_bgsButton.Parent, false);
            
            _ballsButton.OnClick += EnableBalls;
            _bgsButton.OnClick += EnableBGs;
        }

        public void ShowError()
        {
            _errorText.DOKill();
            _errorText.DOFade(1, FadeInTime).OnComplete(() => _errorText.DOFade(0, FadeOutTime)).SetLink(gameObject);
        }
        
        private void EnableBGs()
        {
            _ballsButton.SetActive(false);
            _bgsButton.SetActive(true);
        }

        private void EnableBalls()
        {
            _ballsButton.SetActive(true);
            _bgsButton.SetActive(false);
        }
    }
}