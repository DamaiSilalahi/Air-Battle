using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Glow Settings")]
    public Color glowColor = Color.white;
    public float glowIntensity = 5f;
    public float glowDuration = 0.1f;

    public GameManager gameManager;

    private PlayerGlowController glowController;

    void Start()
    {
        // Ambil PlayerGlowController (TANPA ubah sistem lain)
        glowController = GetComponent<PlayerGlowController>();

        if (glowController == null)
        {
            Debug.LogError("PlayerGlowController tidak ditemukan di Player!");
        }
    }

    void Update()
    {
        // TRIGGER GLOW SAAT TEMBAK
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlashGlow();
        }

        // DIE TEST
        if (Input.GetKeyDown(KeyCode.M))
        {
            Die();
        }
    }

    void FlashGlow()
    {
        if (glowController != null)
        {
            glowController.TriggerGlow();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);

        if (gameManager != null)
        {
            gameManager.FinishGame();
        }
    }
}
