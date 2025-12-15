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
        else
        {
            Debug.LogWarning("Player dengan tag 'Player' tidak ditemukan!");
        }
    }

    void Update()
    {
        if (sudahMati || playerPos == null) return;

        Vector3 arah = playerPos.position - transform.position;
        arah.y = 0;

        if (arah != Vector3.zero)
        {
            arah.Normalize();
            transform.position += arah * kecepatanGerak * Time.deltaTime;

            transform.rotation = Quaternion.LookRotation(arah);
        }

        timerNembak += Time.deltaTime;
        if (timerNembak >= intervalTembak)
        {
            TembakPlayer();
            timerNembak = 0f;
        }
    }

    void TembakPlayer()
    {
        if (peluruJahatPrefab == null) return;

        Instantiate(
            peluruJahatPrefab,
            transform.position,
            transform.rotation
        );
    }

    public void KenaTembak()
    {
        if (sudahMati) return;

        sudahMati = true;

        if (SimpleScoreManager.instance != null)
        {
            SimpleScoreManager.instance.TambahSkor(10);
        }
        else
        {
            Debug.LogWarning("SimpleScoreManager belum ada di Scene!");
        }

        if (myCollider != null)
            myCollider.enabled = false;

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

        Destroy(gameObject);
    }
}
