using System;
using Runtime.Game.Achievements;

namespace Runtime.Game.Gameplay
{
    public class GameData
    {
        public event Action<int> OnCoinCollected;
        public event Action<int> OnGameLevelChanged;
    
        private int _coinsCollected;
        private int _gameLevel;

        private int _snakeLength;
    
        public int SnakeLength
        {
            get => _snakeLength;
            set
            {
                _snakeLength = value;
                AccomplishmentsEventInvoker.InvokeOnSnakeLengthReached(_snakeLength);
            }
        }
    
        public bool GhostActive { get; set; }
        public bool RocketActive { get; set; }

        public float GameSpeed {get;set;}
        public float LevelProgress {get;set;}
        public float TotalDistanceTravelled {get;set;}
        public float TotalScore {get;set;}

        public int CoinsCollected
        {
            get => _coinsCollected;
            set
            {
                _coinsCollected = value;
                OnCoinCollected?.Invoke(value);
            }
        }

        public int GameLevel
        {
            get => _gameLevel;
            set
            {
                _gameLevel = value;
                OnGameLevelChanged?.Invoke(value);
                AccomplishmentsEventInvoker.InvokeOnLevelReached(_gameLevel);
            }
        }
    }
}
