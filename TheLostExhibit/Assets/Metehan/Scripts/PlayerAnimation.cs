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
                Debug.LogError("Animator component'i bulunamadý!");
            }
        }
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        // Koþma animasyonunu baþlatmak için hareket durumu
        bool isMoving = playerMovement != null && (playerMovement.horizontalInput != 0 || playerMovement.verticalInput != 0);
        animator.SetBool("isRunning", isMoving);

        // Zýplama animasyonunu baþlatmak için zýplama durumu
      //  animator.SetBool("isJumping", playerJump.IsJumping);
    }

    public void SetPickUpState(bool state)
    {
        animator.SetBool("isPickingUp", state);
         pickingUpNow = true;


        if (state)
        {
            // PickUp animasyonu süresince `isPickingUp` aktif kalacak, ardýndan kapatýlacak
            StartCoroutine(ResetPickUpState());
        }
    }

    private IEnumerator ResetPickUpState()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Animasyon süresi kadar bekle
        animator.SetBool("isPickingUp", false); // Animasyonu sonlandýr
        pickingUpNow= false;
    }
}
