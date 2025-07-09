using Runtime.Game.Services.UserData;

namespace Runtime.Game.Achievements
{
    public class AccomplishmentsHelper
    {
        private readonly UserInformationHelper _userInformationHelper;

        public AccomplishmentsHelper(UserInformationHelper userInformationHelper)
        {
            _userInformationHelper = userInformationHelper;
        
            AccomplishmentsEventInvoker.OnGameFinished += ProcessGameFinished;
            AccomplishmentsEventInvoker.OnCoinPicked += ProcessCoinPicked;
            AccomplishmentsEventInvoker.OnLevelReached += ProcessLevelReached;
            AccomplishmentsEventInvoker.OnBallPurchased += ProcessBallPurchased;
            AccomplishmentsEventInvoker.OnBackgroundPurchased += ProcessBackgroundPurchased;
            AccomplishmentsEventInvoker.OnDailyBonus += ProcessDailyBonus;
            AccomplishmentsEventInvoker.OnMagnetUsed += ProcessMagnet;
            AccomplishmentsEventInvoker.OnRocketUsed += ProcessRocket;
            AccomplishmentsEventInvoker.OnBombUsed += ProcessBomb;
            AccomplishmentsEventInvoker.OnGhostUsed += ProcessGhost;
            AccomplishmentsEventInvoker.OnSnakeLengthReached += ProcessSnakeLength;
        }

        private void ProcessSnakeLength(int len)
        {
            if(len >= 10)
                UnlockAchievement(GetAchievementsData()._lengthTenAccomplishment);
        
            if(len >= 25)
                UnlockAchievement(GetAchievementsData()._lengthTwentyFiveAccomplishment);
        
            if(len >= 50)
                UnlockAchievement(GetAchievementsData()._lengthFiftyAccomplishment);
        
            if(len >= 100)
                UnlockAchievement(GetAchievementsData()._lengthHundredAccomplishment);
        }

        private void ProcessGhost() => UnlockAchievement(GetAchievementsData()._ghostAccomplishment);

        private void ProcessBomb() => UnlockAchievement(GetAchievementsData()._bombAccomplishment);

        private void ProcessRocket() => UnlockAchievement(GetAchievementsData()._rocketAccomplishment);

        private void ProcessMagnet() => UnlockAchievement(GetAchievementsData()._magnetAccomplishment);

        private void ProcessDailyBonus() => UnlockAchievement(GetAchievementsData()._dailyAccomplishment);

        private void ProcessBackgroundPurchased() => UnlockAchievement(GetAchievementsData()._purchaseBgAccomplishment);

        private void ProcessBallPurchased() => UnlockAchievement(GetAchievementsData()._purchaseBallAccomplishment);

        private void ProcessLevelReached(int level)
        {
            if(level >= 5)
                UnlockAchievement(GetAchievementsData()._levelFiveAccomplishment);
        
            if(level >= 10)
                UnlockAchievement(GetAchievementsData()._levelTenAccomplishment);
        
            if(level >= 20)
                UnlockAchievement(GetAchievementsData()._levelTwentyAccomplishment);
        
            if(level >= 50)
                UnlockAchievement(GetAchievementsData()._levelFiftyAccomplishment);
        }

        private void ProcessCoinPicked()
        {
            int coinsPicked = GetAchievementsData().CoinsPicked;
            coinsPicked++;
            GetAchievementsData().CoinsPicked = coinsPicked;
        
            if (coinsPicked >= 10)
                UnlockAchievement(GetAchievementsData()._pickTenCoinsAccomplishment);
        
            if (coinsPicked >= 50)
                UnlockAchievement(GetAchievementsData()._pickFiftyCoinsAccomplishment);
        
            if (coinsPicked >= 1000)
                UnlockAchievement(GetAchievementsData()._pickThousandAccomplishment);
        }

        private void ProcessGameFinished()
        {
            int gamesFinished = GetAchievementsData().GamesFinished;
            gamesFinished++;

            GetAchievementsData().GamesFinished = gamesFinished;
        
            if (gamesFinished >= 1)
                UnlockAchievement(GetAchievementsData()._finishOneGameAccomplishment);
        
            if (gamesFinished >= 3)
                UnlockAchievement(GetAchievementsData()._finishThreeGamesAccomplishment);
        
            if (gamesFinished >= 10)
                UnlockAchievement(GetAchievementsData()._finishTenGamesAccomplishment);
        }

        private void UnlockAchievement(AccomplishmentData data)
        {
            if (!data.Unlocked)
                data.Unlocked = true;
        }

        private UserAchievementsData GetAchievementsData() => _userInformationHelper.GetSerializedData().UserAchievementsData;
    }
}
