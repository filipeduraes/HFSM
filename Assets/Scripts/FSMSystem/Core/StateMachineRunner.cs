using UnityEngine;

namespace FSMSystem.Core
{
    public class StateMachineRunner : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private UpdateType updateType;
        
        private void Start()
        {
            stateMachine.StartStateMachine();
        }

        private void Update()
        {
            if(updateType == UpdateType.Update)
                stateMachine.UpdateStateMachine();
        }

        private void FixedUpdate()
        {
            if(updateType == UpdateType.FixedUpdate)
                stateMachine.UpdateStateMachine();
        }

        private void LateUpdate()
        {
            if(updateType == UpdateType.LateUpdate)
                stateMachine.UpdateStateMachine();
        }

        private enum UpdateType
        {
            Update,
            FixedUpdate,
            LateUpdate
        }
    }
}