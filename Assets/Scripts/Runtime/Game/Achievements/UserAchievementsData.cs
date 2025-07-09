using System;
using UnityEngine.Serialization;

namespace Runtime.Game.Achievements
{
    [Serializable]
    public class UserAchievementsData
    {
        public int GamesFinished;
        public int CoinsPicked;
    
        [FormerlySerializedAs("FinishOneGameAchievement")] public AccomplishmentData _finishOneGameAccomplishment = new()
        {
            Text = "Finish One Game",
            Reward = 10,
        };
    
        [FormerlySerializedAs("FinishThreeGamesAchievement")] public AccomplishmentData _finishThreeGamesAccomplishment = new()
        {
            Text = "Finish Three Games",
            Reward = 25,
        };
    
        [FormerlySerializedAs("FinishTenGamesAchievement")] public AccomplishmentData _finishTenGamesAccomplishment = new()
        {
            Text = "Finish Ten Games",
            Reward = 50,
        };
    
        [FormerlySerializedAs("PickTenCoinsAchievement")] public AccomplishmentData _pickTenCoinsAccomplishment = new()
        {
            Text = "Pickup Ten Coins",
            Reward = 10,
        };
    
        [FormerlySerializedAs("PickFiftyCoinsAchievement")] public AccomplishmentData _pickFiftyCoinsAccomplishment = new()
        {
            Text = "Pickup Fifty Coins",
            Reward = 50,
        };
    
        [FormerlySerializedAs("PickThousandAchievement")] public AccomplishmentData _pickThousandAccomplishment = new()
        {
            Text = "Pickup a Thousand Coins",
            Reward = 1000,
        };
    
        [FormerlySerializedAs("LevelFiveAchievement")] public AccomplishmentData _levelFiveAccomplishment = new()
        {
            Text = "Reach Level Five",
            Reward = 25,
        };
    
        [FormerlySerializedAs("LevelTenAchievement")] public AccomplishmentData _levelTenAccomplishment = new()
        {
            Text = "Reach Level Ten",
            Reward = 25,
        };
    
        [FormerlySerializedAs("LevelTwentyAchievement")] public AccomplishmentData _levelTwentyAccomplishment = new()
        {
            Text = "Reach Level Twenty",
            Reward = 50,
        };
    
        [FormerlySerializedAs("LevelFiftyAchievement")] public AccomplishmentData _levelFiftyAccomplishment = new()
        {
            Text = "Reach Level Fifty",
            Reward = 100,
        };
    
        [FormerlySerializedAs("PurchaseBallAchievement")] public AccomplishmentData _purchaseBallAccomplishment = new()
        {
            Text = "Purchase Skin",
            Reward = 15,
        };
    
        [FormerlySerializedAs("PurchaseBgAchievement")] public AccomplishmentData _purchaseBgAccomplishment = new()
        {
            Text = "Purchase Background Set",
            Reward = 30,
        };
    
        [FormerlySerializedAs("DailyAchievement")] public AccomplishmentData _dailyAccomplishment = new()
        {
            Text = "Participate In a Daily Bonus Game",
            Reward = 50,
        };
    
        [FormerlySerializedAs("MagnetAchievement")] public AccomplishmentData _magnetAccomplishment = new()
        {
            Text = "Pickup a Magnet",
            Reward = 10,
        };
    
        [FormerlySerializedAs("RocketAchievement")] public AccomplishmentData _rocketAccomplishment = new()
        {
            Text = "Pickup a Booster",
            Reward = 10,
        };
    
        [FormerlySerializedAs("BombAchievement")] public AccomplishmentData _bombAccomplishment = new()
        {
            Text = "Blow Something Up With a Bomb",
            Reward = 25,
        };
    
        [FormerlySerializedAs("GhostAchievement")] public AccomplishmentData _ghostAccomplishment = new()
        {
            Text = "Pickup an Invincibility Booster",
            Reward = 10,
        };
    
        [FormerlySerializedAs("LengthTenAchievement")] public AccomplishmentData _lengthTenAccomplishment = new()
        {
            Text = "Reach a Snake's Length of Ten",
            Reward = 25,
        };
    
        [FormerlySerializedAs("LengthTwentyFiveAchievement")] public AccomplishmentData _lengthTwentyFiveAccomplishment = new()
        {
            Text = "Reach a Snake's Length of Twenty Five",
            Reward = 100,
        };
    
        [FormerlySerializedAs("LengthFiftyAchievement")] public AccomplishmentData _lengthFiftyAccomplishment = new()
        {
            Text = "Reach a Snake's Length of Fifty",
            Reward = 250,
        };
    
        [FormerlySerializedAs("LengthHundredAchievement")] public AccomplishmentData _lengthHundredAccomplishment = new()
        {
            Text = "Reach a Snake's Length of a Hundred",
            Reward = 1000,
        };
    }
}
