using System.Collections.Generic;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Game.Services.SettingsProvider;
using Runtime.Game.Services.UserData;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Achievements
{
    public class AchievementsFactory : IInitializable
    {
        private readonly UserInformationHelper _userInformationHelper;
        private readonly GameObjectFactory _gameObjectFactory;
        private readonly IObjectGetterService _objectGetterService;

        private GameObject _prefab;

        public AchievementsFactory(UserInformationHelper userInformationHelper, GameObjectFactory gameObjectFactory,
            IObjectGetterService objectGetterService)
        {
            _userInformationHelper = userInformationHelper;
            _gameObjectFactory = gameObjectFactory;
            _objectGetterService = objectGetterService;
        }
        
        public async void Initialize()
        {
            _prefab = await _objectGetterService.Load<GameObject>(PrefabNames.AchieventPrefab);
        }

        public List<AccomplishmentView> GetAchievements()
        {
            List<AccomplishmentView> achievements = new List<AccomplishmentView>();
            
            Create1(achievements);
            Create2(achievements);
            Create3(achievements);
            Create4(achievements);

            return achievements;
        }

        private void Create4(List<AccomplishmentView> achievements)
        {
            achievements.Add(CreateAccomplishment(GetAchievementsData()._ghostAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._lengthTenAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._lengthTwentyFiveAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._lengthFiftyAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._lengthHundredAccomplishment));
        }

        private void Create3(List<AccomplishmentView> achievements)
        {
            achievements.Add(CreateAccomplishment(GetAchievementsData()._purchaseBgAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._dailyAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._magnetAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._rocketAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._bombAccomplishment));
        }

        private void Create2(List<AccomplishmentView> achievements)
        {
            achievements.Add(CreateAccomplishment(GetAchievementsData()._pickThousandAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._levelFiveAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._levelTenAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._levelTwentyAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._levelFiftyAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._purchaseBallAccomplishment));
        }

        private void Create1(List<AccomplishmentView> achievements)
        {
            achievements.Add(CreateAccomplishment(GetAchievementsData()._finishOneGameAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._finishThreeGamesAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._finishTenGamesAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._pickTenCoinsAccomplishment));
            achievements.Add(CreateAccomplishment(GetAchievementsData()._pickFiftyCoinsAccomplishment));
        }

        private UserAchievementsData GetAchievementsData() => _userInformationHelper.GetSerializedData().UserAchievementsData;
        
        private AccomplishmentView CreateAccomplishment(AccomplishmentData data)
        {
            var display = _gameObjectFactory.Create<AccomplishmentView>(_prefab);
            display.Setup(data);
            return display; 
        }
    }
}