using System.Collections.Generic;
using Runtime.Core.Audio;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Spawning.BlockRows
{
    public class RowOfBlocks : MonoBehaviour
    {
        [SerializeField] private List<GameBlock> _blocks;

        private BlocksCreator _blocksCreator;
        private NodeManager _nodeManager;
        private BlockRowPool _blockRowPool;
        private ISoundService _soundService;
    
        [Inject]
        private void Construct(BlocksCreator blocksCreator, NodeManager nodeManager, 
            BlockRowPool blockRowPool, ISoundService soundService)
        {
            _blocksCreator = blocksCreator;
            _nodeManager = nodeManager;
            _blockRowPool = blockRowPool;
            _soundService = soundService;
        }

        public void Initialize(Sprite blockSprite)
        {
            var blocksData = _blocksCreator.CreatePenaltiesList();

            InitBlocks(blockSprite, blocksData);
        }

        private void InitBlocks(Sprite blockSprite, List<int> blocksData)
        {
            for(int i = 0; i < blocksData.Count; i++)
                _blocks[i].Initialize(blocksData[i], this, blockSprite);
        }

        public void ProcessTrigger(int penalty)
        {
            _nodeManager.RemoveNodes(penalty);
            ReturnToPool();
            _soundService.PlaySound(ConstAudioNames.CollisionSound);
        }

        public void ReturnToPool() => _blockRowPool.ReturnToPool(this);
    }
}
