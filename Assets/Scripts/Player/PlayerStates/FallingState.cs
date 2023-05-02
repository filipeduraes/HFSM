using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;

namespace HFSM.Player.PlayerStates
{
    public class FallingState : State<PlayerController>
    {
        public override Type ParentState => typeof(AirState);

        public FallingState(PlayerController stateMachine) : base(stateMachine) { }

        public override void EnterState()
        {
            stateMachine.Physics.Fall();
        }

        public override void FixedUpdate()
        {
            stateMachine.Physics.LimitFallingVelocity();
            
            if(stateMachine.Physics.IsGrounded())
                SetState<IdleState>();
        }
    }
}