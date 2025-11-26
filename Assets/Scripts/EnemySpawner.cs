using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    public Vector2 xRange = new Vector2(-6f, 6f);
    public Vector2 zRange = new Vector2(10f, 12f);
    public float yHeight = 0f; 

    private float timer = 0f;
    private float currentSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentSpawnTime)
        {
            SpawnEnemy();
            timer = 0f;
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // Posisi Acak 3D
        float randomX = Random.Range(xRange.x, xRange.y);
        float randomZ = Random.Range(zRange.x, zRange.y);

        Vector3 spawnPos = new Vector3(randomX, yHeight, randomZ);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}