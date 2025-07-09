using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;

namespace Runtime.Core.Controllers
{
    public abstract class BaseController
    {
        protected ThisState CurrentState = ThisState.Pending;

        public ThisState CurrentThisState => CurrentState;

        public virtual UniTask Perform(CancellationToken cancellationToken)
        {
            Assert.IsFalse(CurrentState == ThisState.Run, $"{this.GetType().Name}: try run already running controller");

            CurrentState = ThisState.Run;
            return UniTask.CompletedTask;
        }

        public virtual UniTask Return()
        {
            Assert.IsFalse(CurrentState != ThisState.Run, $"{this.GetType().Name}: try to stop not active controller");

            CurrentState = ThisState.Stop;
            return UniTask.CompletedTask;
        }
    }
}