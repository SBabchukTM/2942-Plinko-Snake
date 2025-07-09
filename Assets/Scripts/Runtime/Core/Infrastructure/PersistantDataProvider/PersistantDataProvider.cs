using System;
using Runtime.Core.Infrastructure.FileStorageService;
using Runtime.Core.Infrastructure.Serializer;

namespace Runtime.Core.Infrastructure.PersistantDataProvider
{
    public class PersistantDataProvider : IPersistentDataProvider
    {
        private readonly IDataService _dataService;
        private readonly ISerializationProvider _defaultSerializationProvider;

        public PersistantDataProvider(IDataService dataService,
            ISerializationProvider defaultSerializationProvider)
        {
            _dataService = dataService;
            _defaultSerializationProvider = defaultSerializationProvider;
        }

        public T Load<T>(string path, string fileName, ISerializationProvider serializationProvider = null) where T : class
        {
            try
            {
                var text = _dataService.LoadData(path, fileName);

                if (string.IsNullOrEmpty(text))
                    return null;

                var serializer = serializationProvider ?? _defaultSerializationProvider;
                return string.IsNullOrEmpty(text) ? default : serializer.Deserialize<T>(text);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Save<T>(T data, string path, string fileName, ISerializationProvider serializationProvider = null) where T : class
        {
            try
            {
                var serializer = serializationProvider ?? _defaultSerializationProvider;

                var text = serializer.Serialize(data);
                _dataService.SaveData(text, path, fileName);
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}