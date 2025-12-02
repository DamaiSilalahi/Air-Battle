using UnityEngine;

public class ClickToFlashAllEnemies : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // klik kiri
        {
            FlashAllEnemies();
        }
    }

    void FlashAllEnemies()
    {
        FlashWhenHit[] enemies = FindObjectsOfType<FlashWhenHit>();

        foreach (var e in enemies)
        {
            e.Flash();
        }
    }
}
