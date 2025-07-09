using System;
using System.IO;
using Runtime.Core.Infrastructure.Logger;

namespace Runtime.Core.Infrastructure.FileStorageService
{
    public sealed class PersistentDataService : IDataService
    {
        private readonly IDebugger _debugger;

        public PersistentDataService(IDebugger debugger)
        {
            _debugger = debugger;
        }

        public void SaveData(string data, string filePath, string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                _debugger.Error("Failed save text, no file name");
                return;
            }
            
            if(string.IsNullOrEmpty(filePath))
            {
                _debugger.Error("Failed save text, no file path");
                return;
            }

            if(string.IsNullOrEmpty(data))
            {
                _debugger.Error("Failed save text, data is empty");
                return;
            }

            try
            {
                if(!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var path = Path.Combine(filePath, fileName);
                File.WriteAllText(path, data);
            }
            catch (Exception e)
            {
                _debugger.Error($"Failed save text: {e}");
            }
        }

        public string LoadData(string filePath, string fileName)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                _debugger.Error("Failed load text, path is empty");
                return null;
            }

            if(string.IsNullOrEmpty(fileName))
            {
                _debugger.Error("Failed load text, no file name");
                return null;
            }

            if(!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                _debugger.Warning($"Directory not exist: {filePath}");
                return null;
            }

            var path = Path.Combine(filePath, fileName);

            if(!File.Exists(path))
            {
                _debugger.Warning($"File not exist: {path}");
                return null;
            }

            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                _debugger.Error($"Failed load text: {e}");
                return null;
            }
        }
    }
}