using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Debug.Log("Input Vertical: " + Input.GetAxisRaw("Vertical"));
        // --- 1. GERAK MANUAL (WASD) ---
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical"); // Z untuk maju-mundur 3D

        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;

        // Pindahkan posisi secara manual (Tanpa MoveTo/Translate)
        if (direction.magnitude >= 0.1f)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        // --- 2. ROTASI MANUAL (Ikut Mouse) ---
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 lookDir = point - transform.position;

            // Hitung sudut rotasi Y
            float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;

            // Masukkan ke rotasi Y
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // --- 3. TEMBAK (Klik Kiri) ---
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletPrefab != null) // Cek biar tidak error kalau lupa pasang peluru
            {
                // Munculkan peluru di posisi pemain, dengan rotasi pemain
                Instantiate(bulletPrefab, transform.position, transform.rotation);
            }
        }
    }
}