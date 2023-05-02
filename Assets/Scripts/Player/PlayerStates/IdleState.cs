using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;
using UnityEngine;

namespace HFSM.Player.PlayerStates
{
    public class IdleState : State<PlayerController>
    {
        public override Type ParentState => typeof(GroundState);

        public IdleState(PlayerController stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Entering Idle State");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exiting Idle State");
        }
    }
}