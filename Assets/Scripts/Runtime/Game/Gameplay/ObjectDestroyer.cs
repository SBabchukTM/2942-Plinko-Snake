using Runtime.Game.Gameplay.Items;
using UnityEngine;

namespace Runtime.Game.Gameplay
{
    public class ObjectDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out GameItem item))
                item.PoolMe();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out GameItem item))
                item.PoolMe();
        }
    }
}
