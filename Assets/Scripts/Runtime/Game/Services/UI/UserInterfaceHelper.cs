using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Core.UI.Popup;
using Runtime.Game.UI.Screen;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Services.UI
{
    public sealed class UserInterfaceHelper : IUserInterfaceHelper
    {
        private readonly Dictionary<string, BaseWindow> _shownScreens = new Dictionary<string, BaseWindow>();
        
        private IObjectGetterService _objectGetterService;
        private Dictionary<string, GameObject> _screenPrototypes;
        private Dictionary<string, GameObject> _popupPrototypes;
        private GameObjectFactory _factory;
        private UiServiceViewContainer _uiServiceViewContainer;
        
        [Inject] 
        private void Construct(GameObjectFactory factory, IObjectGetterService objectGetterService)
        {
            _factory = factory;
            _objectGetterService = objectGetterService;
        }

        public async UniTask Setup()
        {
            GameObject container = await _objectGetterService.Create(WindowNames.UiServiceViewContainer);
            _uiServiceViewContainer = container.GetComponent<UiServiceViewContainer>();

            RegisterScreens();

            RegisterPopups();
        }

        private void RegisterPopups()
        {
            _popupPrototypes = new Dictionary<string, GameObject>(_uiServiceViewContainer.PopupsPrefab.Count);
            foreach (var popup in _uiServiceViewContainer.PopupsPrefab)
            {
                if (!_popupPrototypes.ContainsKey(popup.Id))
                {
                    _popupPrototypes.Add(popup.Id, popup.gameObject);
                }
            }
        }

        private void RegisterScreens()
        {
            _screenPrototypes = new Dictionary<string, GameObject>(_uiServiceViewContainer.ScreensPrefab.Count);
            foreach (var screen in _uiServiceViewContainer.ScreensPrefab)
            {
                if (!_screenPrototypes.ContainsKey(screen.Identifier))
                {
                    _screenPrototypes.Add(screen.Identifier, screen.gameObject);
                }
            }
        }

        public bool IsScreenShowed(string id)
        {
            return TryGetShownScreen(id, out _);
        }

        public async UniTask ShowWindow(string id, CancellationToken cancellationToken = default)
        {
            if (TryGetShownScreen(id, out BaseWindow screen))
            {
                await screen.Reveal(cancellationToken);
            }
            else
            {
                screen = CreateScreen(id);
                _shownScreens.Add(id, screen);
                await screen.Reveal(cancellationToken);
            }
        }

        public T RetrieveWindow<T>(string id) where T : BaseWindow
        {
            if (!TryGetShownScreen(id, out BaseWindow screen))
            {
                screen = CreateScreen(id);
                _shownScreens.Add(id, screen);
                screen.HideImmediately();
            }

            return screen as T;
        }

        public async UniTask HideWindow(string id, CancellationToken cancellationToken = default)
        {
            if (TryGetShownScreen(id, out BaseWindow screen))
            {
                await screen.Remove(cancellationToken);
                _shownScreens.Remove(id);
            }
        }

        public async UniTask<BasePopup> ShowPopup(string id, CancellationToken cancellationToken = default)
        {
            if (_popupPrototypes.TryGetValue(id, out GameObject prototype))
            {
                var popup = _factory.Create<BasePopup>(prototype, _uiServiceViewContainer.ScreenParent);
                await popup.Show(cancellationToken);
                return popup;
            }

            throw new ArgumentException($"Prototype for '{id}' is not registered.");
        }

        public T GetPopup<T>(string id) where T : BasePopup
        {
            if (_popupPrototypes.TryGetValue(id, out GameObject prototype))
            {
                var popup = _factory.Create<T>(prototype, _uiServiceViewContainer.ScreenParent);
                popup.HideImmediately();
                return popup;
            }

            throw new ArgumentException($"Prototype for '{id}' is not registered.");
        }

        private bool TryGetShownScreen(string id, out BaseWindow screen)
        {
            if (_shownScreens.TryGetValue(id, out screen))
                return true;

            return false;
        }

        private BaseWindow CreateScreen(string id)
        {
            if (_screenPrototypes.TryGetValue(id, out GameObject prototype))
                return _factory.Create<BaseWindow>(prototype, _uiServiceViewContainer.ScreenParent);

            throw new ArgumentException($"Prototype for '{id}' is not registered.");
        }
    }
}