using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Popup
{
    public class PausePopup : BasePopup
    {
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _homeButton;

        public event Action OnHomePressed;
        public event Action OnReturnPressed;

        public override UniTask Show(CancellationToken cancellationToken = default)
        {
            Sub();
            return base.Show(cancellationToken);
        }

        private void Sub()
        {
            _returnButton.onClick.AddListener(() => OnReturnPressed?.Invoke());
            _homeButton.onClick.AddListener(() => OnHomePressed?.Invoke());
        }
    }
}