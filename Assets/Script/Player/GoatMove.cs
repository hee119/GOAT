using System.Collections.Generic;
using System.Collections;
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
    public float fallThreshold;
    public GameObject spawnPoint;
    public float goatCrageTime;
    public float goatCragePosition = 1.5f;
    public float goatCrageRotation = 0.1f;
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
        if (IsGrounded()){
            horizental = new Vector3(playerInput.x * playerSpeed, 0,  playerInput.y * playerSpeed);
            playerRb.AddForce(horizental, ForceMode.Acceleration);
        }
        
        

        vertical = new Vector3(0, Mathf.Max(playerRb.linearVelocity.y, -playerMaxGravityForce),  0);
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, vertical.y, playerRb.linearVelocity.z);
        Debug.DrawRay(transform.position, Vector3.down * playerJumpDistance, Color.red);

        if (transform.position.y < fallThreshold)
        {
            StartCoroutine(GoatGoingCrage());
            transform.position = spawnPoint.transform.position;
            playerRb.linearVelocity = Vector3.zero;
        }
        
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

    IEnumerator GoatGoingCrage()
    {
        Debug.Log("Going crage");
        float t = 0;   
        while (goatCrageTime > t)
        {
            transform.position = new Vector3(Random.Range(transform.position.x - goatCragePosition, transform.position.x + goatCragePosition), transform.position.y, Random.Range(transform.position.z - goatCragePosition, transform.position.z + goatCragePosition));
            transform.rotation = new Quaternion(Random.Range(transform.rotation.x - goatCrageRotation, transform.rotation.x + goatCrageRotation), Random.Range(transform.rotation.y - goatCrageRotation, transform.rotation.y + goatCrageRotation), Random.Range(transform.rotation.z - goatCrageRotation, transform.rotation.z + goatCrageRotation), Random.Range(transform.rotation.w - goatCrageRotation, transform.rotation.w + goatCrageRotation));
            t += Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
