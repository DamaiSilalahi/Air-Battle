using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

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
        if (!sudahMeledak)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Peluru tabrak: " + other.name);

        if (other.CompareTag("Player") || other.CompareTag("Bullet")) return;
        if (other.isTrigger) return;

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Flash musuh!");
            FlashWhenHit flash = other.GetComponent<FlashWhenHit>();
            if (flash != null) flash.Flash();
        }

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
