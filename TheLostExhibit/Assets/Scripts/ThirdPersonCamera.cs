using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Kameran�n karaktere g�re pozisyonu
    public float mouseSensitivity = 5f; // Mouse hassasiyeti
    public float smoothTime = 0.1f; // Kameran�n hareketinin yumu�akl���

    private float pitch = 0f;
    private float yaw = 0f;
    private Vector3 currentVelocity;

    void Start()
    {
        // Mouse hareketi ba�lang�� de�erleri
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Mouse hareketini oku
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -40f, 85f); // Dikey bak�� a��s�n� s�n�rlamak i�in

        // Kameran�n a��s�n� mouse hareketine g�re ayarla
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Kameran�n pozisyonunu karakterin etraf�nda hareket ettir (sabit offset ile)
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
