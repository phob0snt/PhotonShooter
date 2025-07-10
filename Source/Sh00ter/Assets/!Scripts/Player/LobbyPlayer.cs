using Fusion;
using TMPro;
using UnityEngine;

namespace ShooterGame.Player
{
    public class LobbyPlayer : NetworkBehaviour
    {
        [Networked] public NetworkString<_16> PlayerName { get; set; }

        [SerializeField] private TMP_Text _playerNameText;

        public override void Spawned()
        {
            UpdateNameUI();
        }

        private void UpdateNameUI()
        {
            Debug.Log("PLAYER NICK " + PlayerName.ToString());
            //_playerNameText.text = PlayerName.ToString();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RpcSetPlayerName(string name)
        {
            PlayerName = name;
        }
    }
}