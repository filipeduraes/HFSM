using System.Collections.Generic;
using UnityEngine;

namespace HFSM.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        private StateFactory<StateMachine> stateFactory;
        private State<StateMachine> currentState;

        private List<State<StateMachine>> currentStatePath = new();

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
            currentState = stateFactory.GetState<T>();
            
            List<State<StateMachine>> newStatePath = stateFactory.GetStatePath(currentState);

            ExitOldStates(newStatePath);
            EnterNewStates(newStatePath);

            currentStatePath = newStatePath;
        }

        private void EnterNewStates(List<State<StateMachine>> newStatePath)
        {
            foreach (State<StateMachine> newStateInPath in newStatePath)
            {
                if (!currentStatePath.Contains(newStateInPath))
                    newStateInPath?.EnterState();
            }
        }

        private static void ExitOldStates(List<State<StateMachine>> newStatePath)
        {
            foreach (State<StateMachine> oldStateInPath in newStatePath)
            {
                if (!newStatePath.Contains(oldStateInPath))
                    oldStateInPath?.ExitState();
            }
        }
    }
}