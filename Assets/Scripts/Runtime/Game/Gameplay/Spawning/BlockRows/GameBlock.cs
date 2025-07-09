using Runtime.Game.Gameplay.Snake;
using TMPro;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Spawning.BlockRows
{
    public class GameBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _penaltyText;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private NodeManager _nodeManager;
    
        private int _penalty;
        private RowOfBlocks _rowOfBlocks;

        [Inject]
        private void Construct(NodeManager nodeManager)
        {
            _nodeManager = nodeManager;
        }
    
        public void Initialize(int penalty, RowOfBlocks rowOfBlocks, Sprite sprite)
        {
            SetData(penalty, rowOfBlocks, sprite);
        }

        private void SetData(int penalty, RowOfBlocks rowOfBlocks, Sprite sprite)
        {
            _penalty = penalty;
            _rowOfBlocks = rowOfBlocks;
            _spriteRenderer.sprite = sprite;
        
            _penaltyText.text = penalty.ToString();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_nodeManager.NodeCount == 0)
                return;
        
            if(!other.TryGetComponent(out Node node))
                return;
        
            if(node != _nodeManager.GetHead())
                return;
        
            _rowOfBlocks.ProcessTrigger(_penalty);
        }

        public void DestroyRow() => _rowOfBlocks.ReturnToPool();
    }
}
