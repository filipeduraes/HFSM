using HFSM.Player.PlayerCore;
using HFSM.StateMachine;
using UnityEngine;

namespace HFSM.Player.PlayerStates
{
    public class AirState : State<PlayerController>
    {
        public AirState(PlayerController stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Entering Air State");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exiting Air State");
        }
    }
}