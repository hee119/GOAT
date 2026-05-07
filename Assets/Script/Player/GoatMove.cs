using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoatMove : MonoBehaviour
{
    [Header("Move")] public float playerSpeed;
    public float playerJumpForce;
    public float playerMaxGravityForce;

    [Header("Ground Check")] public float playerJumpDistance;
    private int groundLayer;

    [Header("Fall")] public float fallThreshold;
    public GameObject spawnPoint;

    [Header("Crazy Effect")] public float goatCrageTime;
    public float goatCragePosition = 1.5f;
    public float goatCrageRotation = 0.1f;

    [Header("Goat Animation")] public Animator animator;

    private bool isRespawning = false;

    private Vector2 playerInput;
    private Rigidbody2D playerRb;

    private Vector2 moveForce;

    private Vector2 lastLookDirection;

    private bool isTurn;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        lastLookDirection = new Vector2(1, 0);
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
        if (IsGrounded() && !isTurn)
        {
            moveForce = new Vector2(playerInput.x * playerSpeed, 0);
            animator.SetBool("Jump", false);
            if (Mathf.Abs(playerRb.linearVelocity.x) > 0.1f)
            {
                animator.SetFloat("Forward", Mathf.Abs(3));
            }
            else
            {
                animator.SetFloat("Forward", 0);
            }

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

        if (playerInput.x > 0 && lastLookDirection.x < 0 && !isTurn)
        {
            StartCoroutine(IsTurn(1));
        }
        else if (playerInput.x < 0 && lastLookDirection.x > 0 && !isTurn)
        {
            StartCoroutine(IsTurn(-1));
        }

        lastLookDirection = playerInput.normalized;
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded())
        {
            playerRb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
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

        bool grounded = hit.collider != null;

        animator.SetBool("IsGrounded", grounded);

        return grounded;
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
            transform.position =
                new Vector3(
                    Random.Range(transform.position.x - goatCragePosition, transform.position.x + goatCragePosition),
                    transform.position.y, transform.position.z);

            transform.rotation = Quaternion.Euler(Random.Range(-goatCrageRotation * 100, goatCrageRotation * 100),
                Random.Range(-goatCrageRotation * 100, goatCrageRotation * 100),
                Random.Range(-goatCrageRotation * 100, goatCrageRotation * 100));

            t += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        playerRb.linearVelocity = Vector2.zero;

        transform.rotation = Quaternion.identity;

        isRespawning = false;
    }

    IEnumerator IsTurn(int dir)
    {
        isTurn = true;

        animator.SetFloat("Turn", dir);
        while (isTurn)
        {
            float y = transform.rotation.eulerAngles.y;

            if (dir == 1 && y >= 90)
            {
                animator.SetFloat("Turn", 0);
                isTurn = false;
            }
            else if (dir == -1 && y >= 270)
            {
                animator.SetFloat("Turn", 0);
                isTurn = false;
            }
            yield return null;
        }
    }
}