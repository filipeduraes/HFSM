using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;

namespace HFSM.Player.PlayerStates
{
    public class IdleState : State<PlayerController>
    {
        public override Type ParentState => typeof(GroundState);

        public IdleState(PlayerController stateMachine) : base(stateMachine) { }

        public override void EnterState()
        {
            if (stateMachine.Input.AxisInput.x == 0f)
                stateMachine.Physics.Stop();
        }

        public override void FixedUpdate()
        {
            if (stateMachine.Input.AxisInput.x != 0f)
                SetState<WalkState>();
        }
    }
}