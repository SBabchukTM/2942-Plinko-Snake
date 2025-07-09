using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Snake
{
    public class PlayerInputProvider : ITickable, IInitializable
    {
        private Camera _camera;
        private Vector3 _worldPos;
    
        public Vector3 WorldPos => _worldPos;
    
        public void Tick()
        {
            if (!AnyInput() || Helper.Helper.IsPointerOverUIElement())
            {
                _worldPos = Constants.SnakeHeadPosition;
                return;
            }
        
            var touchPos = UpdateWorldPos();

            _worldPos = touchPos;
        }

        private Vector3 UpdateWorldPos()
        {
            Vector3 touchPos = _camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPos.y = Constants.SnakeHeadPosition.y;
            touchPos.z = 0;
            return touchPos;
        }

        private bool AnyInput() => Input.touchCount > 0;

        public void Initialize() => _camera = Camera.main;
    }
}
