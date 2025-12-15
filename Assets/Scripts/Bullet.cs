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
        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;

        if (other.CompareTag("Enemy"))
        {
            FlashWhenHit flash = other.GetComponentInParent<FlashWhenHit>();
            if (flash != null) flash.Flash();

            EnemyController musuh = other.GetComponent<EnemyController>();
            if (musuh == null) musuh = other.GetComponentInParent<EnemyController>();

            if (musuh != null)
            {
                musuh.KenaTembak(); // 🚨 Panggilan Score terjadi di sini!
            }

            Meledak();
            return;
        }

        if (other.isTrigger) return;
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