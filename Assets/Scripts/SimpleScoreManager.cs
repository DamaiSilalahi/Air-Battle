using UnityEngine;
using UnityEngine.UI;
public class SimpleScoreManager : MonoBehaviour
{
    public static SimpleScoreManager instance;

    [Header("Hubungkan UI Disini")]
    public Text scoreText;
    public Text finalScoreText;
    private int score = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    public void TambahSkor(int nilai)
    {
        score += nilai;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }

        if (finalScoreText != null)
        {
            finalScoreText.text = "FINAL SCORE: " + score;
        }
    }
}