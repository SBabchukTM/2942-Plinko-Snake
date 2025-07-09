using Runtime.Core.Audio;
using UnityEngine;

namespace Runtime.Core.Extensions
{
    public static class AudioExtension
    {
        public static AudioClip GetData(this AudioConfig config, string clipId)
        {
            var audioData = config.Audio.Find(x => x.Id == clipId);
            return audioData.Clip;
        }
    }
}