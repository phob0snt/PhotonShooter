using ShooterGame.GlobalStateMachine.States;
using ShooterGame.GlobalStateMachine;
using Zenject;
using ShooterGame.Network;
using ShooterGame.Services;

namespace ShooterGame.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGlobalStates();

            Container.BindInterfacesTo<AddressablesFactory>().AsSingle();
            Container.Bind<IAssetLoader>().To<AddressablesAssetLoader>().AsSingle();
            Container.BindInterfacesTo<StateMachine>().AsSingle();
            Container.BindInterfacesTo<NetworkManager>().AsSingle();
        }

        private void BindGlobalStates()
        {
            Container.Bind<IGlobalState>().To<BootstrapState>().AsSingle();
            Container.Bind<IGlobalState>().To<MainMenuState>().AsSingle();
        }
    }
}