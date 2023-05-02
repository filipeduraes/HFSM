using System.Collections.Generic;
using UnityEngine;

namespace HFSM.StateMachine
{
    public abstract class StateMachine<T> : MonoBehaviour where T : StateMachine<T>
    {
        private StateFactory<T> stateFactory;
        private State<T> currentState;

        private List<State<T>> currentStatePath = new();

        private void Awake()
        {
            stateFactory = new StateFactory<T>(this as T);
            SetInitialState();
        }

        private void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        protected abstract void SetInitialState();

        public void SetState<TState>() where TState : State<T>
        {
            currentState = stateFactory.GetState<TState>();
            
            List<State<T>> newStatePath = stateFactory.GetStatePath(currentState);

            ExitOldStates(newStatePath);
            EnterNewStates(newStatePath);

            currentStatePath = newStatePath;
        }

        private void EnterNewStates(List<State<T>> newStatePath)
        {
            foreach (State<T> newStateInPath in newStatePath)
            {
                if (!currentStatePath.Contains(newStateInPath))
                    newStateInPath?.EnterState();
            }
        }

        private static void ExitOldStates(List<State<T>> newStatePath)
        {
            foreach (State<T> oldStateInPath in newStatePath)
            {
                if (!newStatePath.Contains(oldStateInPath))
                    oldStateInPath?.ExitState();
            }
        }
    }
}