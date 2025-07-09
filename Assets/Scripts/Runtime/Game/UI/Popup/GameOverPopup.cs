using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Popup
{
    public class GameOverPopup : BasePopup
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _homeButton;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        public event Action OnRetryButtonPressed;
        public event Action OnHomeButtonPressed;

        public override UniTask Show(CancellationToken cancellationToken = default)
        {
            Sub();
            return base.Show(cancellationToken);
        }

        private void Sub()
        {
            _retryButton.onClick.AddListener(() => OnRetryButtonPressed?.Invoke());
            _homeButton.onClick.AddListener(() => OnHomeButtonPressed?.Invoke());
        }

        public void SetData(int coins, int score)
        {
            _coinsText.text = coins.ToString();
            _scoreText.text = score.ToString();
        }
    }
}