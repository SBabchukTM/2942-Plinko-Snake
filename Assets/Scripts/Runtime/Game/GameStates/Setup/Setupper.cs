using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Game.GameStates.Game;
using Zenject;

namespace Runtime.Game.GameStates.Setup
{
    public class Setupper : IInitializable
    {
        private readonly StateController _stateController;
        private readonly SetupperState _setupperState;
        private readonly GameState _gameState;

        public Setupper(StateController stateController, SetupperState setupperState, GameState gameState)
        {
            _stateController = stateController;
            _setupperState = setupperState;
            _gameState = gameState;
        }

        public void Initialize()
        {
            UnityEngine.Application.targetFrameRate = 60;
            _stateController.Setup(_setupperState, _gameState);
            _stateController.EnterState<SetupperState>().Forget();
        }
    }
}