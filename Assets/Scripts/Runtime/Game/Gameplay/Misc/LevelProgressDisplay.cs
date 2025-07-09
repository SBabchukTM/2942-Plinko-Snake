using Runtime.Core.Audio;
using Runtime.Game.Services.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Game.Gameplay.Misc
{
    public class LevelProgressDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private TextMeshProUGUI _nextLevelText;
        [SerializeField] private Slider _progressSlider;
    
        private GameData _gameData;
        private ISoundService _soundService;
    
        [Inject]
        private void Construct(GameData gameData, ISoundService soundService)
        {
            _gameData = gameData;
            _soundService = soundService;
        
            _gameData.OnGameLevelChanged += UpdateLevel;
        }

        private void OnDestroy()
        {
            _gameData.OnGameLevelChanged -= UpdateLevel;
        }

        private void Update()
        {
            _progressSlider.value = _gameData.LevelProgress;
        }

        private void UpdateLevel(int currentLevel)
        {
            _soundService.PlaySound(ConstAudioNames.LevelUpSound);
            _currentLevelText.text = currentLevel.ToString();
            _nextLevelText.text = (currentLevel + 1).ToString();
        }
    }
}
