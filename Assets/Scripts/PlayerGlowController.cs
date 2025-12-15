using UnityEngine;
using System.Collections;

public class PlayerGlowController : MonoBehaviour
{
    [Header("Glow Object")]
    public GameObject glowObject;

    [Header("Glow Settings")]
    public float glowDuration = 0.15f;

    Coroutine glowCoroutine; // 🔹 TAMBAHAN KECIL

    void Start()
    {
        if (glowObject != null)
            glowObject.SetActive(false);
    }

    public void TriggerGlow()
    {
        if (glowObject == null) return;

        // 🔹 HENTIKAN glow sebelumnya
        if (glowCoroutine != null)
            StopCoroutine(glowCoroutine);

        glowCoroutine = StartCoroutine(GlowEffect());
    }

    IEnumerator GlowEffect()
    {
        glowObject.SetActive(true);
        yield return new WaitForSeconds(glowDuration);
        glowObject.SetActive(false);
    }
}
