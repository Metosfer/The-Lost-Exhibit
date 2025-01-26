using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    public bool pickingUpNow = false;
    public bool playerAttacking = false; // Saldýrý durumu
    public bool playerCanAttack = false;

    private readonly int isRunningHash = Animator.StringToHash("isRunning");
    private readonly int isPickingUpHash = Animator.StringToHash("isPickingUp");
    private readonly int attackHash = Animator.StringToHash("attack"); // "attack" trigger parametresi eklendi

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            playerCanAttack = true;
        }

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
        HandleAttack(); // Sol týklama ile saldýrý animasyonunu tetikleme
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

    private void HandleAttack()
    {
        if (playerCanAttack == true && Input.GetMouseButtonDown(0)) // Sol týklama algýlandýðýnda
        {
            animator.SetTrigger("attack"); // "attack" animasyonunu tetikle
            playerAttacking = true; // Saldýrý durumu true yap
            StartCoroutine(ResetAttackState()); // Saldýrý durumu resetleme coroutine baþlat
            
        }
    }

    private IEnumerator ResetAttackState()
    {

        
        yield return new WaitForSeconds(1.1f);
        playerAttacking = false;


    }
}



