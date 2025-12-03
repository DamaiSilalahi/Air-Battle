using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;   // drag GameManager ke sini

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);

        if (gameManager != null)
        {
            gameManager.FinishGame();   // Panggil FINISH dari GameManager
        }
    }
}
