using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Popup
{
    public class HowToPlayPopup : BasePopup
    {
        [SerializeField] private Button _button;

        public override UniTask Show(CancellationToken cancellationToken = default)
        {
            Sub();
            return base.Show(cancellationToken);
        }

        private void Sub()
        {
            _button.onClick.AddListener(DestroyPopup);
        }
    }
}