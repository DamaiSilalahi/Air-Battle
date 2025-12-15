using UnityEngine;
using UnityEngine.UI; // Wajib untuk bekerja dengan elemen Text
using System.Linq; // Tambahkan ini untuk menggunakan Linq (memudahkan pencarian)

public class ScoreManager : MonoBehaviour
{
    // Pola Singleton
    public static ScoreManager Instance { get; private set; }

    // Ubah ini menjadi private/internal jika tidak ingin diakses dari Inspector
    // public Text scoreText; // DIHAPUS

    private Text scoreText; // Cari komponen Text saat runtime
    private int currentScore = 0;
    private const int SCORE_PER_KILL = 10;

    void Awake()
    {
        // Implementasi Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Opsional
        }

        // --- PENCARIAN TEKS OTOMATIS (Tanpa Drag & Drop) ---
        FindScoreTextAutomatically();
        // ---------------------------------------------------

        UpdateScoreUI();
    }

    // Fungsi pencari Text yang berdiri sendiri
    private void FindScoreTextAutomatically()
    {
        // Cara paling sederhana: Cari objek Text di seluruh scene
        Text[] allTexts = FindObjectsOfType<Text>();

        // Coba temukan Text yang diberi nama spesifik 'ScoreDisplay'
        scoreText = allTexts.FirstOrDefault(t => t.name == "ScoreDisplay");

        if (scoreText == null)
        {
            Debug.LogError("Komponen Text UI dengan nama 'ScoreDisplay' TIDAK DITEMUKAN di scene. Skor tidak akan ditampilkan!");
            // Jika tidak ditemukan, ambil Text pertama sebagai fallback (risiko salah)
            if (allTexts.Length > 0)
            {
                scoreText = allTexts[0];
                Debug.LogWarning("Menggunakan Text pertama di scene sebagai fallback. Pastikan namanya 'ScoreDisplay' untuk keamanan.");
            }
        }
    }

    // Fungsi publik yang dipanggil dari EnemyController
    public void AddScoreForKill()
    {
        currentScore += SCORE_PER_KILL;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString("D5");
        }
        // Jika scoreText tetap null (tidak ditemukan di FindScoreTextAutomatically), error sudah dilaporkan di sana
    }
}