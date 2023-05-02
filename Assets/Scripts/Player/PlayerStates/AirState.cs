using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;

namespace HFSM.Player.PlayerStates
{
    public class AirState : State<PlayerController>
    {
        public override Type ParentState => null;
        
        public AirState(PlayerController stateMachine) : base(stateMachine) { }

        public override void FixedUpdate()
        {
            float walkDirection = stateMachine.Input.AxisInput.x;
            stateMachine.Physics.Walk(walkDirection);
        }
    }
}