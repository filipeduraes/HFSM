using System;
using System.Collections;
using UnityEngine;

namespace HFSM.StateMachine
{
    public abstract class State<T> where T : StateMachine
    {
        public virtual Type ParentState => null;
        
        protected T stateMachine;

        public State(T stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public virtual void EnterState() { }
        
        public virtual void FixedUpdate() { }
        
        public virtual void ExitState() { }

        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return stateMachine.StartCoroutine(coroutine);
        }

        protected void StopCoroutine(Coroutine coroutine)
        {
            stateMachine.StopCoroutine(coroutine);
        }
    }
}
