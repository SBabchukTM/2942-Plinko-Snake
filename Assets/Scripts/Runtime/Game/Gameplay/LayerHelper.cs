using UnityEngine;

namespace Runtime.Game.Gameplay
{
    public static class LayerHelper
    {
        public static int GetLayerId(string name) => LayerMask.NameToLayer(name);
    }
}