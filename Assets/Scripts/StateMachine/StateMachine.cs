using UnityEngine;

namespace HFSM.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private StateFactory<StateMachine> stateFactory;
        private State<StateMachine> currentState;

        private void Awake()
        {
            stateFactory = new StateFactory<StateMachine>(this);
            SetInitialState();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        protected abstract void SetInitialState();

        public void SetState<T>() where T : State<StateMachine>
        {
            currentState?.ExitState();
            currentState = stateFactory.GetState<T>();
            currentState?.EnterState();
        }
    }
}