using Runtime.Game.Gameplay.Misc;
using Runtime.Game.Gameplay.Snake;

namespace Runtime.Game.Gameplay.Systems
{
    public class GameSetupController
    {
        private readonly SpritesProvider _spritesProvider;
        private readonly ManagerOfSystems _managerOfSystems;
        private readonly GameBgScroller _gameBgScroller;
        private readonly GameData _gameData;
        private readonly NodeManager _nodesManager;

        public GameSetupController(SpritesProvider spritesProvider, ManagerOfSystems managerOfSystems, 
            GameBgScroller gameBgScroller, GameData gameData,
            NodeManager nodesManager)
        {
            _spritesProvider = spritesProvider;
            _managerOfSystems = managerOfSystems;
            _gameBgScroller = gameBgScroller;
            _gameData = gameData;
            _nodesManager = nodesManager;
        }

        public void Setup()
        {
            ResetGameplayData();
            _gameBgScroller.SetSkin(_spritesProvider.GetBackgroundSkin());
            _managerOfSystems.ResetAll();
            _managerOfSystems.EnableAll(true);

            _nodesManager.CreateSnake(5);
        }

        public void End()
        {
            _managerOfSystems.EnableAll(false);
            _nodesManager.RemoveSnake();
            _managerOfSystems.CleanupAll();
        }

        private void ResetGameplayData()
        {
            _gameData.CoinsCollected = 0;
            _gameData.GameLevel = 0;
            _gameData.TotalDistanceTravelled = 0;
            _gameData.TotalScore = 0;
            _gameData.LevelProgress = 0;
        }
    }
}
