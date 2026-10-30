using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector2 inMove;

    #region Unity Messages
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(inMove.x, rb.linearVelocityY);
    }
    #endregion


    #region Input Messages
    private void OnMove(InputValue value)
    {
        inMove = value.Get<Vector2>();
    } 
    #endregion

}
