using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.GameStates.Game.Menu;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class Obstacle : GameItem
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        private GameplayWindowState _gameplayWindowState;
        private NodeManager _nodeManager;
        private ObstaclePool _obstaclePool;

        public void SetSkin(Sprite longSkin) => _spriteRenderer.sprite = longSkin;

        [Inject]
        private void Construct(GameplayWindowState gameplayWindowState, ObstaclePool obstaclePool, NodeManager nodeManager)
        {
            _gameplayWindowState = gameplayWindowState;
            _obstaclePool = obstaclePool;
            _nodeManager = nodeManager;
        }
    
        public override void PoolMe()
        {
            _obstaclePool.ReturnToPool(this);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Node snakeNode) && _nodeManager.GetHead() == snakeNode)
                OnPlayerCollision(snakeNode);
        }

        protected override void OnPlayerCollision(Node node)
        {
            _gameplayWindowState.OnCollision();
        }
    }
}
