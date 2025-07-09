using System.Collections.Generic;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Game.ShopSystem
{
    public class ShopItemsFactory : IInitializable
    {
        private readonly IConfiguratioGetter _configuratioGetter;
        private readonly IObjectGetterService _objectGetterService;
        private readonly GameObjectFactory _gameObjectFactory;
    
        private GameObject _skinPrefab;
        private GameObject _bgPrefab;
    
        public ShopItemsFactory(IConfiguratioGetter configuratioGetter, IObjectGetterService objectGetterService,
            GameObjectFactory gameObjectFactory)
        {
            _configuratioGetter = configuratioGetter;
            _objectGetterService = objectGetterService;
            _gameObjectFactory = gameObjectFactory;
        }

        public async void Initialize()
        {
            _skinPrefab = await _objectGetterService.Load<GameObject>(PrefabNames.ShopItemDisplayPrefab);
            _bgPrefab = await _objectGetterService.Load<GameObject>(PrefabNames.BgShopItemDisplayPrefab);
        }

        public List<ShopItemView> CreateSkinsDisplayList()
        {
            List<ShopItemView> shopItemDisplayList = new List<ShopItemView>();

            var skinsData = _configuratioGetter.Get<ShopConfig>().SkinItems;
            foreach (var skinData in skinsData)
            {
                var display = _gameObjectFactory.Create<ShopItemView>(_skinPrefab);
                display.Setup(skinData);
            
                shopItemDisplayList.Add(display);
            }
        
            return shopItemDisplayList;
        }
    
        public List<ShopItemView> CreateBGsDisplayList()
        {
            List<ShopItemView> shopItemDisplayList = new List<ShopItemView>();

            var skinsData = _configuratioGetter.Get<ShopConfig>().BackgroundSets;
            foreach (var skinData in skinsData)
            {
                var display = _gameObjectFactory.Create<ShopItemView>(_bgPrefab);
                display.Setup(skinData.ShopItem);
            
                shopItemDisplayList.Add(display);
            }
        
            return shopItemDisplayList;
        }
    }
}
