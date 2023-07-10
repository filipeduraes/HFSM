using System.Collections.Generic;
using UnityEngine;

namespace FSMSystem.Core
{
    [CreateAssetMenu(fileName = "StateMachine")]
    public class StateMachine : ScriptableObject
    {
        public IEnumerable<State> States => states;
        private State RootState => states[0];
        [SerializeField] private List<State> states = new();

        private State currentState;
        private State waitingState;

        public void StartStateMachine()
        {
            SetState(RootState);
        }

        public void UpdateStateMachine()
        {
            if (!CheckTransitions()) 
                currentState.UpdateState();
        }

        public void AddTransition(State fromState, State toState)
        {
            Transition transition = CreateInstance<Transition>();
            transition.SetState(toState);
            fromState.AddTransition(transition);
        }

        public void AddState(State state)
        {
            states.Add(state);
        }

        public void RemoveState(State state)
        {
            states.Remove(state);
        }

        private bool CheckTransitions()
        {
            bool hasTransition = currentState.HasTransition(out State state);
            
            if (hasTransition) 
                FinishAndWaitForNewState(state);
            
            return hasTransition;
        }

        private void FinishAndWaitForNewState(State state)
        {
            waitingState = state;
            currentState.ExitState();
        }

        private void SetWaitingState()
        {
            currentState.OnExecutionFinished -= SetWaitingState;
            currentState.OnExecutionFailed -= InterruptExecution;
            
            State stateToTransition = waitingState;
            SetState(stateToTransition);
        }

        private void SetState(State stateToTransition)
        {
            currentState = stateToTransition;
            currentState.OnExecutionFinished += SetWaitingState;
            currentState.OnExecutionFailed += InterruptExecution;
            
            if (!CheckTransitions())
                currentState.EnterState();
        }

        private void InterruptExecution()
        {
            currentState.OnExecutionFailed -= InterruptExecution;
            currentState.OnExecutionFinished -= SetWaitingState;
            Debug.Log($"Execution failed at FSM: {name}");
        }
    }
}