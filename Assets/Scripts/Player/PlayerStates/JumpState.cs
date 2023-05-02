using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;

namespace HFSM.Player.PlayerStates
{
    public class JumpState : State<PlayerController>
    {
        public override Type ParentState => typeof(AirState);

        public JumpState(PlayerController stateMachine) : base(stateMachine) { }

        public override void EnterState()
        {
            stateMachine.Physics.Jump();
        }

        public override void FixedUpdate()
        {
            if (!stateMachine.Input.IsPressingJump || stateMachine.Physics.IsFalling())
                SetState<FallingState>();
        }
    }
}