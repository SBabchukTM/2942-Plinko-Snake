using System;
using UnityEngine;

namespace Runtime.Game.Gameplay.Effects
{
    public class ExplosionEffect
    {
        public event Action<Vector3> OnExplode;
        
        public void Trigger(Vector3 position) => OnExplode?.Invoke(position);
    }
}