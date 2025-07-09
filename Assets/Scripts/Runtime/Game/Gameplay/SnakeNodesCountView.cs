using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Game.Gameplay
{
    public class SnakeNodesCountView : MonoBehaviour
    {
        [SerializeField] private Image _ballImage;
        [SerializeField] private TextMeshProUGUI _lenText;
    
        private GameData _gameData;

        [Inject]
        private void Construct(GameData gameData, SpritesProvider spritesProvider)
        {
            _ballImage.sprite = spritesProvider.GetBallSkin();
            _gameData = gameData;
        }

        private void Update()
        {
            _lenText.text = "x"+_gameData.SnakeLength;
        }
    }
}
