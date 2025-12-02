using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    private float timer = 0f;
    public float zPosition = 10f;
    public float xMin = -6f;
    public float xMax = 6f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMusuh();
            timer = 0f; 
        }
    }

    void SpawnMusuh()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("LUPA MASUKIN PREFAB MUSUH BOS!");
            return;
        }

        float randomX = Random.Range(xMin, xMax);
        Vector3 posisiMuncul = new Vector3(randomX, 0, zPosition);

        Instantiate(enemyPrefab, posisiMuncul, Quaternion.identity);
    }
}