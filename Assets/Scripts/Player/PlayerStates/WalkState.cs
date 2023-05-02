using HFSM.Player.PlayerCore;
using HFSM.StateMachine;
using UnityEngine;

namespace HFSM.Player.PlayerStates
{
    public class WalkState : State<PlayerController>
    {
        public WalkState(PlayerController stateMachine) : base(stateMachine)
        {
        }
        
        public override void EnterState()
        {
            Debug.Log("Entering Walk State");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exiting Walk State");
        }
    }
}