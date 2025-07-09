using System;
using Runtime.Core.Infrastructure.Logger;
using UnityEngine;

namespace Runtime.Core.Infrastructure.Serializer
{
    public class JsonSerializationProvider : ISerializationProvider
    {
        protected readonly IDebugger Debugger;


        public JsonSerializationProvider(IDebugger debugger)
        {
            Debugger = debugger;
        }

        public T Deserialize<T>(string text) where T : class
        {
            try
            {
                var result = JsonUtility.FromJson<T>(text);

                return result;
            }
            catch (Exception exception)
            {
                Debugger.Error($"{exception.GetType()}: Could not parse JSON {text}. Exception: {exception.Message}");
                return default;
            }
        }

        public string Serialize<T>(T obj) where T : class
        {
            try
            {
                var result = JsonUtility.ToJson(obj);

                return result;
            }
            catch (Exception exception)
            {
                Debugger.Error(
                    $"{exception.GetType()}: Could not serialize object {typeof(T)}. Exception: {exception.Message}");
                return default;
            }
        }
    }
}