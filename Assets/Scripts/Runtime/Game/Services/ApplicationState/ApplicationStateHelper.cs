using System;
using UnityEngine;

namespace Runtime.Game.Services.ApplicationState
{
    public class ApplicationStateHelper
    {
        public event Action NotifyApplicationQuitEvent;
        public event Action<bool> NotifyApplicationPauseEvent;

        private ApplicationStateMonoHelper _helper;
 
        public void Setup()
        {
            GameObject applicationStateHelper = new GameObject("ApplicationStateHelper");
            _helper = applicationStateHelper.AddComponent<ApplicationStateMonoHelper>();

            Sub();
        }

        private void Sub()
        {
            _helper.ApplicationQuitEvent += InvokeAppQuit;
            _helper.ApplicationPauseEvent += InvokeAppPause;
        }

        public void Cleanup()
        {
            if(_helper == null)
                return;

            Unsub();
        }

        private void Unsub()
        {
            _helper.ApplicationQuitEvent -= InvokeAppQuit;
            _helper.ApplicationPauseEvent -= InvokeAppPause;
        }

        private void InvokeAppQuit() => NotifyApplicationQuitEvent?.Invoke();

        private void InvokeAppPause(bool isPause) => NotifyApplicationPauseEvent?.Invoke(isPause);
    }
}