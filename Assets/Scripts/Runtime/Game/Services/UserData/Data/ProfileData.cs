using System;

namespace Runtime.Game.Services.UserData.Data
{
    [Serializable]
    public class ProfileData
    {
        public string PlayerName = "Player";
        public string AvatarAsString = string.Empty;
        
        public ProfileData GetCopy() => MemberwiseClone() as ProfileData;
    }
}