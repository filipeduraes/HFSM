using System;
using UnityEngine;

namespace FSMSystem.Core
{
    public abstract class ActionTask : ScriptableObject
    {
        public event Action<bool> OnTaskFinished = delegate {  };

        public virtual void Start() { }

        public virtual void Update() { }
        
        public virtual void End() { }

        protected void FinishTask(bool wasSuccessful)
        {
            End();
            OnTaskFinished(wasSuccessful);
        }
    }
}