using UnityEngine;

namespace Runtime.Core.Audio
{
    public interface ISoundService
    {
        void PlayMusic(AudioClip clip);
        void PlaySound(string clipId);
        void SetVolume(AudioType audioType, float volume);
    }
}