using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [Header("Crash Effects & Sounds")]
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] AudioClip crashSFX;

    private AudioSource audioSource;
    private bool hasCrashed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCrash(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCrash(collision.gameObject);
    }

    void HandleCrash(GameObject other)
    {
        // Nhận diện đầu chạm đất dốc tuyết
        if (other.name.Contains("Terrain") || other.CompareTag("Ground"))
        {
            if (!hasCrashed)
            {
                PlayerController player = GetComponentInParent<PlayerController>();
                if (player != null)
                {
                    // Nếu đang bất tử, bỏ qua va chạm ngã
                    if (player.IsInvincible())
                    {
                        Debug.Log("Barry được bảo vệ bởi trạng thái Bất tử!");
                        return;
                    }

                    hasCrashed = true;
                    player.OnPlayerCrash(); // Báo cho PlayerController biết

                    // Chạy hiệu ứng tóe tuyết ngã
                    if (crashEffect != null)
                    {
                        crashEffect.Play();
                    }

                    // Phát âm thanh ngã
                    if (crashSFX != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(crashSFX);
                    }
                }
            }
        }
    }

    // Reset lại để cho phép ngã ở mạng tiếp theo
    public void ResetCrash()
    {
        hasCrashed = false;
    }
}
