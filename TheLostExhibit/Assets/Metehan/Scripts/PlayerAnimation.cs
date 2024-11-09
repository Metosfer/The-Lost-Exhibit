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
        animator.SetBool("isJumping", playerJump.IsJumping);
    }
}
