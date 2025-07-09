using Runtime.Game.Achievements;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Services.Audio;
using Zenject;

namespace Runtime.Game.Gameplay.Items
{
    public class CoinItem : GameItem
    {
        private GameData _gameData;
        private CoinPool _coinPool;

        [Inject]
        private void Construct(GameData gameData, CoinPool coinPool)
        {
            _gameData = gameData;
            _coinPool = coinPool;
        }

        protected override void OnPlayerCollision(Node node)
        {
            AddCoin();
            PoolMe(); 
            AccomplishmentsEventInvoker.InvokeOnCoinPicked();
            SoundService.PlaySound(ConstAudioNames.CoinSound);
        }

        private void AddCoin()
        {
            _gameData.CoinsCollected++;
        }

        public override void PoolMe() => _coinPool.ReturnToPool(this);
    }
}
