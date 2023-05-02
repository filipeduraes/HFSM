using HFSM.Player.PlayerStates;
using UnityEngine;

namespace HFSM.Player.PlayerCore
{
    public class PlayerController : StateMachine.StateMachine<PlayerController>
    {
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
