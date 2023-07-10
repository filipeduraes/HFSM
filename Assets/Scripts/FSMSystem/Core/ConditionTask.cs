using UnityEngine;

namespace FSMSystem.Core
{
    public abstract class ConditionTask : ScriptableObject
    {
        [SerializeField] private bool negateCondition;

        public bool CheckCondition(State fromState)
        {
            bool result = Check(fromState);
            return negateCondition ? !result : result;
        }

        public virtual void Initialize(State fromState) { }

        public virtual void Dispose(State fromState) { }
        
        protected abstract bool Check(State state);
    }
}