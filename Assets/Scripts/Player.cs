using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Camera mainCamera;

    void Start()
    {
        // 1. Cari Kamera
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("KAMERA TIDAK DITEMUKAN! Pastikan Tag MainCamera dipasang!");
        }

        // 2. --- [KODE BARU: MATIKAN TABRAKAN ANTAR LAYER] ---
        // Ini solusi pengganti Matrix Physics yang hilang
        int layerPlayer = LayerMask.NameToLayer("Player");
        int layerBullet = LayerMask.NameToLayer("Bullet");

        // Pastikan kedua layer itu ada sebelum dijalankan
        if (layerPlayer != -1 && layerBullet != -1)
        {
            // Perintah sakti untuk mematikan tabrakan antara dua layer ini
            Physics.IgnoreLayerCollision(layerPlayer, layerBullet);
            Debug.Log("Tabrakan Player vs Bullet berhasil dimatikan lewat Script!");
        }
        else
        {
            Debug.LogError("Layer belum dibuat! Pastikan kamu sudah buat Layer 'Player' dan 'Bullet' di pojok kanan atas Unity.");
        }
    }

    void Update()
    {
        // === CEGAH PLAYER GERAK KETIKA GAME PAUSE ===
        if (Time.timeScale == 0f) return;

        // Debugging Input (Bisa dihapus nanti kalau sudah lancar)
        Debug.Log("Input Vertical: " + Input.GetAxisRaw("Vertical"));

        // --- 1. GERAK MANUAL (WASD) ---
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        if (direction.magnitude >= 0.1f)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // --- 2. ROTASI MANUAL (Ikut Mouse) ---
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 lookDir = point - transform.position;

                float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

        // --- 3. TEMBAK (Klik Kiri) ---
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletPrefab != null && firePoint != null)
            {
                // Menggunakan firePoint.rotation agar peluru searah dengan moncong
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                Debug.LogWarning("Bullet Prefab atau FirePoint belum dipasang di Inspector!");
            }
        }
    }
}