using Runtime.Core.Infrastructure.PersistantDataProvider;
using Runtime.Game.Services.Path;
using Runtime.Game.Services.UserData.Data;

namespace Runtime.Game.Services.UserData
{
    public class UserInformationHelper
    {
        private readonly IPersistentDataProvider _persistentDataProvider;

        private SerializedData _serializedData;

        public UserInformationHelper(IPersistentDataProvider persistentDataProvider)
        {
            _persistentDataProvider = persistentDataProvider;
        }

        public void Initialize()
        {
#if DEV
            _serializedData = _persistentDataProvider.Load<SerializedData>(ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName) ?? new SerializedData();
#else
            _serializedData = _persistentDataProvider.Load<SerializedData>(ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName, null) ?? new SerializedData();
#endif
        }

        public SerializedData GetSerializedData() => _serializedData;

        public void SaveUserData()
        {
            if (_serializedData == null)
                return;

#if DEV
            _persistentDataProvider.Save(_serializedData, ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName);
#else
            _persistentDataProvider.Save(_serializedData, ConstDataPath.UserDataPath, ConstDataPath.UserDataFileName, null);
#endif
        }
    }
}