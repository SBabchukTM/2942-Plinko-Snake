using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Misc
{
    public class CoinCollectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsText;

        private GameData _gameData;
    
        [Inject]
        private void Construct(GameData gameData)
        {
            _gameData = gameData;
            _gameData.OnCoinCollected += UpdateCoins;
        }

        private void OnDestroy() => _gameData.OnCoinCollected -= UpdateCoins;

        private void UpdateCoins(int coins) => _coinsText.text = coins.ToString();
    }
}
