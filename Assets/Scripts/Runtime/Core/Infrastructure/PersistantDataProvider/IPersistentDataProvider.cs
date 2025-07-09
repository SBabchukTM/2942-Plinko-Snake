using Runtime.Core.Infrastructure.Serializer;

namespace Runtime.Core.Infrastructure.PersistantDataProvider
{
    public interface IPersistentDataProvider
    {
        T Load<T>(string path, string fileName, ISerializationProvider serializationProvider = null) where T : class;

        bool Save<T>(T data, string path, string fileName, ISerializationProvider serializationProvider = null) where T : class;
    }
}