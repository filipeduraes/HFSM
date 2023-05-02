using HFSM.Player.PlayerCore;
using HFSM.StateMachine;
using UnityEngine;

namespace HFSM.Player.PlayerStates
{
    public class GroundState : State<PlayerController>
    {
        public GroundState(PlayerController stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Entering Ground State");
        }

        public override void ExitState()
        {
            Debug.Log("Exiting Ground State");
        }
    }
}