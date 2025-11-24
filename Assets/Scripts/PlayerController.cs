using UnityEngine;

public class PlayerShootShaderTrigger : MonoBehaviour
{
    public Material playerMaterial;       // Material yang dipakai player (dengan shader custom)
    public string shaderProperty = "_Glow";  // Nama parameter di shader yang mau diubah
    public float glowValueOnShoot = 2f;      // Nilai efek saat menembak
    public float glowValueNormal = 0.5f;     // Nilai normal / default
    public float duration = 0.2f;            // Lama efek shader

    private float timer;
    private bool isGlowing;

    void Start()
    {
        // Set shader ke kondisi normal di awal
        playerMaterial.SetFloat(shaderProperty, glowValueNormal);
    }

    void Update()
    {
        // Kalau klik kiri → trigger shader glowing
        if (Input.GetMouseButtonDown(0))
        {
            playerMaterial.SetFloat(shaderProperty, glowValueOnShoot);
            timer = duration;
            isGlowing = true;
        }

        // Hitung waktu untuk kembali normal
        if (isGlowing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                playerMaterial.SetFloat(shaderProperty, glowValueNormal);
                isGlowing = false;
            }
        }
    }
}
