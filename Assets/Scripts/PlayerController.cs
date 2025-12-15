using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    public int maxHealth = 5;
    [SerializeField] private int currentHealth;

    [Header("Glow Settings")]
    public Color glowColor = Color.white;
    public float glowIntensity = 5f;
    public float glowDuration = 0.1f;

    public GameManager gameManager;

    private PlayerGlowController glowController;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Game Start! Player HP: " + currentHealth);

        glowController = GetComponent<PlayerGlowController>();

        if (glowController == null)
        {
            Debug.LogError("PlayerGlowController tidak ditemukan di Player!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlashGlow();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Die();
        }
    }

    public void FlashGlow()
    {
        if (glowController != null)
        {
            glowController.TriggerGlow();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        FlashGlow();

        Debug.Log("Player Kena Tembak! Sisa HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);

        if (gameManager != null)
        {
            gameManager.FinishGame();
        }
        else if (GameManager.instance != null) 
        {
            GameManager.instance.FinishGame();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            HandlePlayerDeath();
        }
    }

    void HandlePlayerDeath()
    {
        Die();
    }
}