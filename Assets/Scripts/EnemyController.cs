using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Gerak")]
    public float kecepatanGerak = 3f;
    public float durasiMengecil = 0.5f;

    [Header("Senjata")]
    public GameObject peluruJahatPrefab; 
    public float intervalTembak = 1f;   

    private Transform playerPos;
    private bool sudahMati = false;
    private Vector3 skalaAwal;
    private Collider myCollider;
    private float timerNembak;

    void Start()
    {
        skalaAwal = transform.localScale;
        myCollider = GetComponent<Collider>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerPos = player.transform;
        }
    }

    void Update()
    {
        if (sudahMeledak() || playerPos == null) return;

        Vector3 arah = playerPos.position - transform.position;
        arah.Normalize();
        transform.position += arah * kecepatanGerak * Time.deltaTime;

        float sudut = Mathf.Atan2(arah.y, arah.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, sudut + 90);

        timerNembak += Time.deltaTime;
        if (timerNembak >= intervalTembak)
        {
            TembakPlayer();
            timerNembak = 0f; 
        }
    }

    void TembakPlayer()
    {
        if (peluruJahatPrefab != null)
        {
            Instantiate(peluruJahatPrefab, transform.position, transform.rotation);
        }
    }

    bool sudahMeledak() { return sudahMati; }

    public void KenaTembak()
    {
        if (sudahMati) return;
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScoreForKill();
        }
        sudahMati = true;
        if (myCollider != null) myCollider.enabled = false;
        StartCoroutine(AnimasiMati());
    }

    IEnumerator AnimasiMati()
    {
        float timer = 0f;
        while (timer < durasiMengecil)
        {
            timer += Time.deltaTime;
            float progress = timer / durasiMengecil;
            transform.localScale = Vector3.Lerp(skalaAwal, Vector3.zero, progress);
            yield return null;
        }
        transform.localScale = Vector3.zero;
        Destroy(gameObject);
    }
}