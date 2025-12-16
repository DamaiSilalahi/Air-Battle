using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject FinishPanel;
    public GameObject ScoreUIObject;

    public static GameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("restart", 0) == 1)
        {
            PlayerPrefs.SetInt("restart", 0);
            Time.timeScale = 1f;
            AudioListener.pause = false;

            if (StartPanel != null) StartPanel.SetActive(false);
            if (FinishPanel != null) FinishPanel.SetActive(false);
            if (ScoreUIObject != null) ScoreUIObject.SetActive(true);
            return;
        }

        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (StartPanel != null) StartPanel.SetActive(true);
        if (FinishPanel != null) FinishPanel.SetActive(false);
        if (ScoreUIObject != null) ScoreUIObject.SetActive(false);
    }

    public void StartGame()
    {
        if (StartPanel != null) StartPanel.SetActive(false);
        if (FinishPanel != null) FinishPanel.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        if (ScoreUIObject != null) ScoreUIObject.SetActive(true);
    }

    public void FinishGame()
    {
        if (FinishPanel != null) FinishPanel.SetActive(true);

        Time.timeScale = 0f;
        AudioListener.pause = true;

        if (ScoreUIObject != null) ScoreUIObject.SetActive(false);

        // Pastikan pemanggilan menggunakan nama class yang benar (SimpleScoreManager)
        if (SimpleScoreManager.instance != null)
        {
            // Panggil fungsi publik yang sekarang sudah bisa diakses
            SimpleScoreManager.instance.UpdateUI();
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