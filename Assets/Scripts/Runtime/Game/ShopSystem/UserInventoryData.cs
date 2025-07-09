using System;
using System.Collections.Generic;

namespace Runtime.Game.ShopSystem
{
    [Serializable]
    public class UserInventoryData
    {
        public int Balance = 0;
    
        public int UsedBallSkinID = 0;
        public List<int> PurchasedSkinIDs = new List<int>(){0};
    
        public int UsedBGID = 0;
        public List<int> PurchasedBGIDs = new List<int>(){0};
    }
}
