using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Controllers;
using Runtime.Game.Services.ApplicationState;
using Runtime.Game.Services.UserData;

namespace Runtime.Game
{
    public class ApplicationStateEventsHeloer : BaseController
    {
        private readonly ApplicationStateHelper _applicationStateHelper;
        private readonly UserInformationHelper _userInformationHelper;

        public ApplicationStateEventsHeloer(ApplicationStateHelper applicationStateHelper,
            UserInformationHelper userInformationHelper)
        {
            _applicationStateHelper = applicationStateHelper;
            _userInformationHelper = userInformationHelper;
        }

        public override UniTask Perform(CancellationToken cancellationToken)
        {
            base.Perform(cancellationToken);

            _applicationStateHelper.Setup();

            Sub();

            return UniTask.CompletedTask;
        }

        private void Sub()
        {
            _applicationStateHelper.NotifyApplicationQuitEvent += QuitNotifyApplicationHandler;
            _applicationStateHelper.NotifyApplicationPauseEvent += PauseNotifyApplicationHandler;
        }

        public override UniTask Return()
        {
            base.Return();

            Unsub();

            _applicationStateHelper.Cleanup();

            return UniTask.CompletedTask;
        }

        private void Unsub()
        {
            _applicationStateHelper.NotifyApplicationQuitEvent -= QuitNotifyApplicationHandler;
            _applicationStateHelper.NotifyApplicationPauseEvent -= PauseNotifyApplicationHandler;
        }

        private void QuitNotifyApplicationHandler() => _userInformationHelper.SaveUserData();

        private void PauseNotifyApplicationHandler(bool isPause)
        {
            if (isPause)
                _userInformationHelper.SaveUserData();
        }
    }
}