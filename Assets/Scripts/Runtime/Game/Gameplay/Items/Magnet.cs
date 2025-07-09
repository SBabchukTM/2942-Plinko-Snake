using Runtime.Game.Achievements;
using Runtime.Game.Gameplay.Effects;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class Magnet : GameItem
    {
        private MagnetsPool _magnetsPool;
        private MagnetEffect _magnetEffect;
    
        private Camera _mainCamera;

        [Inject]
        private void Construct(MagnetsPool magnetsPool, MagnetEffect magnetEffect)
        {
            _magnetsPool = magnetsPool;
            _magnetEffect = magnetEffect;
        
            _mainCamera = Camera.main;
        }

        protected override void OnPlayerCollision(Node node)
        {
            PoolMe();
            AccomplishmentsEventInvoker.InvokeOnMagnetUsed();
            SoundService.PlaySound(ConstAudioNames.MagnetSound);
            CollectibleNode[] collectibleNodes = FindObjectsOfType<CollectibleNode>();
        
            AddNodes(collectibleNodes);    
        }

        private void AddNodes(CollectibleNode[] collectibleNodes)
        {
            foreach (var node in collectibleNodes)
            {
                var nodeTransform = node.transform;
                if(IsOnScreen(nodeTransform))
                    _magnetEffect.AddItem(nodeTransform);
            }
        }

        private bool IsOnScreen(Transform nodeTransform)
        {
            Vector3 screenPoint = _mainCamera.WorldToViewportPoint(nodeTransform.position);

            return screenPoint.z > 0 &&
                   screenPoint.x >= 0 && screenPoint.x <= 1 &&
                   screenPoint.y >= 0 && screenPoint.y <= 1;
        }

        public override void PoolMe() => _magnetsPool.ReturnToPool(this);
    }
}
