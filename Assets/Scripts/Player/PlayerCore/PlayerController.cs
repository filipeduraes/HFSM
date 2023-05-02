using HFSM.Player.PlayerStates;
using UnityEngine;

namespace HFSM.Player.PlayerCore
{
    public class PlayerController : StateMachine.StateMachine<PlayerController>
    {
        public PlayerPhysics Physics => physics;
        public PlayerInput Input => input;

        [SerializeField] private PlayerPhysics physics;
        [SerializeField] private PlayerInput input;

        protected override void SetInitialState()
        {
            SetState<IdleState>();
        }
    }
}
