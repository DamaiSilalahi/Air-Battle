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
        // === CEGAH PLAYER GERAK KETIKA GAME PAUSE ===
        if (Time.timeScale == 0f) return;

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

        // --- 3. TEMBAK (Klik Kiri) ---
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletPrefab != null)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
}
