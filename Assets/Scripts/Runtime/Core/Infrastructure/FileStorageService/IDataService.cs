namespace Runtime.Core.Infrastructure.FileStorageService
{
    public interface IDataService
    {
        void SaveData(string data, string filePath, string fileName);
        string LoadData(string filePath, string fileName);
    }
}