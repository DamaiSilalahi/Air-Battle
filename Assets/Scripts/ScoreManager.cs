using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("In-Game Display")]
    public TextMeshProUGUI ScoreText;
    public int scorePerKill = 10;

    [Header("Finish Game Display")]
    public TextMeshProUGUI finalScoreText;

    private int currentScore = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        UpdateScoreDisplay();
    }

    public void AddScoreForKill()
    {
        currentScore += scorePerKill;
        UpdateScoreDisplay();
    }

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

    public void DisplayFinalScore()
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = "SCORE AKHIR: " + currentScore;
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}