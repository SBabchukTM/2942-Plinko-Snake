﻿namespace Runtime.Core.Infrastructure.Logger
{
    public class SimpleDebugger : IDebugger
    {
        public void Log(string message) => UnityEngine.Debug.Log(message);

        public void Warning(string message) => UnityEngine.Debug.LogWarning(message);

        public void Error(string message) => UnityEngine.Debug.LogError(message);
    }
}