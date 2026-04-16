using UnityEngine;
using UnityEngine.InputSystem;
public class GoatMove : MonoBehaviour
{
    private Vector2 playerInput;
    private Rigidbody playerRb;
    public float playerSpeed;
    public float playerJumpForce;
    public float playerJumpDistance;
    public float playerMaxGravityForce;
    private Vector3 horizental;
    private Vector3 vertical;
    private int groundLayer;
    private bool canJump;
    private RaycastHit hit;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizental = new Vector3(playerInput.x * playerSpeed, 0,  playerInput.y * playerSpeed);
        playerRb.AddForce(horizental, ForceMode.Acceleration);

        vertical = new Vector3(0, Mathf.Max(playerRb.linearVelocity.y, -playerMaxGravityForce),  0);
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, vertical.y, playerRb.linearVelocity.z);
        Debug.DrawRay(transform.position, Vector3.down * playerJumpDistance, Color.red);
        
    }

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
        Debug.Log(playerInput.ToString());
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            playerRb.AddForce(Vector3.up * playerJumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerJumpDistance, groundLayer))
        {
            return true;
        }
        return false;
    }
}
