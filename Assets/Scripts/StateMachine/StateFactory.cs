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
    }
}