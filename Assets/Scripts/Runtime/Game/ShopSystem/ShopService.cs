using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.UserData;
using UnityEngine;

namespace Runtime.Game.ShopSystem
{
    public class ShopService
    {
        private const string PurchasedStatus = "Use";
        private const string UsedStatus = "Active";
        private const string UnpurchasedStatus = "Buy";
        
        private readonly UserInformationHelper _userInformationHelper;
        private readonly IConfiguratioGetter _configuratioGetter;
        private readonly InventoryHelper _inventoryHelper;

        public ShopService(UserInformationHelper userInformationHelper, IConfiguratioGetter configuratioGetter, InventoryHelper inventoryHelper)
        {
            _userInformationHelper = userInformationHelper;
            _configuratioGetter = configuratioGetter;
            _inventoryHelper = inventoryHelper;
        }

        public string GetSkinItemStatus(int itemId)
        {
            if (_inventoryHelper.GetUsedSkinItemId() == itemId)
                return UsedStatus;
            
            if(_inventoryHelper.GetPurchasedSkinItemIds().Contains(itemId))
                return PurchasedStatus;
            
            return UnpurchasedStatus;
        }
        
        public bool CanPurchase(ShopItem shopItem) => _inventoryHelper.GetCoins() >= shopItem.Price;

        public void PurchaseSkin(ShopItem shopItem)
        {
            int id = GetItemIDSkin(shopItem);
            _inventoryHelper.AddCoins(-shopItem.Price);
            _inventoryHelper.AddSkin(id);
            _inventoryHelper.SetUsedSkinId(id);
        }
        
        public bool IsPurchasedSkin(ShopItem shopItem) =>
            _inventoryHelper.GetPurchasedSkinItemIds().Contains(GetItemIDSkin(shopItem));

        public void SetActiveSkin(ShopItem shopItem) => _inventoryHelper.SetUsedSkinId(GetItemIDSkin(shopItem));
         
        public bool IsUsedSkin(ShopItem shopItem) => _inventoryHelper.GetUsedSkinItemId() == GetItemIDSkin(shopItem);

        private int GetItemIDSkin(ShopItem shopItem) => GetShopConfig().SkinItems.FindIndex(x => x == shopItem);
        
        public string GetBGItemStatus(int itemId)
        {
            if (_inventoryHelper.GetUsedBGItemId() == itemId)
                return UsedStatus;
            
            if(_inventoryHelper.GetPurchasedBGItemIds().Contains(itemId))
                return PurchasedStatus;
            
            return UnpurchasedStatus;
        }

        public void PurchaseBG(ShopItem shopItem)
        {
            int id = GetItemIDBG(shopItem);
            _inventoryHelper.AddCoins(-shopItem.Price);
            _inventoryHelper.AddNewBackground(id);
            _inventoryHelper.SetUsedBGId(id);
        }
        
        public bool IsPurchasedBg(ShopItem shopItem) =>
            _inventoryHelper.GetPurchasedBGItemIds().Contains(GetItemIDBG(shopItem));
        public void SetActiveBg(ShopItem shopItem) => _inventoryHelper.SetUsedBGId(GetItemIDBG(shopItem));
        public bool IsUsedBG(ShopItem shopItem) => _inventoryHelper.GetUsedBGItemId() == GetItemIDBG(shopItem);
        private int GetItemIDBG(ShopItem shopItem) => GetShopConfig().BackgroundSets.FindIndex(x => x.ShopItem == shopItem);


        private ShopConfig GetShopConfig() => _configuratioGetter.Get<ShopConfig>();
    }
}