using System;
using System.Collections.Generic;
using Runtime.Game.Services.UserData;

namespace Runtime.Game.ShopSystem
{
    public class InventoryHelper
    {
        private readonly UserInformationHelper _userInformationHelper;

        public event Action<int> CoinsChangedEvent;
        
        public InventoryHelper(UserInformationHelper userInformationHelper)
        {
            _userInformationHelper = userInformationHelper;
        }

        public int GetCoins() => GetUserInventoryData().Balance;
        
        public void AddCoins(int amount)
        {
            var balance = ChangeBalance(amount);
            CoinsChangedEvent?.Invoke(balance);
        }

        private int ChangeBalance(int amount)
        {
            var data = GetUserInventoryData();
            int balance = data.Balance;
            
            balance += amount;
            data.Balance = balance;
            return balance;
        }

        public void AddNewBackground(int bgID)
        {
            var data = GetUserInventoryData();
            data.PurchasedBGIDs.Add(bgID);
        }
        
        public void SetUsedBGId(int bgID) => GetUserInventoryData().UsedBGID = bgID;

        public int GetUsedBGItemId() => GetUserInventoryData().UsedBGID;
        public List<int> GetPurchasedBGItemIds() => GetUserInventoryData().PurchasedBGIDs;
        
        public void AddSkin(int skinID)
        {
            var data = GetUserInventoryData();
            data.PurchasedSkinIDs.Add(skinID);
        }
        
        public void SetUsedSkinId(int skinID) => GetUserInventoryData().UsedBallSkinID = skinID;

        public int GetUsedSkinItemId() => GetUserInventoryData().UsedBallSkinID;
        public List<int> GetPurchasedSkinItemIds() => GetUserInventoryData().PurchasedSkinIDs;
        
        private UserInventoryData GetUserInventoryData() => _userInformationHelper.GetSerializedData().UserInventoryData;
    }
}