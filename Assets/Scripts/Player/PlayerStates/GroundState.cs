using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;

namespace HFSM.Player.PlayerStates
{
    public class GroundState : State<PlayerController>
    {
        public override Type ParentState => null;
        
        public GroundState(PlayerController stateMachine) : base(stateMachine) { }

        public override void EnterState()
        {
            stateMachine.Input.OnJumpDown += SetJump;
        }

        public override void ExitState()
        {
            stateMachine.Input.OnJumpDown -= SetJump;
        }

        public override void FixedUpdate()
        {
            if (!stateMachine.Physics.IsGrounded())
                SetState<FallingState>();
        }
        
        private void SetJump()
        {
            SetState<JumpState>();
        }
    }
}