using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    // KITA BUAT SLOT UNTUK ANAK-ANAKNYA
    public GameObject visualPeluru;  // Masukkan "charged" ke sini
    public GameObject visualLedakan; // Masukkan "hit" ke sini
    // "warped-shooting-fx" biarkan nyala otomatis dari awal (karena itu efek suara/awal)

    private bool sudahMeledak = false; // Supaya tidak meledak 2 kali

    void Start()
    {
        // STEP 1: KONDISI AWAL
        // Pastikan peluru terlihat, dan ledakan disembunyikan
        if (visualPeluru != null) visualPeluru.SetActive(true);
        if (visualLedakan != null) visualLedakan.SetActive(false);

        // Hancurkan peluru jika tidak kena apa-apa setelah sekian detik
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Kalau belum meledak, terus maju ke depan
        if (!sudahMeledak)
        {
            // Bergerak relatif terhadap arah hadap sendiri (Z Axis)
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    // STEP 2: LOGIKA TABRAKAN
    void OnTriggerEnter(Collider other)
    {
        // Jangan meledak kalau kena Pesawat Sendiri (Player) atau kena Peluru lain
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;

        // Jangan meledak kalau kena Trigger "FirePoint" atau area sensor lain
        if (other.isTrigger) return;

        Meledak();
    }

    void Meledak()
    {
        if (sudahMeledak) return;
        sudahMeledak = true;

        // 1. Matikan visual peluru (charged)
        if (visualPeluru != null) visualPeluru.SetActive(false);

        // 2. Nyalakan visual ledakan (hit)
        if (visualLedakan != null) visualLedakan.SetActive(true);

        // 3. Stop pergerakan (diam di tempat tabrakan)
        speed = 0;

        // 4. Hancurkan objek total setelah ledakan selesai (misal 0.5 detik)
        Destroy(gameObject, 0.5f);
    }
}