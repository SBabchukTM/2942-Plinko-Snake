using Runtime.Game.Achievements;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class Rocket : GameItem
    {
        private RocketPool _pool;
        private GameplayZoomiesController _speedController;

        [Inject]
        private void Construct(RocketPool pool, GameplayZoomiesController speedController)
        {
            _pool = pool;
            _speedController = speedController;
        }

        protected override void OnPlayerCollision(Node node)
        {
            AccomplishmentsEventInvoker.InvokeOnRocketUsed();
            SoundService.PlaySound(ConstAudioNames.RocketSound);

            PoolMe();
            _speedController.ApplySpeedBuff();    
        }

        public override void PoolMe() => _pool.ReturnToPool(this);
    }
}
