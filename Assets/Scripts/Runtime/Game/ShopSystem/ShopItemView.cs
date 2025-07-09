using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.ShopSystem
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _statusText;
        [SerializeField] private Button _button;

        public event Action<ShopItemView> OnClick;
    
        public ShopItem ShopItem { get; private set; }
    
        public void Setup(ShopItem shopItem)
        {
            _itemImage.sprite = shopItem.Sprite;
            _priceText.text = shopItem.Price.ToString();
            ShopItem = shopItem;
        
            _button.onClick.AddListener(() => OnClick?.Invoke(this));
        }
    
        public void SetStatus(string status) => _statusText.text = status;
    }
}
