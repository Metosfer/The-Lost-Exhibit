using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonRigidbodyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public Transform cam;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Dönmeyi rigidbody ile yapacaðýz, fizik motoru döndürmesin
    }

    void Update()
    {
        // Hareket giriþi al
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Eðer hareket ediyorsa
        if (direction.magnitude >= 0.1f)
        {
            // Karakterin bakýþ açýsýný kameraya göre döndür
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Karakteri ileriye doðru hareket ettir
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        // Fizik tabanlý hareket
        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
