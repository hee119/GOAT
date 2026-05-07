using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoatMove : MonoBehaviour
{
    [Header("Move")] public float playerSpeed;
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

    [Header("Goat Animation")] 
    public Animator animator;
    
    [Header("Goat Transform")] 
    public Transform goatTransform;

    private bool isRespawning = false;
    
    private bool isCrazy = false;

    private Vector2 playerInput;
    private Rigidbody2D playerRb;

    private Vector2 moveForce;

    private Vector2 lastLookDirection;

    public bool isTurn;
    
    

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
        if (IsGrounded() && !isTurn && !isCrazy)
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

    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && IsGrounded() && !isTurn && !isCrazy)
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
        
        isCrazy = true;
        
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

        isCrazy = false;
        
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
            float y = goatTransform.localEulerAngles.y;

            // 0~360 보정
            if (y > 180f)
                y -= 360f;

            // 오른쪽 보기
            if (dir == 1)
            {
                if (y >= 89f)
                {
                    animator.SetFloat("Turn", 0);
                    lastLookDirection = Vector2.right;
                    isTurn = false;
                }
            }
            // 왼쪽 보기
            else if (dir == -1)
            {
                if (y <= -90)
                {
                    animator.SetFloat("Turn", 0);
                    lastLookDirection = Vector2.left;
                    isTurn = false;
                }
            }

            yield return null;
        }

        goatTransform.transform.position = new Vector3(goatTransform.transform.position.x, goatTransform.transform.position.y, 0);
    }
}