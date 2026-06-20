using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float loadDelay = 2.0f;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] AudioClip finishSFX;

    private AudioSource audioSource;
    private bool hasFinished = false;

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
        if (collision.CompareTag("Player") || collision.name.Contains("Barry") || collision.transform.parent?.name == "Barry")
        {
            if (!hasFinished)
            {
                hasFinished = true;
                Debug.Log("Chúc mừng! Đã về đích chiến thắng!");

                // Phát hiệu ứng pháo hoa về đích (nếu có)
                if (finishEffect != null)
                {
                    finishEffect.Play();
                }

                // Phát âm thanh chiến thắng
                if (finishSFX != null && audioSource != null)
                {
                    audioSource.PlayOneShot(finishSFX);
                }

                // Vô hiệu hóa điều khiển
                PlayerController player = collision.GetComponentInParent<PlayerController>();
                if (player != null)
                {
                    player.DisableControls();
                }

                // Quay về MainMenu sau một thời gian trễ
                Invoke("LoadMainMenu", loadDelay);
            }
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
