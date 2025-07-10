using Fusion;
using ShooterGame.Player;

namespace ShooterGame.UI.Views
{
    public class LobbyView : View
    {
        private GameMode _gameMode;
        private string _playerName;

        public void Configure(LobbyPlayer player)
        {
            // _gameMode = player.GameMode;
            // _playerName = player.PlayerName.ToString();
            // UpdateUI();
        }
    }
}