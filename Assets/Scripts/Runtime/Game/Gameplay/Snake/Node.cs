using UnityEngine;

namespace Runtime.Game.Gameplay.Snake
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        public void SetSkin(Sprite sprite) => _spriteRenderer.sprite = sprite;
    }
}
