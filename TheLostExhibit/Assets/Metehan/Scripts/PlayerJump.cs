using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12f;

    private Rigidbody rb;
    public bool IsGrounded { get; private set; } // Yere temas bilgisini d��ar�ya a�ar
    public bool IsJumping { get; private set; } // Z�plama bilgisini d��ar�ya a�ar

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            Jump();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            IsJumping = false; // Yere temas edince z�plama durumu sonlan�r
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
            IsJumping = true; // Yerden ayr�ld���nda z�plama durumu ba�lar
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        IsGrounded = false;
        IsJumping = true; // Z�plama an�nda isJumping true olur
    }
}
