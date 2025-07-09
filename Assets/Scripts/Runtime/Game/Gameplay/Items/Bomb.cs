using Runtime.Game.Achievements;
using Runtime.Game.Gameplay.Effects;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.BlockRows;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class Bomb : GameItem
    {
        private readonly Vector3 _offset = new Vector3(0f, 4, 0f);
    
        private BombPool _pool;
        private NodeManager _nodeManager;
        private ExplosionEffect _explosionEffect;

        private bool _active;
    
        [Inject]
        private void Construct(BombPool pool, NodeManager nodeManager, ExplosionEffect explosionEffect)
        {
            _pool = pool;
            _nodeManager = nodeManager;
            _explosionEffect = explosionEffect;
        }

        protected override void OnPlayerCollision(Node node) => _active = true;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out GameItem baseItem))
            {
                baseItem.PoolMe();
                Explode();
            }
            else if (other.TryGetComponent(out GameBlock block))
            {
                block.DestroyRow();
                Explode();
            }
            else
                _active = true;
        }

        private void Explode()
        {
            PoolMe();
            AccomplishmentsEventInvoker.InvokeOnBombUsed();
            SoundService.PlaySound(ConstAudioNames.ExplosionSound);
            _explosionEffect.Trigger(transform.position);
        }

        private void Update()
        {
            if(!_active)
                return;

            if (_nodeManager.NodeCount == 0)
            {
                PoolMe();
                return;
            }
        
            transform.position = _nodeManager.GetHead().transform.position + _offset;
        }

        public override void PoolMe() => _pool.ReturnToPool(this);
    }
}
