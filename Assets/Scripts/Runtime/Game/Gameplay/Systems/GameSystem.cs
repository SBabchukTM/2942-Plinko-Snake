namespace Runtime.Game.Gameplay.Systems
{
    public abstract class GameSystem : IEnableable, IResettable
    {
        protected bool Enabled;

        public GameSystem(ManagerOfSystems manager) => manager.Register(this);

        public virtual void Enable(bool enable) => Enabled = enable;

        public abstract void Reset();
    }
}