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

        originalColor = rend.material.color;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    System.Collections.IEnumerator FlashRoutine()
    {
        rend.material.color = flashColor;
        yield return new WaitForSeconds(flashTime);
        rend.material.color = originalColor;
    }
}
