using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Game.ShopSystem
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
    
        private InventoryHelper _inventoryHelper;

        [Inject]
        private void Construct(InventoryHelper inventoryHelper)
        {
            _inventoryHelper = inventoryHelper;
        
            _inventoryHelper.CoinsChangedEvent += UpdateAmount;
            _balanceText.text = _inventoryHelper.GetCoins().ToString();
        }

        private void OnDestroy() => _inventoryHelper.CoinsChangedEvent -= UpdateAmount;

        private void UpdateAmount(int balance) => _balanceText.text = balance.ToString();
    }
}
