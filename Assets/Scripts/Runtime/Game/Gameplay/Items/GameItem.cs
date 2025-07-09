using Runtime.Core.Audio;
using Runtime.Game.Gameplay.Snake;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public abstract class GameItem : MonoBehaviour
    {
        [Inject]
        protected ISoundService SoundService;
    
        public abstract void PoolMe();

        protected abstract void OnPlayerCollision(Node node);
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Node snakeNode))
                OnPlayerCollision(snakeNode);
        }
    }
}
