using UnityEngine;

public class PlayerJumpAnna : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public bool IsGrounded { get; private set; }

    private Rigidbody rb;
    private bool jumped;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        jumped = true;
    }

    public void ResetJumpState()
    {
        jumped = false;
    }

    public bool Jumped
    {
        get { return jumped; }
    }
}
