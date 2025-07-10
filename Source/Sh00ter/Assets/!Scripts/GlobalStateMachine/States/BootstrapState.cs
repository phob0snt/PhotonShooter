using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ShooterGame.GlobalStateMachine.States
{
    public class BootstrapState : BaseGlobalState
    {
        public override void Enter()
        {
            InitializeAsync().Forget();
        }

        private async UniTask InitializeAsync()
        {
            await SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive).ToUniTask();
            await SceneManager.UnloadSceneAsync("Boot").ToUniTask();
            _globalStateMachine.ChangeState<MainMenuState>();
        }
    }
}