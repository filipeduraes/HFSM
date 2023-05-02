﻿using System;
using HFSM.Player.PlayerCore;
using HFSM.StateMachine;
using UnityEngine;

namespace HFSM.Player.PlayerStates
{
    public class JumpState : State<PlayerController>
    {
        public override Type ParentState => typeof(AirState);

        public JumpState(PlayerController stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Entering Jump State");
        }
        
        public override void ExitState()
        {
            Debug.Log("Exiting Jump State");
        }
    }
}