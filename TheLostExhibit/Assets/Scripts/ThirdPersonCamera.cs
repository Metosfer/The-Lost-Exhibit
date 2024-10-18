using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Takip edilecek karakter
    public Vector3 offset = new Vector3(0f, 2f, -5f); // Kameranýn karaktere göre pozisyonu
    public float mouseSensitivity = 5f; // Mouse hassasiyeti
    public float smoothTime = 0.1f; // Kameranýn hareketinin yumuþaklýðý

    private float pitch = 0f;
    private float yaw = 0f;
    private Vector3 currentVelocity;

    void Start()
    {
        // Mouse hareketi baþlangýç deðerleri
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Mouse hareketini oku
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -40f, 85f); // Dikey bakýþ açýsýný sýnýrlamak için

        // Kameranýn açýsýný mouse hareketine göre ayarla
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Kameranýn pozisyonunu karakterin etrafýnda hareket ettir (sabit offset ile)
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
