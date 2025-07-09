using System;
using UnityEngine.Serialization;

namespace Runtime.Game.Achievements
{
    [Serializable]
    public class AccomplishmentData
    {
        public string Text;
        public bool Unlocked;
        public bool Claimed;
        public int Reward;
    }
}
