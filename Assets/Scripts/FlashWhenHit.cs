using UnityEngine;

public class FlashWhenHit : MonoBehaviour
{
    public MeshRenderer rend;
    public Color flashColor = Color.red;
    public float flashTime = 0.2f;

    private Color originalColor;

    void Start()
    {
        if (rend == null)
            rend = GetComponentInChildren<MeshRenderer>();

        // Ambil warna asli dengan cara universal
        originalColor = rend.material.color;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    System.Collections.IEnumerator FlashRoutine()
    {
        // Set warna jadi merah
        rend.material.color = flashColor;

        // Tunggu
        yield return new WaitForSeconds(flashTime);

        // Balikkan ke warna asli
        rend.material.color = originalColor;
    }
}
