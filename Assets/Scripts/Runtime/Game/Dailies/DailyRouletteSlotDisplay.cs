using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.Dailies
{
    public class DailyRouletteSlotDisplay : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
    
        private DailyReward _bonusReward;
    
        public void Initialize(DailyReward bonusReward)
        {
            _bonusReward = bonusReward;

            _itemImage.sprite = bonusReward.RewardSprite;
        }
    }
}
