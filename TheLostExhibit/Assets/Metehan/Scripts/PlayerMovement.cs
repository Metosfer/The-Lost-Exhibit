using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7000f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float maxSpeed = 8f;

    public float horizontalInput;
    public float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private Transform orientation;
    private PlayerJump playerJump; // Z�plama scriptine referans

    private PlayerAnimation playerAnimation;

    private void Start()
    {
        playerAnimation = FindAnyObjectByType<PlayerAnimation>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        orientation = transform.Find("Orientation");
        playerJump = GetComponent<PlayerJump>();

        if (orientation == null)
            Debug.LogError("Orientation objesi bulunamad�!");
    }

    private void Update()
    {
        ProcessInputs();
        SpeedControl();

        rb.drag = playerJump.IsGrounded ? groundDrag : 0; // Z�plama scriptinden yere temas bilgisini al
    }

    private void FixedUpdate()
    {
        MovePlayer();
        AnimationStop();
    }

    private void ProcessInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        if (orientation == null) return;

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (playerJump.IsGrounded)
        {
            rb.velocity = new Vector3(moveDirection.normalized.x * maxSpeed, rb.velocity.y, moveDirection.normalized.z * maxSpeed);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    private void AnimationStop()
    {
        if (playerAnimation.pickingUpNow == true)
        {
            moveSpeed = 0;
            maxSpeed = 0;
        }
        else
        {
            moveSpeed = 3f;
            maxSpeed = 4f;
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
