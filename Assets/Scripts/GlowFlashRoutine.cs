using UnityEngine;
using System.Collections; // Wajib ada untuk Coroutine

public class GlowFlashRoutine : MonoBehaviour
{
    // Tidak perlu public, bisa diambil otomatis
    private MeshRenderer rend;

    // Ini harus PUBLIC agar bisa diatur di Inspector PlayerController
    public Material glowMaterial;

    public float flashTime = 0.2f;

    // Warna emission default saat tidak glow (biasanya hitam)
    private Color originalEmission = Color.black;

    // Warna dan Intensitas yang akan dipancarkan
    public Color flashGlowColor = Color.white;
    public float flashIntensity = 5f;

    void Start()
    {
        rend = GetComponentInChildren<MeshRenderer>();

        if (rend == null)
        {
            Debug.LogError("GlowFlashRoutine requires a MeshRenderer component!");
            return;
        }

        // Ambil material yang benar dari Element 0
        if (rend.materials.Length > 0)
        {
            glowMaterial = rend.materials[0];
            glowMaterial.EnableKeyword("_EMISSION");
            glowMaterial.renderQueue = 3000; // Coba render Transparent untuk visibility
            glowMaterial.SetColor("_EmissionColor", originalEmission);
        }
    }

    // Fungsi ini dipanggil dari PlayerController
    public void Flash()
    {
        // Berhenti Coroutine yang sedang berjalan (agar glow tidak tumpang tindih)
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // 1. SET GLOW TERTINGGI (Putih Intensitas 5)
        Color finalGlow = flashGlowColor * flashIntensity;
        glowMaterial.SetColor("_EmissionColor", finalGlow);

        // 2. Tunggu Durasi
        yield return new WaitForSeconds(flashTime);

        // 3. MATIKAN GLOW (Kembalikan ke Hitam/Original Emission)
        glowMaterial.SetColor("_EmissionColor", originalEmission);
    }
}