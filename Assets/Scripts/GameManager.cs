using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject FinishPanel;
    public GameObject ScoreUIObject;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // 2. Inisialisasi Instance di Awake()
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Opsional: DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("restart", 0) == 1)
        {
            PlayerPrefs.SetInt("restart", 0);
            Time.timeScale = 1f;
            AudioListener.pause = false;

            StartPanel.SetActive(false);
            FinishPanel.SetActive(false);
            return;
        }

        Time.timeScale = 0f;
        AudioListener.pause = true;

        StartPanel.SetActive(true);
        FinishPanel.SetActive(false);

        if (ScoreUIObject != null) ScoreUIObject.SetActive(false);
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        FinishPanel.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (ScoreUIObject != null) ScoreUIObject.SetActive(true);
    }

    public void FinishGame()
    {
        FinishPanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (ScoreUIObject != null) ScoreUIObject.SetActive(false);

        if (ScoreManager.Instance != null)
        {
            // Hapus (FinishPanel). Fungsi DisplayFinalScore() tidak memerlukan parameter.
            ScoreManager.Instance.DisplayFinalScore();
        }
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("restart", 1);

        AudioListener.pause = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
