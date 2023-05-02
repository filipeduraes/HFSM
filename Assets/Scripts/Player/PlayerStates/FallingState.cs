using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;
using UnityEngine;

namespace HFSM.Player.PlayerStates
{
    public class FallingState : State<PlayerController>
    {
        public override Type ParentState => typeof(AirState);

        public FallingState(PlayerController stateMachine) : base(stateMachine)
        {
        }
        
        public override void EnterState()
        {
            Debug.Log("Entering Falling State");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exiting Falling State");
        }
    }
}