using Runtime.Core.GameStateMachine;
using Runtime.Game.GameStates.Game;
using UnityEngine;
using Zenject;

namespace Runtime.Game.GameStates.Setup
{
    [CreateAssetMenu(fileName = "BootstrapInstaller", menuName = "Installers/BootstrapInstaller")]
    public class SetupperInstaller : ScriptableObjectInstaller<SetupperInstaller>
    {
        public override void InstallBindings()
        {
            Bind1();

            Bind2();
        }

        private void Bind2()
        {
            Container.Bind<AudioSetupper>().AsSingle();
            Container.Bind<ApplicationStateEventsHeloer>().AsSingle();
        }

        private void Bind1()
        {
            Container.BindInterfacesAndSelfTo<Setupper>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SetupperState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
            Container.Bind<StateController>().AsTransient();
        }
    }
}