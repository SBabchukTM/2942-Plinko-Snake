using Runtime.Game.Gameplay.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay.Misc
{
    public class GameBgScroller : MonoBehaviour, IEnableable, IResettable
    {
        [SerializeField] private Transform[] _backgrounds;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private GameData _gameData;
    
        private float _backgroundHeight;
        private float _totalScroll;

        private bool _enabled;

        [Inject]
        private void Construct(GameData gameData, ManagerOfSystems managerOfSystems)
        {
            _gameData = gameData;
            managerOfSystems.Register(this);
        }

        private void Awake()
        {
            Reset();
        }

        private void Update()
        {
            if(!_enabled)
                return;

            DoTask();
        }

        private void DoTask()
        {
            float moveAmount = Time.deltaTime * _gameData.GameSpeed;
            AddScrollTotal(moveAmount);
        
            ScrollBackgrounds();
            ScrollContent(new Vector3(0, -moveAmount, 0));
        }

        private void AddScrollTotal(float moveAmount)
        {
            _totalScroll += moveAmount;
        }

        public void Enable(bool enable)
        {
            _enabled = enable;

            foreach (var bg in _backgrounds)
                bg.gameObject.SetActive(enable);
        }

        public void AddContent(Transform content) => content.transform.SetParent(_contentParent);

        public void SetSkin(Sprite sprite)
        {
            foreach (var bg in _backgrounds)
                bg.GetComponent<SpriteRenderer>().sprite = sprite;
        }

        public void Reset()
        {
            _backgroundHeight = _spriteRenderer.bounds.size.y;

            for (int i = 0; i < _backgrounds.Length; i++) 
                _backgrounds[i].position = new Vector3(0, i * _backgroundHeight, 0);
        }

        private void ScrollBackgrounds()
        {
            for (int i = 0; i < _backgrounds.Length; i++)
            {
                float newY = (_backgroundHeight * i - _totalScroll) % (_backgroundHeight * _backgrounds.Length);
            
                if (newY < -_backgroundHeight) 
                    newY += _backgroundHeight * _backgrounds.Length;

                _backgrounds[i].position = new Vector3(0, newY, 0);
            }
        }

        private void ScrollContent(Vector3 moveAmount) => _contentParent.position += moveAmount;
    }
}
