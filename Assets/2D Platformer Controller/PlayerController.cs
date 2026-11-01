using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float jumpSpeed = 1;
    public float maxJumpTime = 0.1f;
    public LayerMask groundLayers;

    private Rigidbody2D rb;
    private CapsuleCollider2D collider;

    private Vector2 moveInput;
    private bool jumpInput;
    private bool isGrounded;
    private bool isJumping;
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
        if (isGrounded)
            timeInJump = 0;

        rb.linearVelocityX = moveInput.x * moveSpeed;

        if (jumpInput)
        {
            HandleJumpFixed();
        }
        else
        {
            isJumping = false;
        }
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
    }
    #endregion

    private void HandleJumpFixed()
    {
        if (isGrounded)
        {
            isJumping = true;
        }

        if (isJumping && timeInJump < maxJumpTime)
        {
            timeInJump += Time.fixedDeltaTime;
            rb.linearVelocityY = jumpSpeed;
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
