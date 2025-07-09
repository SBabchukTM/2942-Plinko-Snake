using Runtime.Game.Achievements;
using Runtime.Game.Dailies;
using Runtime.Game.Gameplay;
using Runtime.Game.Gameplay.Effects;
using Runtime.Game.Gameplay.Misc;
using Runtime.Game.Gameplay.Snake;
using Runtime.Game.Gameplay.Spawning;
using Runtime.Game.Gameplay.Spawning.Pools;
using Runtime.Game.Gameplay.Systems;
using Runtime.Game.GameStates.Game.Menu;
using Runtime.Game.Leaderboard;
using Runtime.Game.ShopSystem;
using Runtime.Game.UserAccountSystem;
using UnityEngine;
using Zenject;

namespace Runtime.Game.GameStates.Game
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        [SerializeField] private GameBgScroller _bgScroller;
        
        public override void InstallBindings()
        {
            BindStateControllers();
            BindDailyRewards();
            BindShop();
            BindLeaderboard();
            BindProfile();
            BindGameplay();

            Container.Bind<AccomplishmentsHelper>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AchievementsFactory>().AsSingle();
            Container.Bind<ExplosionEffect>().AsSingle();
        }

        private void BindStateControllers()
        {
            Bind1();
            Bind2();
            Bind3();
        }

        private void Bind3()
        {
            Container.Bind<ToUWindow>().AsSingle();
            Container.Bind<PausePopupState>().AsSingle();
            Container.Bind<GameOverPopupState>().AsSingle();
        }

        private void Bind2()
        {
            Container.Bind<MenuWindow>().AsSingle();
            Container.Bind<PrivacyPolicyState>().AsSingle();
            Bind4();
        }

        private void Bind4()
        {
            Container.Bind<ProfileState>().AsSingle();
            Container.Bind<SettingsScreenState>().AsSingle();
            Container.Bind<ShopScreenState>().AsSingle();
        }

        private void Bind1()
        {
            Container.Bind<AccomplishmentWindowState>().AsSingle();
            Container.Bind<BonusWindowState>().AsSingle();
            Bind5();
        }

        private void Bind5()
        {
            Container.Bind<GameplayWindowState>().AsSingle();
            Container.Bind<ScoresWindowState>().AsSingle();
            Container.Bind<MainWindowState>().AsSingle();
        }

        private void BindDailyRewards()
        {
            Container.Bind<LoginHelper>().AsSingle();
            Container.Bind<DailyRewardGenerator>().AsSingle();
            Bind6();
        }

        private void Bind6()
        {
            Container.Bind<DailyRouletteSpinController>().AsSingle();
            Container.Bind<RewardPopupState>().AsSingle();
            Container.Bind<InventoryHelper>().AsSingle();
        }

        private void BindShop()
        {
            Container.Bind<ShopService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShopItemsFactory>().AsSingle();
        }

        private void BindLeaderboard()
        {
            Container.Bind<ScoresService>().AsSingle();
            Container.BindInterfacesAndSelfTo<RecordsFactory>().AsSingle();
        }

        private void BindProfile()
        {
            Container.Bind<ProfileService>().AsSingle();
            Container.Bind<GalleryPickerService>().AsSingle();
            Container.Bind<SpriteConverter>().AsSingle();
        }

        private void BindGameplay()
        {
            Bind8();
            Bind7();

            Container.Bind<GameBgScroller>().FromComponentInNewPrefab(_bgScroller).AsSingle();
            
            BindPools();
        }

        private void Bind8()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputProvider>().AsSingle();
            Container.Bind<BlocksCreator>().AsSingle();
            Container.Bind<GameData>().AsSingle();
            Container.Bind<NodeManager>().AsSingle();
            Container.Bind<SpritesProvider>().AsSingle();
        }

        private void Bind7()
        {
            Container.Bind<GhostEffect>().AsSingle();
            Container.Bind<ManagerOfSystems>().AsSingle();
            Container.Bind<GameSetupController>().AsSingle();
            Container.BindInterfacesAndSelfTo<MagnetEffect>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayZoomiesController>().AsSingle();
            Container.BindInterfacesAndSelfTo<Spawner>().AsSingle();
        }

        private void BindPools()
        {
            Bind10();
            Bind9();
        }

        private void Bind10()
        {
            Container.BindInterfacesAndSelfTo<NodesPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectibleNodesPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<MagnetsPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoinPool>().AsSingle();
        }

        private void Bind9()
        {
            Container.BindInterfacesAndSelfTo<GhostPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<BombPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<RocketPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<BlockRowPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<ObstaclePool>().AsSingle();
        }
    }
}