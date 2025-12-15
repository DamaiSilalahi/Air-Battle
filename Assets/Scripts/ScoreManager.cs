using UnityEngine;
using TMPro; // PENTING: Harus ada karena kita pakai TextMeshPro

public class ScoreManager : MonoBehaviour
{
    // 1. SINGLETON: Membuat instance (contoh) dari ScoreManager ini
    public static ScoreManager Instance { get; private set; }

    [Header("Settings")]
    public TextMeshProUGUI ScoreText; // Slot untuk dihubungkan ke ScoreDisplay
    public int scorePerKill = 10;

    private int currentScore = 0;

    void Awake()
    {
        // Logika Singleton: Hanya izinkan satu instance dari ScoreManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Memastikan objek ini tidak hancur saat berpindah scene (jika ada)
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Inisialisasi tampilan skor saat game dimulai
        UpdateScoreDisplay();
    }

    /// <summary>
    /// Fungsi yang dipanggil oleh EnemyController saat musuh mati.
    /// </summary>
    public void AddScoreForKill()
    {
        currentScore += scorePerKill;
        UpdateScoreDisplay();
    }

    /// <summary>
    /// Memperbarui tampilan teks skor di layar.
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score : " + currentScore;
        }
        else
        {
            // Pesan peringatan jika Anda lupa menghubungkan ScoreText di Inspector
            Debug.LogWarning("ScoreText belum dihubungkan di Inspector ScoreManager!");
        }
    }

    // Anda bisa tambahkan fungsi lain di sini, misalnya:
    // public int GetCurrentScore() { return currentScore; }
    // public void ResetScore() { currentScore = 0; UpdateScoreDisplay(); }
}