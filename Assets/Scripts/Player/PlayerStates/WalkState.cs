using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;

namespace HFSM.Player.PlayerStates
{
    public class WalkState : State<PlayerController>
    {
        public override Type ParentState => typeof(GroundState);

        public WalkState(PlayerController stateMachine) : base(stateMachine) { }

        public override void FixedUpdate()
        {
            float horizontalInput = stateMachine.Input.AxisInput.x;

            if (horizontalInput == 0f)
                SetState<IdleState>();
            else
                stateMachine.Physics.Walk(horizontalInput);
        }
    }
}