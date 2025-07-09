using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Snake
{
    public class SnakeMovementController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 25f;
    
        private PlayerInputProvider _inputProvider;
        private NodeManager _nodeManager;

        [Inject]
        private void Construct(PlayerInputProvider inputProvider, NodeManager nodeManager)
        {
            _inputProvider = inputProvider;
            _nodeManager = nodeManager;
        }
    
        private void Update()
        {
            if(_nodeManager.NodeCount == 0)
                return;

            var currentTime = Time.time;
            _nodeManager.Update(currentTime);

            float moveAmount = _movementSpeed * Time.deltaTime;
        
            MoveHead(moveAmount);
            MoveBody(currentTime, moveAmount);
        }

        private void MoveHead(float moveAmount)
        {
            var head = _nodeManager[0];
            var target = _inputProvider.WorldPos;

            head.transform.position = Vector3.MoveTowards(
                head.transform.position,
                target,
                moveAmount
            );
        }

        private void MoveBody(float currentTime, float moveAmount)
        {
            for (var i = 1; i < _nodeManager.NodeCount; i++)
            {
                var targetPos = _nodeManager.GetTargetPosition(currentTime, i);
                targetPos.y -= NodeManager.NodeOffset;

                _nodeManager[i].transform.position = Vector3.MoveTowards(
                    _nodeManager[i].transform.position,
                    targetPos,
                    moveAmount
                );
            }
        }
    }
}