using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.Game.Gameplay.Effects
{
    public class GhostEffect
    {
        private const float EffectDuration = 5f;
    
        private CancellationTokenSource _cts;
    
        private readonly GameData _gameData;

        public GhostEffect(GameData gameData)
        {
            _gameData = gameData;
        }

        public void StartEffect()
        {
            InterruptEffect();

            _cts = new CancellationTokenSource();
            PlayEffect(_cts.Token).Forget();
        }

        private void InterruptEffect()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        
            _gameData.GhostActive = false;
        }

        private async UniTask PlayEffect(CancellationToken token)
        {
            Physics2D.IgnoreLayerCollision(LayerHelper.GetLayerId(ConstLayers.SnakeLayer), LayerHelper.GetLayerId(ConstLayers.ObstacleLayer), true);
            _gameData.GhostActive = true;
        
            await UniTask.WaitForSeconds(EffectDuration, cancellationToken: token);
        
            Physics2D.IgnoreLayerCollision(LayerHelper.GetLayerId(ConstLayers.SnakeLayer), LayerHelper.GetLayerId(ConstLayers.ObstacleLayer), false);
            _gameData.GhostActive = false;
        }
    }
}
