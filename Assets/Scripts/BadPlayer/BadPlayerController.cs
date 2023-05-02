using UnityEngine;

namespace HFSM.BadPlayer
{
    /// <summary>
    /// BAD EXAMPLE for demonstration purposes - Do not follow
    /// </summary>
    public class BadPlayerController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Rigidbody2D body;
        
        [Header("Ground")]
        [SerializeField] private float groundCheckDistance = 0.8f;
        [SerializeField] private float moveSpeed = 9f;
        [SerializeField] private LayerMask groundLayer;
        
        [Header("Air")]
        [SerializeField] private float jumpVelocity = 10f;
        [SerializeField] private float fallingGravity = 10f;
        [SerializeField] private float maxFallVelocity = 22f;
        [SerializeField] private float jumpGravity = 4f;
        
        private Vector2 bodyVelocity;
        private float horizontalInput = 0f;
        private bool isPressingJump = false;
        private bool jumpDownPressed = false;
        private bool isJumping = false;
        private bool isFalling = false;
        
        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");

            bool wasPressingJump = isPressingJump;
            isPressingJump = Input.GetButton("Jump");

            if (!wasPressingJump && isPressingJump)
                jumpDownPressed = true;
        }

        private void FixedUpdate()
        {
            bodyVelocity = body.velocity;
            bool isGrounded = Physics2D.Raycast(body.position, Vector2.down, groundCheckDistance, groundLayer);

            MovePlayer(horizontalInput);

            if (isJumping)
            {
                UpdateJumpState();
            }
            else if (isFalling)
            {
                UpdateFallingState(isGrounded);
            }
            else if (isGrounded)
            {
                UpdateGroundState();
            }
            else
            {
                isFalling = true;
            }

            SetBodyVelocity();
        }

        private void UpdateGroundState()
        {
            if (jumpDownPressed)
                Jump();
        }

        private void UpdateFallingState(bool isGrounded)
        {
            ConstraintFallingVelocity();

            if (isGrounded)
                isFalling = false;
        }

        private void UpdateJumpState()
        {
            isFalling = body.velocity.y < 0f;

            if (!isPressingJump || isFalling)
            {
                body.gravityScale = fallingGravity;
                isFalling = true;
                isJumping = false;
            }
        }

        private void SetBodyVelocity()
        {
            body.velocity = bodyVelocity;
        }

        private void MovePlayer(float direction)
        {
            bodyVelocity.x = direction * moveSpeed;
        }

        private void ConstraintFallingVelocity()
        {
            if (Mathf.Abs(bodyVelocity.y) > maxFallVelocity)
            {
                bodyVelocity.y = -maxFallVelocity;
            }
        }

        private void Jump()
        {
            bodyVelocity.y = jumpVelocity;
            body.gravityScale = jumpGravity;
            
            isJumping = true;
            jumpDownPressed = false;
        }
    }
}
