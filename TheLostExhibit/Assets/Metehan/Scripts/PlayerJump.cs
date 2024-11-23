using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12f;
    public Animator animator;

    private Rigidbody rb;
    public bool IsGrounded { get; private set; } // Yere temas bilgisini dýþarýya açar
    public bool IsJumping { get; private set; } // Zýplama bilgisini dýþarýya açar

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
           // Debug.Log(collision.collider.name);
            IsGrounded = true;
            IsJumping = false; // Yere temas edince zýplama durumu sonlanýr
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
            IsJumping = true; // Yerden ayrýldýðýnda zýplama durumu baþlar
        }
    }

    private void Jump()
    {
        animator.SetBool("isJumping", true);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        IsGrounded = false;
        IsJumping = true; // Zýplama anýnda isJumping true olur
    }
}
