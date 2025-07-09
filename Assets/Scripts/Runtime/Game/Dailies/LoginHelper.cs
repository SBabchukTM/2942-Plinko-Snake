using System;
using Runtime.Game.Services.UserData;

namespace Runtime.Game.Dailies
{
    public class LoginHelper
    {
        private readonly UserInformationHelper _userInformationHelper;

        public LoginHelper(UserInformationHelper userInformationHelper)
        {
            _userInformationHelper = userInformationHelper;
        }

        public bool IsSpinAvailable()
        {
            var lastLoginDateString  = GetLastSpinDate();
            if (lastLoginDateString == String.Empty)
                return true;
         
            var lastLoginDate = Convert.ToDateTime(lastLoginDateString);
            return DateTime.Now.Date > lastLoginDate.Date;
        }
    
        public void RecordCurrentDate()
        {
            _userInformationHelper.GetSerializedData().DailyLoginData.LastDate = DateTime.Now.ToString();
            _userInformationHelper.SaveUserData();
        }
    
        private string GetLastSpinDate() => _userInformationHelper.GetSerializedData().DailyLoginData.LastDate;
    }
}