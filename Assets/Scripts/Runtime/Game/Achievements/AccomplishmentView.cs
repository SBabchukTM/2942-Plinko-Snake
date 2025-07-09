using Runtime.Game.ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Game.Achievements
{
    public class AccomplishmentView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private TextMeshProUGUI _rewardText;

        [SerializeField] private Button _claimButton;

        private InventoryHelper _inventoryHelper;
    
        [Inject]
        private void Construct(InventoryHelper inventoryHelper)
        {
            _inventoryHelper = inventoryHelper;
        }
    
        public void Setup(AccomplishmentData accomplishmentData)
        {
            SetData(accomplishmentData);

            Sub(accomplishmentData);
        }

        private void SetData(AccomplishmentData accomplishmentData)
        {
            _descText.text = accomplishmentData.Text;
            _rewardText.text = "x" + accomplishmentData.Reward;

            _claimButton.interactable = accomplishmentData.Unlocked && !accomplishmentData.Claimed;
        }

        private void Sub(AccomplishmentData accomplishmentData)
        {
            _claimButton.onClick.AddListener(() =>
            {
                accomplishmentData.Claimed = true;
                _inventoryHelper.AddCoins(accomplishmentData.Reward);
                _claimButton.interactable = false;
            });
        }
    }
}
