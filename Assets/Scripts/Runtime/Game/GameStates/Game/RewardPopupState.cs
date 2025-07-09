using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Dailies;
using Runtime.Game.GameStates.Game.Menu;
using Runtime.Game.Services.UI;
using Runtime.Game.ShopSystem;
using Runtime.Game.UI.Popup;

namespace Runtime.Game.GameStates.Game
{
    public class RewardPopupState : State
    {
        private readonly IUserInterfaceHelper _userInterfaceHelper;
        private readonly InventoryHelper _inventoryHelper;
    
        private DailyReward _reward;
    
        public RewardPopupState(IDebugger debugger, IUserInterfaceHelper userInterfaceHelper, InventoryHelper inventoryHelper) : base(debugger)
        {
            _userInterfaceHelper = userInterfaceHelper;
            _inventoryHelper = inventoryHelper;
        }

        public override async UniTask Switch(CancellationToken cancellationToken = default)
        {
            RewardPopup popup = await _userInterfaceHelper.ShowPopup(ProjectPopupNames.RewardPopupName) as RewardPopup;
        
            popup.SetData(_reward);

            popup.OnClaimPressed += async () =>
            {
                popup.DestroyPopup();
                await SwitchTo<MainWindowState>();
            };
        }
    
        public void SetRewardData(DailyReward reward)
        {
            _reward = reward;

            if (reward.RewardType == RewardType.Coin)
            {
                _inventoryHelper.AddCoins(reward.RewardValue);
            }
            else if (reward.RewardType == RewardType.Skin)
            {
                _inventoryHelper.AddSkin(reward.RewardValue);
                _inventoryHelper.SetUsedSkinId(reward.RewardValue);
            }
        }
    }
}
