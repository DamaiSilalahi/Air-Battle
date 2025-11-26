using UnityEngine;
using System.Collections;

public class EnemyAutoFlash : MonoBehaviour
{
    public Renderer rend;                // <--- WAJIB Renderer (bukan SpriteRenderer)
    public Color flashColor = Color.red;
    public float flashTime = 0.2f;
    public float interval = 2f;

    private Color originalColor;

    void Start()
    {
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();  // otomatis cari MeshRenderer

        originalColor = rend.material.color;
        StartCoroutine(FlashLoop());
    }

    IEnumerator FlashLoop()
    {
        while (true)
        {
            rend.material.color = flashColor;
            yield return new WaitForSeconds(flashTime);

            rend.material.color = originalColor;
            yield return new WaitForSeconds(interval);
        }
    }
}
