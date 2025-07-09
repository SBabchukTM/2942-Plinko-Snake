using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Popup;
using Runtime.Game.Dailies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Popup
{
    public class RewardPopup : BasePopup
    {
        [SerializeField] private Button _claimButton;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _congratsText;

        public event Action OnClaimPressed;

        public override UniTask Show(CancellationToken cancellationToken = default)
        {
            Sub();
            return base.Show(cancellationToken);
        }

        private void Sub()
        {
            _claimButton.onClick.AddListener(() => OnClaimPressed?.Invoke());
        }

        public void SetData(DailyReward reward)
        {
            _image.sprite = reward.RewardSprite;

            if (reward.RewardType == RewardType.Coin)
            {
                _amountText.text = "x" + reward.RewardValue.ToString();
            }
            else if (reward.RewardType == RewardType.None)
            {
                _congratsText.text = "Maybe next time...";
            }
        }
    }
}