using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12f;
    public Animator animator;

    private Rigidbody rb;
    public bool IsGrounded { get; private set; } // Yere temas bilgisini dýþarýya açar

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
            animator.ResetTrigger("isJumping");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    private void Jump()
    {
        animator.SetTrigger("isJumping");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        IsGrounded = false;
    }
}
