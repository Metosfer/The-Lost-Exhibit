using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    public bool pickingUpNow = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component'i bulunamad�!");
            }
        }
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        // Ko�ma animasyonunu ba�latmak i�in hareket durumu
        bool isMoving = playerMovement != null && (playerMovement.horizontalInput != 0 || playerMovement.verticalInput != 0);
        animator.SetBool("isRunning", isMoving);

        // Z�plama animasyonunu ba�latmak i�in z�plama durumu
      //  animator.SetBool("isJumping", playerJump.IsJumping);
    }

    public void SetPickUpState(bool state)
    {
        animator.SetBool("isPickingUp", state);
         pickingUpNow = true;


        if (state)
        {
            // PickUp animasyonu s�resince `isPickingUp` aktif kalacak, ard�ndan kapat�lacak
            StartCoroutine(ResetPickUpState());
        }
    }

    private IEnumerator ResetPickUpState()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Animasyon s�resi kadar bekle
        animator.SetBool("isPickingUp", false); // Animasyonu sonland�r
        pickingUpNow= false;
    }
}
