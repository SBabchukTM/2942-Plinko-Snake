﻿namespace Runtime.Game.Services.Path
{
    public static class ConstDataPath
    {
        private const string GameFolderPath = "Game";

        public const string UserDataFileName = "UserData";

        public static string PersistantDataPath => UnityEngine.Application.persistentDataPath;
        public static string UserDataPath => $"{PersistantDataPath}/{GameFolderPath}";
    }
}