using UnityEngine;
using TMPro; // PENTING: Harus ada untuk menggunakan TextMeshPro
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    // 1. SINGLETON: Agar bisa diakses oleh EnemyController, GameManager, dll.
    public static ScoreManager Instance { get; private set; }

    [Header("In-Game Display")]
    public TextMeshProUGUI ScoreText; // Slot untuk teks skor saat bermain (ScoreDisplay)
    public int scorePerKill = 10;

    // PERUBAHAN NAMA HEADER: Agar sesuai dengan FinishGame() di GameManager
    [Header("Finish Game Display")]
    public TextMeshProUGUI finalScoreText; // Slot untuk teks skor di panel Finish Game

    private int currentScore = 0;

    void Awake()
    {
        // Logika Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Pastikan skor ditampilkan 0 saat dimulai
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
    /// Memperbarui tampilan teks skor di layar utama (saat bermain).
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score : " + currentScore;
        }
        else
        {
            Debug.LogWarning("ScoreText belum dihubungkan di Inspector ScoreManager!");
        }
    }

    /// <summary>
    /// Dipanggil oleh GameManager saat FinishGame() dipanggil, untuk menampilkan skor akhir.
    /// </summary>
    public void DisplayFinalScore()
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = "SCORE AKHIR: " + currentScore;
        }
    }

    /// <summary>
    /// Memberikan nilai skor yang sedang berjalan (Dipanggil oleh GameManager).
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }
}