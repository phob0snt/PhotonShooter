using Fusion;

namespace ShooterGame.Network
{
    public interface ILobbyHandler
    {
        void CreateOrJoinLobby(GameMode gameMode, string playerName);
    }
}