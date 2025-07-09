using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Runtime.Core.GameStateMachine
{
    public class StateController
    {
        private Dictionary<Type, State> _states;
        private State _activeState;
        
        public void Setup(params State[] stateControllers)
        {
            _states = new Dictionary<Type, State>(stateControllers.Length);
            AddState(stateControllers);
        }

        private void AddState(State[] stateControllers)
        {
            foreach (var executiveStateController in stateControllers)
            {
                _states.Add(executiveStateController.GetType(), executiveStateController);
                executiveStateController.Initialize(this);
            }
        }

        public async UniTask EnterState<TState>(CancellationToken cancellationToken = default) where TState : State
        {
            State state = await SwitchState<TState>();
            await state.Switch(cancellationToken);
        }
        

        private async UniTask<TState> SwitchState<TState>() where TState : State
        {
            if (_activeState != null)
                await _activeState.Leave();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class => _states[typeof(TState)] as TState;
    }
}