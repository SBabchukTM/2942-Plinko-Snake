using UnityEngine;

namespace Runtime.Game.ShopSystem
{
    [CreateAssetMenu(fileName = "BackgroundSetConfig", menuName = "Config/BackgroundSetConfig", order = 0)]
    public class BackgroundShopItemConfig : ScriptableObject
    {
        public ShopItem ShopItem;
        public Sprite BlockSprite;
        public Sprite LongItemSprite;
        public Sprite ShortItemSprite;
    }
}
