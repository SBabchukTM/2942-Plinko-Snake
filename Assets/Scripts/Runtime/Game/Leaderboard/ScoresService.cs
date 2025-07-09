using Runtime.Game.Services.UserData;
using Runtime.Game.Services.UserData.Data;

namespace Runtime.Game.Leaderboard
{
    public class ScoresService
    {
        private readonly UserInformationHelper _userInformationHelper;

        public ScoresService(UserInformationHelper userInformationHelper)
        {
            _userInformationHelper = userInformationHelper;
        }

        public ScoreData GetUserRecord()
        {
            return new ScoreData()
            {
                Name = _userInformationHelper.GetSerializedData().ProfileData.PlayerName,
                Score = GetHighestScore(),
            };
        }
        
        public void RecordScore(int score)
        {
            int lastScore = GetHighestScore();

            if (score > lastScore)
            {
                GetRecordData().Score = score;
            }
        }

        public int GetHighestScore() => GetRecordData().Score;

        private Services.UserData.Data.ScoreData GetRecordData() => _userInformationHelper.GetSerializedData()._scoreData;
    }
}