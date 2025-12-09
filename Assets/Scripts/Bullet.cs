using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 20f;
    public float lifeTime = 2f;

    [Header("Visual")]
    public GameObject visualPeluru;
    public GameObject visualLedakan;

    private bool sudahMeledak = false;

    void Start()
    {
        if (visualPeluru != null) visualPeluru.SetActive(true);
        if (visualLedakan != null) visualLedakan.SetActive(false);

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (sudahMeledak) return;

        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Abaikan Player atau Bullet lain
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;

        // --- KENA MUSUH ---
        if (other.CompareTag("Enemy"))
        {
            // 🔥 Panggil FlashWhenHit (efek flash warna merah)
            FlashWhenHit flash = other.GetComponentInParent<FlashWhenHit>();
            if (flash != null) flash.Flash();

            // ⚠️ BARIS DI BAWAH INI DIHAPUS karena class GlowFlash sudah dihapus:
            // GlowFlash glow = other.GetComponentInParent<GlowFlash>();
            // if (glow != null) glow.Flash();

            Meledak();
            return;
        }

        // Abaikan trigger lain
        if (other.isTrigger) return;

        // Kena benda padat -> meledak
        Meledak();
    }

    void Meledak()
    {
        if (sudahMeledak) return;
        sudahMeledak = true;

        if (visualPeluru != null) visualPeluru.SetActive(false);
        if (visualLedakan != null) visualLedakan.SetActive(true);

        speed = 0;

        Destroy(gameObject, 0.5f);
    }
}