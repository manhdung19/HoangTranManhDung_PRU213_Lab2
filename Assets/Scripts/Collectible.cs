using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { Snowflake, SpeedBoost, Invincibility }

    [Header("Collectible Settings")]
    [SerializeField] CollectibleType type;
    [SerializeField] int scoreValue = 10;
    [SerializeField] float duration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.name.Contains("Barry") || collision.transform.parent?.name == "Barry")
        {
            PlayerController player = collision.GetComponentInParent<PlayerController>();
            if (player == null)
            {
                player = collision.GetComponent<PlayerController>();
            }

            if (player != null)
            {
                if (type == CollectibleType.Snowflake)
                {
                    player.AddCollectibleScore(scoreValue); // Sử dụng hàm nhân điểm
                }
                else if (type == CollectibleType.SpeedBoost)
                {
                    player.ActivateSpeedBoost(duration);
                }
                else if (type == CollectibleType.Invincibility)
                {
                    player.ActivateInvincibility(duration);
                }

                Destroy(gameObject);
            }
        }
    }
}
