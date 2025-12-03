using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject FinishPanel;

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
    }

    public void StartGame()
    {
        StartPanel.SetActive(false);
        FinishPanel.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void FinishGame()
    {
        FinishPanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void RestartGame()
    {
        PlayerPrefs.SetInt("restart", 1);

        AudioListener.pause = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
