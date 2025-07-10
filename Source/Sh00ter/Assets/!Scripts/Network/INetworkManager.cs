using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Fusion;
using UnityEngine;

namespace ShooterGame.Network
{
    public interface INetworkManager
    {
        UniTask StartGame(GameMode gameMode, string lobbyName);
        Task<T> Spawn<T>(string address, Vector3 pos, Quaternion rot, PlayerRef authority) where T : NetworkBehaviour;
        PlayerRef GetLocalPlayer();
        bool IsRunning { get; }
    }
}