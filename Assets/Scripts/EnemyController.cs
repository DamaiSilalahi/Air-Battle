using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Status Musuh")]
    public int maxHP = 3;
    private int currentHP;

    [Header("Gerak")]
    public float kecepatanGerak = 3f;
    public float durasiMengecil = 0.5f;

    [Header("Senjata")]
    public GameObject peluruJahatPrefab;
    public Transform firePoint;
    public float intervalTembak = 1.5f;

    private Transform playerPos;
    private bool sudahMati = false;
    private Vector3 skalaAwal;
    private Collider myCollider;
    private float timerNembak;

    void Start()
    {
        currentHP = maxHP;
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
        arah.y = 0;
        arah.Normalize();
        transform.position += arah * kecepatanGerak * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0, 180, 0);

        timerNembak += Time.deltaTime;
        if (timerNembak >= intervalTembak)
        {
            TembakPlayer();
            timerNembak = 0f;
        }
    }

    void TembakPlayer()
    {
        if (peluruJahatPrefab != null && playerPos != null)
        {
            Vector3 spawnPos = (firePoint != null) ? firePoint.position : transform.position;

            Vector3 arahTembak = playerPos.position - spawnPos;
            arahTembak.y = 0;

            Quaternion rotasiPeluru = Quaternion.LookRotation(arahTembak);

            Instantiate(peluruJahatPrefab, spawnPos, rotasiPeluru);
        }
    }

    bool sudahMeledak() { return sudahMati; }

    public void KenaTembak()
    {
        if (sudahMati) return;

        currentHP--;
        if (currentHP <= 0) Mati();
    }

    void Mati()
    {
        sudahMati = true;
        if (ScoreManager.Instance != null) ScoreManager.Instance.AddScoreForKill();
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