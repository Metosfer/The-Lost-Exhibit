using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float speed = 5f; // Hareket hýzý
    [SerializeField] float jumpForce = 10f;
    
    [SerializeField] float rotationSpeed = 10f; // Dönüþ hýzý

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {



        // Yatay ve dikey input deðerlerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Hareket yönünü hesapla
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Eðer hareket varsa (yani input deðerleri 0 deðilse)
        if (movement != Vector3.zero)
        {
            // Hareket yönüne doðru bakacak açýyý hesapla
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            // Karakteri yumuþak bir þekilde döndür
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // Karakteri hareket ettir
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
            
            
        }

        



    }

 


}