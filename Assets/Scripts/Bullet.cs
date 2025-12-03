using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 20f;
    public float lifeTime = 2f;

    [Header("Visual")]
    public GameObject visualPeluru;  // Masukkan anak 'Charged' ke sini
    public GameObject visualLedakan; // Masukkan anak 'Hit' ke sini

    private bool sudahMeledak = false;

    void Start()
    {
        // 1. SETUP AWAL
        // Pastikan saat lahir: Peluru NYALA, Ledakan MATI
        if (visualPeluru != null) visualPeluru.SetActive(true);
        if (visualLedakan != null) visualLedakan.SetActive(false);

        // Hancurkan diri sendiri jika tidak kena apa-apa setelah sekian detik
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Jika sudah meledak, stop bergerak
        if (sudahMeledak) return;

        // Gerak Manual (Maju ke depan)
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    // --- BAGIAN PENTING: LOGIKA TABRAKAN ---
    void OnTriggerEnter(Collider other)
    {
        // A. Jangan meledak kalau kena Diri Sendiri atau Teman
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;

        // B. KHUSUS: Kalau kena MUSUH -> MELEDAK!
        // PENTING: Cek Tag "Enemy" DULUAN sebelum cek isTrigger!
        if (other.CompareTag("Enemy"))
        {
            Meledak();
            return;
        }

        // C. Kalau kena Trigger lain (misal sensor area/angin), abaikan
        if (other.isTrigger) return;

        // D. Kalau kena Tembok/Benda Padat -> MELEDAK
        Meledak();
    }

    void Meledak()
    {
        if (sudahMeledak) return;
        sudahMeledak = true;

        // --- GANTI VISUAL ---
        // Matikan gambar peluru (Charged)
        if (visualPeluru != null) visualPeluru.SetActive(false);
        // Nyalakan gambar ledakan (Hit)
        if (visualLedakan != null) visualLedakan.SetActive(true);

        // Hentikan gerakan peluru
        speed = 0;

        // Hancurkan objek total setelah ledakan selesai (misal 0.5 detik)
        Destroy(gameObject, 0.5f);
    }
}