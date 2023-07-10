using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSMSystem.Core
{
    public class State : ScriptableObject
    {
        public event Action OnExecutionFailed = delegate { };
        public event Action OnExecutionFinished = delegate {  };
        public event Action OnUpdateLoop = delegate {  };
        
        public string GUID { get => guid; set => guid = value; }
        public string Label { get => label; set => label = value; }

        public Vector2 Position { get => position; set => position = value; }

        [Header("Info")] 
        [SerializeField] private string label;
        [SerializeField] private string guid;
        [SerializeField] private Vector2 position;
        
        [Header("Actions")]
        //Executes once when entering state
        [SerializeField] private List<ActionTask> enterActions;
        //Executes and restarts after finished
        [SerializeField] private List<ActionTask> updateActions;
        //Executes once right after the transition condition was met and before the actual transition
        [SerializeField] private List<ActionTask> exitActions;
        
        [Header("Transitions")]
        [SerializeField] private List<Transition> transitions;

        private TaskRunner taskRunner = new();
        private bool isUpdatingTasks = false;

        private void OnValidate()
        {
            if (name != label)
            {
                name = label;
                
                #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
                #endif
            }
        }

        public bool HasTransition(out State stateToTransition)
        {
            foreach (Transition transition in transitions)
            {
                if (transition.CheckTransition(this))
                {
                    stateToTransition = transition.ToState;
                    return true;
                }
            }

            stateToTransition = null;
            return false;
        }
        
        public void EnterState()
        {
            foreach (Transition transition in transitions)
                transition.Initialize(this);
            
            taskRunner.OnTasksFinished += StartUpdateActions;
            taskRunner.StartTasks(enterActions);
        }

        public void UpdateState()
        {
            taskRunner.UpdateTask();
        }

        public void ExitState()
        {
            foreach (Transition transition in transitions)
                transition.Dispose(this);
            
            taskRunner.ClearCallback();
            taskRunner.StartTasks(exitActions);
            taskRunner.OnTasksFinished += FinishStateExecution;
        }

        private void StartUpdateActions(bool wasSuccessful)
        {
            taskRunner.ClearCallback();
            taskRunner.OnTasksFinished += RepeatUpdateActions;
            taskRunner.OnTasksFinished += SendUpdateLoopCallback;
            
            RepeatUpdateActions(wasSuccessful);
        }

        private void RepeatUpdateActions(bool wasSuccessful)
        {
            if (!wasSuccessful)
            {
                OnExecutionFailed();
                return;
            }
            
            taskRunner.StartTasks(updateActions);
        }

        private void SendUpdateLoopCallback(bool wasSuccessful)
        {
            OnUpdateLoop();
        }
        
        private void FinishStateExecution(bool wasSuccessful)
        {
            taskRunner.ClearCallback();
            
            if (!wasSuccessful)
            {
                OnExecutionFailed();
                return;
            }

            OnExecutionFinished();
        }

        public void AddTransition(Transition transition)
        {
            transitions.Add(transition);
        }

        public void RemoveTransition(State toState)
        {
            Transition transitionToRemove = null;
            
            foreach (Transition transition in transitions)
            {
                if (transition.ToState == toState)
                {
                    transitionToRemove = transition;
                    break;
                }
            }

            if (transitionToRemove != null)
                transitions.Remove(transitionToRemove);
        }
    }
}