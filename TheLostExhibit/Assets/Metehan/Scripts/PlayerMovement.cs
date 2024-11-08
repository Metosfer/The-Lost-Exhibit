using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7000f;
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private float groundDrag = 5f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private float maxSpeed = 8f;

    [Header("References")]
    [SerializeField] private Animator animator; // Animator referansýný Inspector'dan atayacaðýz

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private Transform orientation;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        orientation = transform.Find("Orientation");
        if (orientation == null)
        {
            Debug.LogError("Orientation objesi bulunamadý!");
        }

        // Eðer animator referansý atanmamýþsa, otomatik bul
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component'i bulunamadý!");
            }
        }
    }

    private void Update()
    {
        ProcessInputs();
        SpeedControl();
        UpdateAnimations();

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void UpdateAnimations()
    {
        // Hareket input'u varsa ve yerdeyse koþma animasyonunu çalýþtýr
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnCollisionStay(Collision collision)
    {
        // Ground tag'ine sahip objelerle temas kontrolü
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Ground tag'li objeden ayrýlma kontrolü
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
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

        float currentForce = moveSpeed;
        if (!isGrounded)
        {
            currentForce *= airMultiplier;
        }

        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection.normalized * currentForce * Time.fixedDeltaTime, ForceMode.Force);
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

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Zýplama anýnda hemen grounded'ý false yap
    }
}
