using Runtime.Core.Audio;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.FileStorageService;
using Runtime.Core.Infrastructure.FileSystemService;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Core.Infrastructure.ObjectGetter;
using Runtime.Core.Infrastructure.PersistantDataProvider;
using Runtime.Core.Infrastructure.Serializer;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.ApplicationState;
using Runtime.Game.Services.Audio;
using Runtime.Game.Services.ScreenOrientation;
using Runtime.Game.Services.UI;
using Runtime.Game.Services.UserData;
using UnityEngine;
using Zenject;

namespace Runtime.Game.Services
{
    [CreateAssetMenu(fileName = "ServicesInstaller", menuName = "Installers/ServicesInstaller")]
    public class ServicesInstaller : ScriptableObjectInstaller<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Bind1();
            Bind2();
            Bind3();
        }

        private void Bind3()
        {
            Container.Bind<ApplicationStateHelper>().AsSingle();
            Container.Bind<UserInformationHelper>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenOrientationAlertController>().AsSingle();
        }

        private void Bind2()
        {
            Bind5();
            Bind4();
        }

        private void Bind5()
        {
            Container.Bind<IDataService>().To<PersistentDataService>().AsSingle();
            Container.Bind<IFileCleaner>().To<FileCleaner>().AsSingle();
            Container.Bind<ISerializationProvider>().To<JsonSerializationProvider>().AsSingle();
        }

        private void Bind4()
        {
            Container.Bind<ISoundService>().To<SoundService>().AsSingle();
            Bind7();
        }

        private void Bind7()
        {
            Container.Bind<GameObjectFactory>().AsSingle();
        }

        private void Bind1()
        {
            Container.Bind<IUserInterfaceHelper>().To<UserInterfaceHelper>().AsSingle();
            Container.Bind<IObjectGetterService>().To<ObjectGetterService>().AsSingle();
            Bind6();
        }

        private void Bind6()
        {
            Container.Bind<IPersistentDataProvider>().To<PersistantDataProvider>().AsSingle();
            Container.Bind<IConfiguratioGetter>().To<SettingsProvider.SettingsProvider>().AsSingle();
            Container.Bind<IDebugger>().To<SimpleDebugger>().AsSingle();
        }
    }
}