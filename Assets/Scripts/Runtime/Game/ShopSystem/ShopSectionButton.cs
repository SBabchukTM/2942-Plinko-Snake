using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.ShopSystem
{
    public class ShopSectionButton : MonoBehaviour
    {
        private readonly Color DisableColor = new Color(1, 1, 1, 0);
        private const float AnimTime = 0.25f;
    
        [SerializeField] private Button _button;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _parent;
    
        public RectTransform Parent => _parent;
    
        public event Action OnClick;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnClick?.Invoke());
        }

        public void SetActive(bool enable)
        {
            _button.image.DOKill();
        
            _parent.gameObject.SetActive(enable);
            _button.image.DOColor(enable ? Color.white : DisableColor, AnimTime);

            if (enable)
                _scrollRect.content = _parent;
        }
    }
}
