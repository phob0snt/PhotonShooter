using ShooterGame.Network;
using ShooterGame.UI.Views;
using UnityEngine;
using Zenject;

namespace ShooterGame.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private ViewManager _viewManager;
        [SerializeField] private LobbyHandler _lobbyHandler;

        public override void InstallBindings()
        {
            Container.Bind<ViewManager>().FromInstance(_viewManager).AsSingle();
            Container.BindInterfacesTo<LobbyHandler>().FromInstance(_lobbyHandler).AsSingle();
        }
    }
}