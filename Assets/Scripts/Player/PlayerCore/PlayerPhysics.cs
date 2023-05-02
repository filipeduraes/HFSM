using System;
using UnityEngine;

namespace HFSM.Player.PlayerCore
{
    public class PlayerPhysics : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D body;
        
        [Header("Air Parameters")]
        [SerializeField] private float maxFallingVelocity = 6f;
        [SerializeField] private float jumpVelocity = 6f;
        [SerializeField] private float jumpGravity = 1f;
        [SerializeField] private float fallingGravity = 3f;

        [Header("Ground Parameters")] 
        [SerializeField] private float walkSpeed = 2f;
        
        [Header("Ground Check")]
        [SerializeField] private float groundCheckDistance = 1f;
        [SerializeField] private float groundCheckOffset = 0.1f;
        [SerializeField] private LayerMask groundLayer;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsGrounded() ? Color.yellow : Color.magenta;
            
            Vector2 middleOrigin = body.position;
            Vector2 leftOrigin = middleOrigin + Vector2.left * groundCheckOffset;
            Vector2 rightOrigin = middleOrigin + Vector2.right * groundCheckOffset;
            
            Gizmos.DrawLine(leftOrigin, leftOrigin + Vector2.down * groundCheckDistance);
            Gizmos.DrawLine(middleOrigin, middleOrigin + Vector2.down * groundCheckDistance);
            Gizmos.DrawLine(rightOrigin, rightOrigin + Vector2.down * groundCheckDistance);
        }

        public void Jump()
        {
            SetVelocityY(jumpVelocity);
            body.gravityScale = jumpGravity;
        }

        public void Fall()
        {
            body.gravityScale = fallingGravity;
        }

        public void Stop()
        {
            SetVelocityX(0f);
        }

        public void Walk(float horizontalInput)
        {
            SetVelocityX(horizontalInput * walkSpeed);
        }

        public void LimitFallingVelocity()
        {
            Vector2 bodyVelocity = body.velocity;
            
            if (Mathf.Abs(bodyVelocity.y) > maxFallingVelocity)
            {
                bodyVelocity.y = -maxFallingVelocity;
                body.velocity = bodyVelocity;
            }
        }

        public bool IsGrounded()
        {
            Vector2 middleOrigin = body.position;
            Vector2 leftOrigin = middleOrigin + Vector2.left * groundCheckOffset;
            Vector2 rightOrigin = middleOrigin + Vector2.right * groundCheckOffset;
            
            RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, groundCheckDistance, groundLayer);
            RaycastHit2D middleHit = Physics2D.Raycast(middleOrigin, Vector2.down, groundCheckDistance, groundLayer);
            RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, groundCheckDistance, groundLayer);
            
            return leftHit || middleHit || rightHit;
        }

        public bool IsFalling()
        {
            return body.velocity.y < 0f;
        }
        
        private void SetVelocityX(float velocityX)
        {
            Vector2 bodyVelocity = body.velocity;
            bodyVelocity.x = velocityX;
            body.velocity = bodyVelocity;
        }
        
        private void SetVelocityY(float velocityY)
        {
            Vector2 bodyVelocity = body.velocity;
            bodyVelocity.y = velocityY;
            body.velocity = bodyVelocity;
        }
    }
}