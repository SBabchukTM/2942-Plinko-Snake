using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Game.Gameplay.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay
{
    public class GameplayZoomiesController : GameSystem, ITickable, ICleanup
    {
        private const int MaxLevel = 20;
    
        private const float BuffDuration = 5f;
        private const float ScoreBuff = 3;
        private const float SpeedBuff = 1.5f;
    
        private const float MinGameSpeed = 15;
        private const float MaxGameSpeed = 50f;

        private const float InitialLevelDistance = 50;
        private const float TimeIncreaseFactor = 1.1f;

        private readonly GameData _gameData;

        private CancellationTokenSource _cts;
    
        private float _nextLevelDistance;
        private float _previousLevelDistance;

        private float _scoreMultiplier = 1;
        private float _speedMultiplier = 1;

        public GameplayZoomiesController(GameData gameData, ManagerOfSystems manager) : base(manager)
        {
            _gameData = gameData;
            Reset();
        }

        public void Tick()
        {
            if(!Enabled)
                return;
        
            UpdateDistTravelled();

            if (_gameData.TotalScore >= _nextLevelDistance) 
                NextLevel();
        
            _gameData.GameSpeed = Mathf.Lerp(MinGameSpeed, MaxGameSpeed, Mathf.InverseLerp(0, MaxLevel, _gameData.GameLevel)) * _speedMultiplier;

            UpdateProgress();
        }

        private void UpdateProgress()
        {
            float levelDistance = _nextLevelDistance - _previousLevelDistance;
            float distanceIntoLevel = _gameData.TotalScore - _previousLevelDistance;
            _gameData.LevelProgress = Mathf.Clamp01(distanceIntoLevel / levelDistance);
        }

        private void UpdateDistTravelled()
        {
            float distanceTravelled = _gameData.GameSpeed * Time.deltaTime;
            _gameData.TotalDistanceTravelled += distanceTravelled;
            _gameData.TotalScore += distanceTravelled * _scoreMultiplier;
        }

        public void ApplySpeedBuff()
        {
            CancelBuff();
        
            _cts = new CancellationTokenSource();
            DoSpeedBuff(_cts.Token).Forget();
        }

        public void CancelBuff()
        {
            _scoreMultiplier = 1;
            _speedMultiplier = 1;
            _gameData.RocketActive = false;
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    
        public override void Reset()
        {
            _gameData.GameSpeed = MinGameSpeed;

            _previousLevelDistance = 0f;
            _nextLevelDistance = InitialLevelDistance;
        }

        public void Cleanup() => CancelBuff();

        private async UniTask DoSpeedBuff(CancellationToken token)
        {
            _scoreMultiplier = ScoreBuff;
            _speedMultiplier = SpeedBuff;
            _gameData.RocketActive = true;

            await UniTask.WaitForSeconds(BuffDuration, cancellationToken: token);

            _scoreMultiplier = 1;
            _speedMultiplier = 1;
            _gameData.RocketActive = false;
        }

        private void NextLevel()
        {
            _gameData.GameLevel++;
            _previousLevelDistance = _nextLevelDistance;
        
            float timeToReachLevel = 10f * Mathf.Pow(TimeIncreaseFactor, _gameData.GameLevel);
            float projectedSpeed = Mathf.Lerp(MinGameSpeed, MaxGameSpeed, Mathf.InverseLerp(0, MaxLevel, _gameData.GameLevel));
            float distanceForLevel = projectedSpeed * timeToReachLevel;

            _nextLevelDistance = _previousLevelDistance + distanceForLevel;
        }
    }
}
