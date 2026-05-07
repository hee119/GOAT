using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoatMove : MonoBehaviour
{
    private Vector2 playerInput;
    private Rigidbody2D playerRb;

    [Header("Move")]
    public float playerSpeed;
    public float playerJumpForce;
    public float playerMaxGravityForce;

    [Header("Ground Check")]
    public float playerJumpDistance;
    private int groundLayer;

    [Header("Fall")]
    public float fallThreshold;
    public GameObject spawnPoint;

    [Header("Crazy Effect")]
    public float goatCrageTime;
    public float goatCragePosition = 1.5f;
    public float goatCrageRotation = 0.1f;

    private bool isRespawning = false;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    void FixedUpdate()
    {
        Move();
        LimitFallSpeed();

        Debug.DrawRay(transform.position, Vector2.down * playerJumpDistance, Color.red);

        if (transform.position.y < fallThreshold && !isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    void Move()
    {
        if (IsGrounded())
        {
            Vector2 moveForce = new Vector2(playerInput.x * playerSpeed, 0);

            playerRb.AddForce(moveForce);
        }
    }

    void LimitFallSpeed()
    {
        playerRb.linearVelocity = new Vector2(
            playerRb.linearVelocity.x,
            Mathf.Max(playerRb.linearVelocity.y, -playerMaxGravityForce)
        );
    }

    public void OnMove(InputValue value)
    {
        playerInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            playerRb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            playerJumpDistance,
            groundLayer
        );

        return hit.collider != null;
    }

    IEnumerator RespawnRoutine()
    {
        
        transform.position = spawnPoint.transform.position;
        playerRb.linearVelocity = playerRb.linearVelocity.normalized;
        
        yield return new WaitUntil(() => IsGrounded());

        isRespawning = true;

        Debug.Log("Going crazy");

        float t = 0;

        while (t < goatCrageTime)
        {
            transform.position = new Vector3(Random.Range(transform.position.x - goatCragePosition, transform.position.x + goatCragePosition), transform.position.y, transform.position.z);

            transform.rotation = Quaternion.Euler(Random.Range(-goatCrageRotation * 100, goatCrageRotation * 100), Random.Range(-goatCrageRotation * 100, goatCrageRotation * 100), Random.Range(-goatCrageRotation * 100, goatCrageRotation * 100));

            t += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        
        playerRb.linearVelocity = Vector2.zero;

        transform.rotation = Quaternion.identity;
        
        isRespawning = false;
    }
}