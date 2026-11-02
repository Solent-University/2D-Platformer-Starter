using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float jumpSpeed = 1;
    public int numberOfJumps = 2;
    public float maxJumpTime = 0.1f;
    public LayerMask groundLayers;

    private Rigidbody2D rb;
    private CapsuleCollider2D collider;

    private Vector2 moveInput;
    private bool jumpInput;
    private bool jumpPressed;
    private bool jumpReleased;
    private bool isGrounded;
    private bool canJump;
    private int jumpsLeft;
    private float xVelocity;
    private float timeInJump;

    #region Unity Messages
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();

        // Reset jumping if on ground
        if (isGrounded)
        {
            timeInJump = 0;
            jumpsLeft = numberOfJumps;
        }

        // Horizontal movement
        rb.linearVelocityX = moveInput.x * moveSpeed;

        if (jumpInput || jumpReleased)
        {
            HandleJumpFixed();
        }

        // Reset jump pressed & released at the end of frame so it only affects 1 frame when pressed
        jumpPressed = false;
        jumpReleased = false;
    }
    #endregion


    #region Input Messages
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        jumpInput = value.Get<float>() > 0;

        // Jump pressed and released, needs resetting at end of frame
        if (jumpInput)
            jumpPressed = true;
        else
            jumpReleased = true;
    }
    #endregion

    private void Jump()
    {
        rb.linearVelocityY = jumpSpeed;
        timeInJump += Time.fixedDeltaTime;
    }

    private void HandleJumpFixed()
    {
        // If jump is released, no more extended jump allowance
        if (jumpReleased)
        {
            timeInJump = maxJumpTime;
        }

        // If first frame of jump, deduct jump and reset extended jump allowance
        if (jumpPressed)
        {
            if (jumpsLeft > 0)
            {
                Jump();
                jumpsLeft--;
                timeInJump = 0;
            }
        }
        // If holding jump, apply extended jump
        else
        {
            if (timeInJump < maxJumpTime)
            {
                Jump();
            }
        }
    }
    private void CheckGrounded()
    {
        Vector2 rayStart = collider.bounds.center;
        float rayRadius = (collider.size.x/2 * transform.localScale.x) * 0.95f;
        float rayLength = 0.55f * (collider.size.y / 2) * transform.localScale.y;
        RaycastHit2D hit = Physics2D.CircleCast(rayStart, rayRadius, Vector2.down,
            rayLength, groundLayers);
        isGrounded = hit.collider != null;
    }
}
