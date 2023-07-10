using FSMSystem.Core.BaseTasks;
using UnityEngine;

namespace FSMSystem.Core
{
    public class Transition : ScriptableObject
    {
        public State ToState => toState;

        [SerializeField] private ConditionTask condition;
        [SerializeField] private State toState;

        public bool CheckTransition(State currentState)
        {
            return condition.CheckCondition(currentState);
        }

        public void Initialize(State fromState)
        {
            condition.Initialize(fromState);
        }

        public void Dispose(State fromState)
        {
            condition.Dispose(fromState);
        }

        public void SetState(State state)
        {
            condition = CreateInstance<StateFinishedCondition>();
            toState = state;
        }
    }
}