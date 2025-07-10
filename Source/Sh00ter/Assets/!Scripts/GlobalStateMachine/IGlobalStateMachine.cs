using ShooterGame.GlobalStateMachine.States;

namespace ShooterGame.GlobalStateMachine
{
    public interface IGlobalStateMachine
    {
        void Initialize();
        void ChangeState<T>() where T : IGlobalState;
        void Shutdown();
    }
}