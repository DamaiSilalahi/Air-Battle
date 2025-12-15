using UnityEngine;
using System.Collections;

public class FlashWhenHit : MonoBehaviour
{
    public float flashTime = 0.2f;

    private Material mat;
    private Coroutine flashRoutine;

    void Awake()
    {
        MeshRenderer rend = GetComponentInChildren<MeshRenderer>();
        mat = rend.material;

        // pastikan awalnya KUNING
        mat.SetFloat("_FlashAmount", 0f);
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // jadi MERAH
        mat.SetFloat("_FlashAmount", 1f);

        yield return new WaitForSeconds(flashTime);

        // balik KUNING
        mat.SetFloat("_FlashAmount", 0f);
    }
}
