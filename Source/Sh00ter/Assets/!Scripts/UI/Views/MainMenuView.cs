using Cysharp.Threading.Tasks;
using Fusion;
using ShooterGame.Network;
using ShooterGame.UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShooterGame.UI.Views
{
    public class MainMenuView : View
    {
        [Inject] private readonly ILobbyHandler _lobbyHandler;
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _joinRoomButton;

        private void OnEnable()
        {
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinRoom);
        }

        private void OnDisable()
        {
            _createRoomButton.onClick.RemoveAllListeners();
            _joinRoomButton.onClick.RemoveAllListeners();
        }

        private async void CreateRoom()
        {
            string nick = await WaitForNicknameInput();
            _lobbyHandler.CreateOrJoinLobby(GameMode.Host, nick);
        }

        private async UniTask<string> WaitForNicknameInput()
        {
            PlayerConfigurationWindow conf = TryGetUIElement<PlayerConfigurationWindow>();
            conf.Show();
            return await conf.WaitForInputAsync();
        }

        private async void JoinRoom()
        {
            string nick = await WaitForNicknameInput();
            _lobbyHandler.CreateOrJoinLobby(GameMode.Client, nick);
        }
    }
}