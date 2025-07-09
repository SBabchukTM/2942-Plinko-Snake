using System.Collections.Generic;

namespace Runtime.Game.Gameplay.Systems
{
    public class ManagerOfSystems
    {
        private readonly List<IEnableable> _enableables = new List<IEnableable>();
        private readonly List<IResettable> _resettables = new List<IResettable>();
        private readonly List<ICleanup> _cleanups = new List<ICleanup>();

        public void Register(object system)
        {
            if(system is IEnableable enableable)
                _enableables.Add(enableable);
        
            if(system is IResettable resettable)
                _resettables.Add(resettable);
        
            if(system is ICleanup cleanup)
                _cleanups.Add(cleanup);
        }
    
        public void EnableAll(bool enable) => _enableables.ForEach(enableable => enableable.Enable(enable));
        public void ResetAll() => _resettables.ForEach(resettable => resettable.Reset());
        public void CleanupAll() => _cleanups.ForEach(cleanup => cleanup.Cleanup());
    }
}
