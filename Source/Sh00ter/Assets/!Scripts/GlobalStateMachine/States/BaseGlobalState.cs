using Zenject;

namespace ShooterGame.GlobalStateMachine.States
{
    public abstract class BaseGlobalState : IGlobalState
    {
        [Inject] protected IGlobalStateMachine _globalStateMachine;
        
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }   
}

