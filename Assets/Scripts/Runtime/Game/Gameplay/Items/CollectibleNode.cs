using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class CollectibleNode : GameItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        private NodeManager _nodeManager;
        private CollectibleNodesPool _pool;

        [Inject]
        private void Construct(NodeManager nodeManager, CollectibleNodesPool pool)
        {
            _nodeManager = nodeManager;
            _pool = pool;
        }
    
        public void SetSkin(Sprite sprite) => _spriteRenderer.sprite = sprite;

        public override void PoolMe() => _pool.ReturnToPool(this);

        protected override void OnPlayerCollision(Node node)
        {
            _nodeManager.AddNode();
            PoolMe();
            SoundService.PlaySound(ConstAudioNames.NodeSound);
        }
    }
}
