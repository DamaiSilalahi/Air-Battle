using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Glow Settings")]
    public Color glowColor = Color.white;
    public float glowIntensity = 5f;
    public float glowDuration = 0.1f;

    public GameManager gameManager;

    // Referensi ke skrip baru
    private GlowFlashRoutine glowRoutine;

    void Start()
    {
        // Dapatkan skrip GlowFlashRoutine yang sudah terpasang
        glowRoutine = GetComponent<GlowFlashRoutine>();

        if (glowRoutine != null)
        {
            // Dapatkan MeshRenderer dari objek ini atau anak objek
            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

            if (meshRenderer != null && meshRenderer.materials.Length > 0)
            {
                // Inisialisasi Material Instance
                Material instance = meshRenderer.materials[0];

                // Pindahkan pengaturan ke skrip GlowFlashRoutine
                glowRoutine.glowMaterial = instance;
                glowRoutine.flashTime = glowDuration;
                glowRoutine.flashGlowColor = glowColor;
                glowRoutine.flashIntensity = glowIntensity;

                // Inisialisasi render properties
                instance.EnableKeyword("_EMISSION");
                instance.renderQueue = 3000; // Coba Transparent (lebih terlihat)
                instance.SetColor("_EmissionColor", Color.black); // Mulai dari hitam
            }
            else
            {
                Debug.LogError("Mesh Renderer atau Material tidak ditemukan!");
                glowRoutine = null; // Matikan routine jika gagal
            }
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
        if (glowRoutine != null)
        {
            glowRoutine.Flash(); // Panggil coroutine flash
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