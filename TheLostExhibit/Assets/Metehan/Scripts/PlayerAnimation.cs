using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    public bool pickingUpNow = false;

    private readonly int isRunningHash = Animator.StringToHash("isRunning");
    private readonly int isPickingUpHash = Animator.StringToHash("isPickingUp");

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
        bool isMoving = playerMovement != null &&
            (playerMovement.horizontalInput != 0 || playerMovement.verticalInput != 0);
        animator.SetBool(isRunningHash, isMoving);
    }

    public void SetPickUpState(bool state)
    {
        if (state)
        {
            animator.SetBool(isPickingUpHash, true);
            pickingUpNow = true;
            StartCoroutine(ResetPickUpState());
        }
    }

    private IEnumerator ResetPickUpState()
    {
        // Mevcut animasyon state'ini al
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Animasyonun bitmesini bekle
        while (stateInfo.normalizedTime < 0.9f) // 0.9f kullanarak animasyonun neredeyse sonuna geldiðimizden emin oluyoruz
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        // Animasyonu yumuþak bir þekilde sonlandýr
        animator.SetBool(isPickingUpHash, false);
        pickingUpNow = false;
    }
}