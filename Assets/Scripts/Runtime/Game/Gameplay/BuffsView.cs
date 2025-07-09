using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay
{
    public class BuffsView : MonoBehaviour
    {
        [SerializeField] private GameObject _ghostBuff;
        [SerializeField] private GameObject _rocketBuff;
    
        private GameData _gameData;

        [Inject]
        private void Construct(GameData gameData)
        {
            _gameData = gameData;
        }

        private void Update()
        {
            _ghostBuff.gameObject.SetActive(_gameData.GhostActive);
            _rocketBuff.gameObject.SetActive(_gameData.RocketActive);
        }
    }
}
