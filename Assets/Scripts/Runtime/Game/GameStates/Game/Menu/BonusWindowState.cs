using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Achievements;
using Runtime.Game.Dailies;
using Runtime.Game.Services.Audio;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;
using UnityEngine;

namespace Runtime.Game.GameStates.Game.Menu
{
    public class BonusWindowState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly LoginHelper _loginHelper;
        private readonly DailyRewardGenerator _bonusRewardGenerator;
        private readonly DailyRouletteSpinController _bonusRouletteSpinner;
        private readonly ISoundService _soundService;
        private readonly RewardPopupState _rewardPopupState;

        private DailyBonusWindow _window;
        
        private List<DailyReward> _bonusRewards;

        public BonusWindowState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, 
            LoginHelper loginHelper, DailyRewardGenerator bonusRewardGenerator, 
            DailyRouletteSpinController bonusRouletteSpinner,
            ISoundService soundService, RewardPopupState rewardPopupState) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _loginHelper = loginHelper;
            _bonusRewardGenerator = bonusRewardGenerator;
            _bonusRouletteSpinner = bonusRouletteSpinner;
            _soundService = soundService;
            _rewardPopupState = rewardPopupState;
        }

        public override UniTask Switch(CancellationToken cancellationToken)
        {
            MakeWindow();
            Sub();

            Create1();

            Create2();
            
            return UniTask.CompletedTask;
        }

        private void Create2()
        {
            if(!_loginHelper.IsSpinAvailable())
                _window.DisableSpinning();
        }

        private void Create1()
        {
            _bonusRewards = _bonusRewardGenerator.GenerateRewards(_window.Slots.ToList());
            for(int i = 0; i < _bonusRewards.Count; i++)
                _window.Slots[i].Initialize(_bonusRewards[i]);
        }

        public override async UniTask Leave()
        {
            await _userInterfaceHelper.HideWindow(WindowNames.DailyBonusWindow);
        }

        private void MakeWindow()
        {
            _window = _userInterfaceHelper.RetrieveWindow<DailyBonusWindow>(WindowNames.DailyBonusWindow);
            _window.Setup();
            _window.Reveal().Forget();
        }

        private void Sub()
        {
            _window.OnBackPressed += async () => await SwitchTo<MainWindowState>();
            _window.OnSpinPressed += ProcessSpin;
        }

        private async void ProcessSpin()
        {
            _window.DisableInteractibleObjects();
            
            _soundService.PlaySound(ConstAudioNames.RouletteSound);
            int rewardId = Random.Range(0, _bonusRewards.Count);
            DailyReward reward = _bonusRewards[rewardId];
            
            await _bonusRouletteSpinner.Spin(_window.RouletteTransform, rewardId);
            
            _loginHelper.RecordCurrentDate();

            AccomplishmentsEventInvoker.InvokeOnDailyBonus();
            
            _rewardPopupState.SetRewardData(reward);
            _rewardPopupState.Switch().Forget();
        }
    }
}