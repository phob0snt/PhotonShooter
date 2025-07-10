using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterGame.UI.Elements
{
    public class PlayerConfigurationWindow : UIElement
    {
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Button _confirmButton;

        private UniTaskCompletionSource<string> _nicknameTcs;

        public async UniTask<string> WaitForInputAsync()
        {
            _nicknameTcs = new();
            return await _nicknameTcs.Task;
        }

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
        }

        private void OnConfirmButtonClicked()
        {
            string playerName = _input.text;

            if (string.IsNullOrEmpty(playerName))
            {
                Debug.LogWarning("Player name cannot be empty.");
                return;
            }

            _nicknameTcs.TrySetResult(playerName);
            Hide();
        }
    }
}