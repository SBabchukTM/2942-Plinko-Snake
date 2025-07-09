using Runtime.Game.Achievements;
using Runtime.Game.Gameplay.Effects;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class Ghost : GameItem
    {
        private GhostPool _pool;
        private GhostEffect _effect;

        [Inject]
        private void Construct(GhostPool pool, GhostEffect effect)
        {
            _pool = pool;
            _effect = effect;
        }

        protected override void OnPlayerCollision(Node node)
        {
            PoolMe();
            ProcessGhost();
        }

        private void ProcessGhost()
        {
            _effect.StartEffect();   
            AccomplishmentsEventInvoker.InvokeOnGhostUsed();
            SoundService.PlaySound(ConstAudioNames.GhostSound);
        }

        public override void PoolMe() => _pool.ReturnToPool(this);
    }
}
