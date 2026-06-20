using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Physics & Movement")]
    [SerializeField] float torqueAmount = 15f; // Lực xoay người nhào lộn
    [SerializeField] float boostSpeed = 30f;   // Tốc độ khi tăng tốc (Boost)
    [SerializeField] float normalSpeed = 15f;  // Tốc độ trượt bình thường
    [SerializeField] float slowSpeed = 8f;     // Tốc độ khi phanh (Slow)

    [Header("References")]
    [SerializeField] SurfaceEffector2D surfaceEffector2D;
    [SerializeField] ParticleSystem snowDust; // Bụi tuyết dưới ván trượt
    [SerializeField] GameObject gameOverPanel;

    // Logic Game & Trạng thái (Mạng, Điểm, Power-up, Combo)
    private Rigidbody2D rb;
    private bool canMove = true;
    private int score = 0;
    private int scoreMultiplier = 1;

    [Header("Lives & Checkpoints")]
    [SerializeField] int lives = 3; // Mặc định 3 mạng
    private Vector3 respawnPosition; // Vị trí hồi sinh tại checkpoint

    private bool isSpeedBoosted = false;
    private bool isInvincible = false;

    // Logic Nhào lộn (Tricks) & Điểm Tốc độ
    private bool isGrounded = true;
    private float lastRotationZ;
    private float airTimeRotation = 0f;
    private float speedScoreTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPosition = transform.position; // Đặt checkpoint ban đầu tại điểm xuất phát

        if (surfaceEffector2D == null)
        {
            surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        }
    }

    void Update()
    {
        if (canMove)
        {
            RotatePlayer();
            RespondToBoost();
            CalculateSpeedScore();
            TrackAirRotation();
        }
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-torqueAmount);
        }
    }

    void RespondToBoost()
    {
        if (surfaceEffector2D == null) return;

        if (isSpeedBoosted)
        {
            surfaceEffector2D.speed = boostSpeed * 1.5f;
            return;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            surfaceEffector2D.speed = slowSpeed;
        }
        else
        {
            surfaceEffector2D.speed = normalSpeed;
        }
    }

    // --- TÍNH ĐIỂM TỐC ĐỘ ---
    void CalculateSpeedScore()
    {
        if (isGrounded && rb.linearVelocity.magnitude > 2f)
        {
            speedScoreTimer += Time.deltaTime;
            if (speedScoreTimer >= 1f)
            {
                int speedPoints = Mathf.RoundToInt(rb.linearVelocity.magnitude * scoreMultiplier);
                AddScore(speedPoints);
                speedScoreTimer = 0f;
            }
        }
    }

    // --- ĐO GÓC NHÀO LỘN ---
    void TrackAirRotation()
    {
        if (!isGrounded)
        {
            float currentRotationZ = transform.eulerAngles.z;
            float deltaAngle = Mathf.DeltaAngle(lastRotationZ, currentRotationZ);
            airTimeRotation += Mathf.Abs(deltaAngle);
            lastRotationZ = currentRotationZ;
        }
    }

    public void DisableControls()
    {
        canMove = false;
        if (surfaceEffector2D != null)
        {
            surfaceEffector2D.speed = 0f; // Dừng dốc đẩy ván trượt khi ngã/về đích
        }
    }

    public bool CanMove()
    {
        return canMove;
    }

    // --- HỆ THỐNG MẠNG & CHECKPOINTS ---

    public void UpdateCheckpoint(Vector3 newCheckpointPosition)
    {
        respawnPosition = newCheckpointPosition + new Vector3(0f, 1.5f, 0f);
        Debug.Log("Đã cập nhật Checkpoint mới tại: " + newCheckpointPosition);
    }

    public int GetLives()
    {
        return lives;
    }

    // Xử lý khi Barry bị ngã (Crash)
    public void OnPlayerCrash()
    {
        if (!canMove) return; // Tránh việc tính ngã liên tục trong 1 lần rơi

        DisableControls();
        lives--;
        ResetMultiplier(); // Ngã sẽ mất chuỗi combo nhân điểm

        Debug.Log("Barry đã bị ngã! Số mạng còn lại: " + lives);

        if (lives > 0)
        {
            StartCoroutine(RespawnRoutine());
        }
        else
        {
            Debug.Log("GAME OVER! Hết mạng chơi.");
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(1.5f); // Đợi hiệu ứng ngã diễn ra

        // Hồi sinh tại Checkpoint gần nhất
        transform.position = respawnPosition;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        canMove = true;

        // Reset trạng thái ngã của CrashDetector ở đầu nhân vật
        CrashDetector crashDetector = GetComponentInChildren<CrashDetector>();
        if (crashDetector != null)
        {
            crashDetector.ResetCrash();
        }
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Hiện bảng GameOver lên
        }
        else
        {
            // Dự phòng nếu quên chưa kéo bảng GameOverPanel vào Inspector
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // --- HỆ THỐNG ĐIỂM SỐ & POWER-UPs ---

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score + " (x" + scoreMultiplier + ")");
    }

    public void AddCollectibleScore(int amount)
    {
        score += amount * scoreMultiplier;
        Debug.Log("Ăn Bông Tuyết! Điểm: " + score + " (Combo: x" + scoreMultiplier + ")");
    }

    public int GetScore()
    {
        return score;
    }

    public int GetMultiplier()
    {
        return scoreMultiplier;
    }

    public float GetSpeed()
    {
        return rb != null ? rb.linearVelocity.magnitude : 0f;
    }

    public void ResetMultiplier()
    {
        scoreMultiplier = 1;
        Debug.Log("Combo Reset! Hệ số nhân về: x1");
    }

    public void ActivateSpeedBoost(float duration)
    {
        StartCoroutine(SpeedBoostRoutine(duration));
    }

    IEnumerator SpeedBoostRoutine(float duration)
    {
        isSpeedBoosted = true;
        yield return new WaitForSeconds(duration);
        isSpeedBoosted = false;
    }

    public void ActivateInvincibility(float duration)
    {
        StartCoroutine(InvincibilityRoutine(duration));
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var r in renderers) r.color = new Color(1f, 0.9f, 0.4f);

        yield return new WaitForSeconds(duration);

        isInvincible = false;
        foreach (var r in renderers) r.color = Color.white;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    // Phát hiện va chạm vật lý
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Terrain") || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (snowDust != null) snowDust.Play();

            // Nhận diện tiếp đất hoàn thành vòng nhào lộn
            if (airTimeRotation > 0)
            {
                int flipsCount = Mathf.FloorToInt((airTimeRotation + 20f) / 360f);
                if (flipsCount > 0)
                {
                    scoreMultiplier += flipsCount;
                    int trickPoints = flipsCount * 100 * (scoreMultiplier - flipsCount);
                    score += trickPoints;
                    Debug.Log("Lộn vòng thành công! Số vòng: " + flipsCount + " | Cộng điểm: " + trickPoints + " | Combo mới: x" + scoreMultiplier);
                }
                airTimeRotation = 0f;
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isInvincible)
            {
                rb.linearVelocity *= 0.5f;
                ResetMultiplier();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Terrain") || collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            if (snowDust != null) snowDust.Stop();

            lastRotationZ = transform.eulerAngles.z;
            airTimeRotation = 0f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
