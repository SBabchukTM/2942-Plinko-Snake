using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.UserData;
using Runtime.Game.ShopSystem;

namespace Runtime.Game.Dailies
{
    public class DailyRewardGenerator
    {
        private const float CoinChance = 0.6f;
        private const float SkinChance = 0.3f;
        private const float NoneChance = 0.1f;

        private static readonly Random _random = new();

        private readonly IConfiguratioGetter _configuratioGetter;
        private readonly UserInformationHelper _userInformationHelper;

        public DailyRewardGenerator(IConfiguratioGetter configuratioGetter, UserInformationHelper userInformationHelper)
        {
            _configuratioGetter = configuratioGetter;
            _userInformationHelper = userInformationHelper;
        }

        public List<DailyReward> GenerateRewards(List<DailyRouletteSlotDisplay> bonusRouletteSlots)
        {
            var shopConfig = _configuratioGetter.Get<ShopConfig>();

            var purchasedSkins = _userInformationHelper.GetSerializedData().UserInventoryData.PurchasedSkinIDs;
            var allSkinIndices = Enumerable.Range(0, shopConfig.SkinItems.Count).ToList();
            var unpurchasedSkins = allSkinIndices.Except(purchasedSkins).ToList();

            var rewards = new List<DailyReward>();
            var guaranteedNonNoneSlots = 1;
            var nonNoneCount = 0;

            foreach (var slot in bonusRouletteSlots)
            {
                var mustGiveRealReward = bonusRouletteSlots.Count - rewards.Count <= guaranteedNonNoneSlots - nonNoneCount;

                var rewardType = GetBalancedRewardType(unpurchasedSkins.Count > 0, mustGiveRealReward);

                var reward = new DailyReward { RewardType = rewardType };

                switch (rewardType)
                {
                    case RewardType.Coin:
                        reward.RewardValue = GetRandomCoinValue();
                        reward.RewardSprite = shopConfig.CoinSprite;
                        nonNoneCount++;
                        break;

                    case RewardType.Skin:
                        var index = GetRandomUnpurchasedSkinIndex(unpurchasedSkins);
                        reward.RewardValue = index;
                        reward.RewardSprite = shopConfig.SkinItems[index].Sprite;
                        nonNoneCount++;
                        break;

                    case RewardType.None:
                        reward.RewardValue = 0;
                        reward.RewardSprite = shopConfig.NothingSprite;
                        break;
                }

                rewards.Add(reward);
            }

            return rewards;
        }

        private RewardType GetBalancedRewardType(bool hasUnpurchasedSkins, bool forceRealReward)
        {
            var chances = new Dictionary<RewardType, float>();

            if (hasUnpurchasedSkins)
            {
                chances[RewardType.Coin] = CoinChance;
                chances[RewardType.Skin] = SkinChance;
                chances[RewardType.None] = forceRealReward ? 0 : NoneChance;
            }
            else
            {
                chances[RewardType.Coin] = CoinChance;
                chances[RewardType.None] = forceRealReward ? 0 : NoneChance;
            }

            var total = chances.Values.Sum();
            var roll = (float)_random.NextDouble() * total;

            float cumulative = 0;
            foreach (var kvp in chances)
            {
                cumulative += kvp.Value;
                if (roll <= cumulative)
                    return kvp.Key;
            }

            return RewardType.Coin;
        }

        private int GetRandomCoinValue()
        {
            int[] coinValues = { 100, 250, 500 };
            return coinValues[_random.Next(coinValues.Length)];
        }

        private int GetRandomUnpurchasedSkinIndex(List<int> unpurchasedSkins)
        {
            if (unpurchasedSkins.Count == 0)
                return -1;

            return unpurchasedSkins[_random.Next(unpurchasedSkins.Count)];
        }
    }
}