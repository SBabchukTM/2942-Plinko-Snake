using System.Collections.Generic;
using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.ShopSystem
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "Configs/ShopConfig")]
    public class ShopConfig : BaseConfigSO
    {
        public List<BackgroundShopItemConfig> BackgroundSets = new List<BackgroundShopItemConfig>();
        public List<ShopItem> SkinItems = new();
        public Sprite CoinSprite;
        public Sprite NothingSprite;
    }
}