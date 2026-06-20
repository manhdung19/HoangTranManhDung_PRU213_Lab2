using UnityEngine;

public class Checkpoint : MonoBehaviour
{
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
                player.UpdateCheckpoint(transform.position);
            }
        }
    }
}
