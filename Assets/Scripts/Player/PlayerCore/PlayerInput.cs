using System;
using UnityEngine;

namespace HFSM.Player.PlayerCore
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 AxisInput { get; private set; }
        public bool IsPressingJump { get; private set; }

        public event Action OnHorizontalInputDown = delegate {  };
        public event Action OnJumpDown = delegate {  };

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            IsPressingJump = Input.GetButton("Jump");

            AxisInput = new Vector2(horizontalInput, verticalInput);

            if (Input.GetButtonDown("Horizontal"))
                OnHorizontalInputDown();

            if (Input.GetButtonDown("Jump"))
                OnJumpDown();
        }
    }
}