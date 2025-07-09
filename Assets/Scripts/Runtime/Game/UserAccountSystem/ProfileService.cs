using System;
using Runtime.Game.Services.UserData;
using Runtime.Game.Services.UserData.Data;
using UnityEngine;

namespace Runtime.Game.UserAccountSystem
{
    public class ProfileService
    {
        private readonly UserInformationHelper _userInformationHelper;
        private readonly SpriteConverter _spriteConverter;
    
        public ProfileService(UserInformationHelper userInformationHelper, 
            SpriteConverter spriteConverter)
        {
            _userInformationHelper = userInformationHelper;
            _spriteConverter = spriteConverter;
        }
    
        public ProfileData GetProfileDataCopy() => _userInformationHelper.GetSerializedData().ProfileData.GetCopy();

        public void SaveAccountData(ProfileData modifiedData)
        {
            var origData = _userInformationHelper.GetSerializedData().ProfileData;

            foreach (var field in typeof(ProfileData).GetFields())
                field.SetValue(origData, field.GetValue(modifiedData));

            _userInformationHelper.SaveUserData();
        }

        public Sprite GetUsedAvatarSprite()
        {
            if (!AvatarExists())
                return null;
            
            return _spriteConverter.ToSprite(GetAvatarBase64());
        }

        public string ConvertToBase64(Sprite sprite, int maxSize = 512) =>
            _spriteConverter.ToString(sprite, maxSize);

        private bool AvatarExists() => _userInformationHelper.GetSerializedData().ProfileData.AvatarAsString != String.Empty;
        
        private string GetAvatarBase64() => _userInformationHelper.GetSerializedData().ProfileData.AvatarAsString;
    }
}
