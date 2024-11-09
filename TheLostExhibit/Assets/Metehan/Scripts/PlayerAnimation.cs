using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

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
        animator.SetBool("isJumping", playerJump.IsJumping);
    }
}
