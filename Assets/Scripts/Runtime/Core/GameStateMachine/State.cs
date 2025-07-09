using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Infrastructure.Logger;

namespace Runtime.Core.GameStateMachine
{
    public abstract class State
    {
        private StateController _stateController;

        protected readonly IDebugger Debugger;

        protected State(IDebugger debugger)
        {
            Debugger = debugger;
        }

        public void Initialize(StateController stateController) => _stateController = stateController;

        public abstract UniTask Switch(CancellationToken cancellationToken = default);

        public virtual UniTask Leave() => UniTask.CompletedTask;

        protected async UniTask SwitchTo<T>(CancellationToken cancellationToken = default) where T : State => await _stateController.EnterState<T>(cancellationToken);
    }
}