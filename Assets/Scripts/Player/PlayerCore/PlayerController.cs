using HFSM.Player.PlayerStates;

namespace HFSM.Player.PlayerCore
{
    public class PlayerController : StateMachine.StateMachine<PlayerController>
    {
        protected override void SetInitialState()
        {
            SetState<IdleState>();
        }
    }
}
