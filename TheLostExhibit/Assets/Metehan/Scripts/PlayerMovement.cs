using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airMultiplier = 0.4f;
    public float moveSpeed = 2f;
    public float maxSpeed = 2.5f;

    private float originalMoveSpeed; // Eski h�z de�erlerini saklamak i�in
    private float originalMaxSpeed;

    //------------------------------------------
    public float horizontalInput;
    public float verticalInput;
    private Vector3 moveDirection;
    public Rigidbody rb;
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

        // Ba�lang�� h�zlar�n� sakla
        originalMoveSpeed = moveSpeed;
        originalMaxSpeed = maxSpeed;
    }

    private void Update()
    {
        ProcessInputs();
        SpeedControl();

        rb.drag = playerJump.IsGrounded ? groundDrag : 0; // Z�plama scriptinden yere temas bilgisini al
    }

    private void FixedUpdate()
    {
        AnimationStop();
        MovePlayer();
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
        if (playerAnimation.pickingUpNow)
        {
            // H�z� s�f�rla
            moveSpeed = 0;
            maxSpeed = 0;
        }
        else
        {
            // H�z� eski de�erlerine d�nd�r
            moveSpeed = originalMoveSpeed;
            maxSpeed = originalMaxSpeed;
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
