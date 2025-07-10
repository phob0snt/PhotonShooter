using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Fusion;
using ShooterGame.Player;
using ShooterGame.UI.Views;
using ShooterGame.Utils;
using UnityEngine;
using Zenject;

namespace ShooterGame.Network
{
    public class LobbyHandler : MonoBehaviour, ILobbyHandler
    {
        [Inject] private readonly INetworkManager _networkManager;
        [Inject] private readonly ViewManager _viewManager;
        [SerializeField] private Transform _playerSpawnPoint;
        private List<LobbyPlayer> _players = new();

        public async void CreateOrJoinLobby(GameMode gameMode, string playerName)
        {
            await _networkManager.StartGame(gameMode, "MyRoom");

            while (!_networkManager.IsRunning)
            {
                await UniTask.Yield();
            }

            var localPlayer = _networkManager.GetLocalPlayer();
            if (localPlayer == PlayerRef.None)
            {
                Debug.LogError("Failed to get local player reference!");
                return;
            }
            
            var lobbyPlayer = await _networkManager.Spawn<LobbyPlayer>(
                AddressableAssetsPaths.LOBBY_PLAYER,
                transform.position + (Vector3.right * _players.Count),
                transform.rotation,
                localPlayer);


            if (lobbyPlayer != null)
            {
                _players.Add(lobbyPlayer);
                lobbyPlayer.RpcSetPlayerName(playerName);
                Debug.Log($"Spawned lobby player for {playerName}");
            }

            _viewManager.Show<LobbyView>();
        }
    }
}