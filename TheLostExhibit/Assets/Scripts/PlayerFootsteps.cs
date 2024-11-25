using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Footstep Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepSounds; // Yürürken çalacak sesler
    [SerializeField] private float stepInterval = 0.5f; // Ad?m aral??? (saniye)

    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private bool isWalking = false;
    private float timeSinceLastStep = 0f;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerJump.IsGrounded && playerMovement.horizontalInput != 0f || playerMovement.verticalInput != 0f)
        {
            // E?er yere bas?l?ysa ve hareket ediyorsa
            isWalking = true;
        }
        else
        {
            // Hareket etmiyorsa
            isWalking = false;
        }

        if (isWalking)
        {
            timeSinceLastStep += Time.deltaTime;

            if (timeSinceLastStep >= stepInterval)
            {
                PlayFootstepSound();
                timeSinceLastStep = 0f;
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0 && audioSource != null)
        {
            // Rasgele bir ses seç
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }
}
