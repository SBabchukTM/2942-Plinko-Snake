using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Achievements;
using Runtime.Game.Services.Audio;
using Runtime.Game.Services.UI;
using Runtime.Game.ShopSystem;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class ShopScreenState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly ShopService _shopService;
        private readonly ShopItemsFactory _shopItemsFactory;
        private readonly ISoundService _soundService;

        private ShopWindow _window;

        private List<ShopItemView> _skinsDisplayList;
        private List<ShopItemView> _bgsDisplayList;
        
        public ShopScreenState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper,
            ShopService shopService, ShopItemsFactory shopItemsFactory, ISoundService soundService) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _soundService = soundService;
            _shopService = shopService;
            _shopItemsFactory = shopItemsFactory;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            CreateShopItems();

            Create1();

            return UniTask.CompletedTask;
        }

        private void Create1()
        {
            MakeWindow();
            Sub();
            UpdateSkinItemStatuses();
            UpdateBGsItemStatuses();
        }

        private void CreateShopItems()
        {
            Init1();

            Init2();
        }

        private void Init2()
        {
            _bgsDisplayList = _shopItemsFactory.CreateBGsDisplayList();
            foreach (var display in _bgsDisplayList)
                display.OnClick += ProcessBGItemClick;
        }

        private void Init1()
        {
            _skinsDisplayList = _shopItemsFactory.CreateSkinsDisplayList();
            foreach (var display in _skinsDisplayList)
                display.OnClick += ProcessSkinItemClick;
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.ShopWindow);
        }

        private void MakeWindow()
        {
            _window = _userInterfaceHelper.RetrieveWindow<ShopWindow>(WindowNames.ShopWindow);
            _window.Initialize(_skinsDisplayList, _bgsDisplayList);
            _window.Reveal().Forget();
        }

        private void Sub()
        {
            _window.OnBackPressed += async () => await SwitchTo<MainWindowState>();
        }

        private void UpdateSkinItemStatuses()
        {
            for (int i = 0; i < _skinsDisplayList.Count; i++)
            {
                var itemDisplay = _skinsDisplayList[i];
                itemDisplay.SetStatus(_shopService.GetSkinItemStatus(i));
            }
        }
        
        private void UpdateBGsItemStatuses()
        {
            for (int i = 0; i < _bgsDisplayList.Count; i++)
            {
                var itemDisplay = _bgsDisplayList[i];
                itemDisplay.SetStatus(_shopService.GetBGItemStatus(i));
            }
        }

        private void ProcessSkinItemClick(ShopItemView view)
        {
            var shopItem = view.ShopItem;

            if (_shopService.IsUsedSkin(shopItem))
            {
            }
            else if (_shopService.IsPurchasedSkin(shopItem))
            {
                _shopService.SetActiveSkin(shopItem);
                _soundService.PlaySound(ConstAudioNames.SelectSound);   
            }
            else if (_shopService.CanPurchase(shopItem))
            {
                _shopService.PurchaseSkin(shopItem);
                AccomplishmentsEventInvoker.InvokeOnBallPurchased();
                _soundService.PlaySound(ConstAudioNames.SelectSound);   
            }
            else
            {
                _window.ShowError();
                _soundService.PlaySound(ConstAudioNames.ErrorSound);
            }

            UpdateSkinItemStatuses();
        }

        private void ProcessBGItemClick(ShopItemView view)
        {
            var shopItem = view.ShopItem;

            if (_shopService.IsUsedBG(shopItem))
            {
                
            }
            else if (_shopService.IsPurchasedBg(shopItem))
            {
                _shopService.SetActiveBg(shopItem);
                _soundService.PlaySound(ConstAudioNames.SelectSound);   
            }
            else if (_shopService.CanPurchase(shopItem))
            {
                _shopService.PurchaseBG(shopItem);
                AccomplishmentsEventInvoker.InvokeOnBackgroundPurchased();
                _soundService.PlaySound(ConstAudioNames.SelectSound);   
            }
            else
            {
                _window.ShowError();
                _soundService.PlaySound(ConstAudioNames.ErrorSound);
            }

            UpdateBGsItemStatuses();
        }
    }
}