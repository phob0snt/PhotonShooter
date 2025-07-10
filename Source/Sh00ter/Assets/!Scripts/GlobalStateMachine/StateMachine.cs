using System.Collections.Generic;
using System.Linq;
using ShooterGame.GlobalStateMachine.States;
using Zenject;

namespace ShooterGame.GlobalStateMachine
{
    public class StateMachine : IGlobalStateMachine, IInitializable
    {
        [Inject] private readonly IEnumerable<IGlobalState> _states;
        private IGlobalState _currentState;

        public void Initialize()
        {
            ChangeState<BootstrapState>();
        }

        public void ChangeState<T>() where T : IGlobalState
        {
            _currentState?.Exit();
            _currentState = _states.OfType<T>().FirstOrDefault();

            if (_currentState == null)
            {
                throw new System.Exception($"State {typeof(T).Name} not found in the global state machine.");
            }

            _currentState.Enter();
        }

        public void Shutdown()
        {
            _currentState?.Exit();
            _currentState = null;
        }
    }
}