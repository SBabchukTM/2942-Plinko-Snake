using System.Collections.Generic;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Effects
{
    public class MagnetEffect : ITickable, ICleanup, IEnableable 
    {
        private const float EffectSpeed = 3;
        
        private List<Transform> _magnetTargets = new();
        
        private NodeManager _nodeManager;

        private bool _enabled = false;

        public MagnetEffect(NodeManager nodeManager, ManagerOfSystems managerOfSystems)
        {
            _nodeManager = nodeManager;
            managerOfSystems.Register(this);
        }
        
        public void Tick()
        {
            if(!_enabled)
                return;
            
            if(_nodeManager.NodeCount == 0)
                return;

            ProcessMagnet();
        }

        private void ProcessMagnet()
        {
            var targetPos = _nodeManager.GetHead().transform.position;

            for (int i = _magnetTargets.Count - 1; i >= 0; i--)
            {
                var item = _magnetTargets[i];

                if (!item || !item.gameObject.activeInHierarchy)
                {
                    _magnetTargets.RemoveAt(i);
                    continue;
                }
                
                Vector3 moveAmount = targetPos - item.position;
                item.transform.position += moveAmount * (Time.deltaTime * EffectSpeed);
            }
        }

        public void AddItem(Transform item) => _magnetTargets.Add(item);
        
        public void Clear() => _magnetTargets.Clear();
        public void Cleanup() => Clear();
        
        public void Enable(bool enable) => _enabled = enable;
    }
}