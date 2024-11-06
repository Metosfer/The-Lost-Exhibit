using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float speed = 5f; // Hareket h�z�
    [SerializeField] float jumpForce = 10f;
    
    [SerializeField] float rotationSpeed = 10f; // D�n�� h�z�

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {



        // Yatay ve dikey input de�erlerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Hareket y�n�n� hesapla
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // E�er hareket varsa (yani input de�erleri 0 de�ilse)
        if (movement != Vector3.zero)
        {
            // Hareket y�n�ne do�ru bakacak a��y� hesapla
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            // Karakteri yumu�ak bir �ekilde d�nd�r
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // Karakteri hareket ettir
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
            
            
        }

        



    }

 


}