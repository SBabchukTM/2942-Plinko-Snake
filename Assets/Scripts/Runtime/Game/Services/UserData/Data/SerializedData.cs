using System;
using System.Collections.Generic;
using Runtime.Game.Achievements;
using Runtime.Game.ShopSystem;
using UnityEngine.Serialization;

namespace Runtime.Game.Services.UserData.Data
{
    [Serializable]
    public class SerializedData
    {
        public SettingsData SettingsData = new SettingsData();
        public DailyLoginData DailyLoginData = new DailyLoginData();
        public UserInventoryData UserInventoryData = new UserInventoryData();
        public ScoreData _scoreData = new ScoreData();
        public ProfileData ProfileData = new ProfileData();
        public UserAchievementsData UserAchievementsData = new UserAchievementsData();
    }
}