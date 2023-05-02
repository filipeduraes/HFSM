using System;
using System.Collections.Generic;

namespace HFSM.StateMachine
{
    public class StateFactory<T> where T : StateMachine
    {
        private readonly Dictionary<Type, State<T>> states = new();

        public StateFactory(T stateMachine)
        {
            Type[] stateTypes = typeof(State<T>).Assembly.GetTypes();

            foreach (Type stateType in stateTypes)
                Activator.CreateInstance(stateType, stateMachine as object);
        }

        public State<T> GetState<TState>() where TState : State<T>
        {
            return states[typeof(TState)];
        }

        private State<T> GetState(Type stateType)
        {
            return states[stateType];
        }

        public List<State<T>> GetStatePath(State<T> state)
        {
            List<State<T>> result = new();
            FindStatePath(state.GetType(), result);
            result.Reverse();
            return result;
        }

        private void FindStatePath(Type stateType, ICollection<State<T>> result)
        {
            State<T> state = GetState(stateType);
            result.Add(state);
            
            if(stateType != null)
                FindStatePath(state.ParentState, result);
        }
    }
}