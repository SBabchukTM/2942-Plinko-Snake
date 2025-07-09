using System;
using System.Collections.Generic;
using Runtime.Game.Gameplay.Spawning.Pools;
using UnityEngine;

namespace Runtime.Game.Gameplay.Snake
{
    public class NodeManager
    {
        private const int BaseSize = 20;
        
        private const float DelayTime = 0.02f;
        public const float NodeOffset = 3;
        
        private List<Node> _nodesList = new(BaseSize);
        private List<PositionRecorder> _positionHistoryList = new(BaseSize);
        
        public int NodeCount => _nodesList.Count;
        public Node this[int index] => _nodesList[index];

        private readonly GameData _gameData;
        private readonly NodesPool _nodesPool;
        
        public event Action OnSnakeDestroyed;

        public NodeManager(GameData gameData, NodesPool nodesPool)
        {
            _gameData = gameData;
            _nodesPool = nodesPool;
        }

        public void CreateSnake(int length)
        {
            _nodesList.Clear();
            _positionHistoryList.Clear();
            
            for (int i = 0; i < length - 1; i++)
            {
                _nodesList.Add(_nodesPool.GetItem());
                SetNodePosition(_nodesList[i].transform, i);
                _positionHistoryList.Add(new PositionRecorder());
            }
            
            _nodesList.Add(_nodesPool.GetSnakeHead());
            _positionHistoryList.Add(new PositionRecorder());
            SetNodePosition(_nodesList[length - 1].transform, length - 1);
            
            _gameData.SnakeLength = length;
        }

        public void RemoveSnake()
        {
            for (int i = NodeCount - 1; i >= 0; i--)
            {
                if (i == NodeCount - 1)
                {
                    _nodesPool.ReturnSnakeHead();
                    continue;
                }

                Node removedNode = _nodesList[i];
                _nodesPool.ReturnToPool(removedNode);
            }
            
            _gameData.SnakeLength = 0;
        }
        
        public Node GetHead()
        {
            if(NodeCount == 0)
                return null;
            
            return this[0];
        }

        public Vector3 GetTargetPosition(float currentTime, int index)
        {
            var prevHistory = _positionHistoryList[index - 1];
            var targetTime = currentTime - DelayTime;

            return prevHistory.GetPositionAtTime(targetTime);
        }

        public void Update(float currentTime)
        {
            RecordHead(currentTime);
            RecordBody(currentTime);
        }

        private void SetNodePosition(Transform transform, int id)
        {
            transform.position = Constants.SnakeHeadPosition - Vector3.up * (NodeOffset * id);
        }
        
        private void RecordHead(float currentTime)
        {
            _positionHistoryList[0].AddNode(this[0].transform.position, currentTime);
        }

        private void RecordBody(float currentTime)
        {
            for (var i = 1; i < NodeCount; i++) 
                _positionHistoryList[i].AddNode(this[i].transform.position, currentTime);
        }
        
        public void AddNode()
        {
            Node headNode = this[0];

            var newNode = _nodesPool.GetItem();
            newNode.transform.position = headNode.transform.position;

            _nodesList.Insert(0, newNode);;
            var newHistory = new PositionRecorder();
            
            newHistory.AddNode(headNode.transform.position, Time.time);
            
            _positionHistoryList.Insert(0, newHistory);
            _gameData.SnakeLength = NodeCount;
        }

        public void RemoveNodes(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (NodeCount == 1)
                {
                    OnSnakeDestroyed?.Invoke();
                    _nodesPool.ReturnSnakeHead();
                    _nodesList.Clear();
                    _positionHistoryList.Clear();
                    return;
                }

                Node removedNode = _nodesList[0];
            
                _nodesList.RemoveAt(0);
                _positionHistoryList.RemoveAt(0);
                
                _nodesPool.ReturnToPool(removedNode);
            }
            
            _gameData.SnakeLength = NodeCount;
        }
    }
}