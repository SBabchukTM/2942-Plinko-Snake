using System.Collections.Generic;
using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;

namespace Runtime.Core.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Config/AudioConfig")]
    public sealed class AudioConfig : BaseConfigSO
    {
        public List<AudioData> Audio;
    }
}