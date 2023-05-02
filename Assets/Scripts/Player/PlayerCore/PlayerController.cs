using HFSM.Player.PlayerStates;
using UnityEngine;

namespace HFSM.Player.PlayerCore
{
    public class PlayerController : StateMachine.StateMachine<PlayerController>
    {
        public PlayerPhysics Physics => physics;
        public PlayerInput Input => input;

        [SerializeField] private PlayerPhysics physics;
        [SerializeField] private PlayerInput input;

        protected override void SetInitialState()
        {
            SetState<IdleState>();
        }

        [ContextMenu(nameof(SetJump))]
        private void SetJump()
        {
            SetState<JumpState>();
        }
        
        [ContextMenu(nameof(SetFalling))]
        private void SetFalling()
        {
            SetState<FallingState>();
        }
        
        [ContextMenu(nameof(SetWalk))]
        private void SetWalk()
        {
            SetState<WalkState>();
        }
        
        [ContextMenu(nameof(SetIdle))]
        private void SetIdle()
        {
            SetState<IdleState>();
        }
    }
}
