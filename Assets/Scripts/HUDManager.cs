using UnityEngine;
using TMPro; // Sử dụng TextMeshPro

public class HUDManager : MonoBehaviour
{
    [Header("UI Text Fields")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject powerUpLabel;

    [Header("Player Reference")]
    [SerializeField] PlayerController player;

    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<PlayerController>();
        }

        if (powerUpLabel != null)
        {
            powerUpLabel.SetActive(false);
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + player.GetScore();
            }

            if (multiplierText != null)
            {
                multiplierText.text = "Combo: x" + player.GetMultiplier();
            }

            if (speedText != null)
            {
                // Quy đổi vận tốc sang km/h hiển thị cho đẹp mắt
                int displaySpeed = Mathf.RoundToInt(player.GetSpeed() * 3.6f);
                speedText.text = "Speed: " + displaySpeed + " km/h";
            }

            if (livesText != null)
            {
                int currentLives = player.GetLives();
                string hearts = "";
                for (int i = 0; i < currentLives; i++)
                {
                    hearts += "❤️";
                }
                livesText.text = "Lives: " + (hearts == "" ? "0" : hearts);
            }

            if (powerUpLabel != null)
            {
                powerUpLabel.SetActive(player.IsInvincible());
            }
        }
    }
}
