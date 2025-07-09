using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.UserData;
using Runtime.Game.ShopSystem;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Gameplay
{
    public class SpritesProvider
    {
        private readonly IConfiguratioGetter _configuratioGetter;
        private readonly UserInformationHelper _userInformationHelper;
        public SpritesProvider(IConfiguratioGetter configuratioGetter, UserInformationHelper userInformationHelper)
        {
            _configuratioGetter = configuratioGetter;
            _userInformationHelper = userInformationHelper;
        }
        public Sprite GetBallSkin()
        {
            var skins = _configuratioGetter.Get<ShopConfig>().SkinItems;
            return skins[GetUserInventoryData().UsedBallSkinID].Sprite;
        }

        public Sprite GetBackgroundSkin()
        {
            var sets = _configuratioGetter.Get<ShopConfig>().BackgroundSets;
            return sets[GetUserInventoryData().UsedBGID].ShopItem.Sprite;
        }

        public Sprite GetBlockSkin()
        {
            var sets = _configuratioGetter.Get<ShopConfig>().BackgroundSets;
            return sets[GetUserInventoryData().UsedBGID].BlockSprite;
        }
        
        public Sprite GetLongObstacleSkin()
        {
            var sets = _configuratioGetter.Get<ShopConfig>().BackgroundSets;
            return sets[GetUserInventoryData().UsedBGID].LongItemSprite;
        }
        
        public Sprite GetShortObstacleSkin()
        {
            var sets = _configuratioGetter.Get<ShopConfig>().BackgroundSets;
            return sets[GetUserInventoryData().UsedBGID].ShortItemSprite;
        }

        private UserInventoryData GetUserInventoryData() => _userInformationHelper.GetSerializedData().UserInventoryData;
    }
}