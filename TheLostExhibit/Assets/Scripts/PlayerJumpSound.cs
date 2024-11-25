using UnityEngine;

public class PlayerJumpSound : MonoBehaviour
{
    [Header("Jump Sound Settings")]
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioClip jumpSound;

    private PlayerJumpAnna playerJump;

    private void Start()
    {
        playerJump = GetComponent<PlayerJumpAnna>();

        if (jumpAudioSource == null)
            jumpAudioSource = GetComponent<AudioSource>();

        if (jumpAudioSource == null)
            Debug.LogError("JumpAudioSource eksik! Z?plama sesini çalamazs?n?z.");
    }

    private void Update()
    {
        if (playerJump.IsGrounded == false && playerJump.Jumped)
        {
            PlayJumpSound();
        }
    }

    private void PlayJumpSound()
    {
        if (jumpSound != null && jumpAudioSource != null)
        {
            jumpAudioSource.PlayOneShot(jumpSound);
        }
        playerJump.ResetJumpState();
    }
}
