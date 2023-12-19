using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveControl : MonoBehaviour
{
    public int maxWaves = 5;
    private int currentWave = 1;
    private int enemiesInWave = 3;
    public Transform playerTransform;
    public GameObject enemyPrefab;
    // Distância mínima entre o jogador e a posição de spawn dos inimigos
    public float minSpawnDistance = 10f;
    private int enemiesAlive = 0;

    [Header("Spawn Volume")]
    [SerializeField] private float VolumeX = 2f;
    [SerializeField] private float VolumeY = 2f;
    [SerializeField] private float VolumeZ = 2f;

    void Start()
    {
        Debug.Log("Iniciando Wave " + currentWave);
        StartNextWave();
    }

    void Update()
    {
        
    }

    public void EnemyDefeated()
    {
        if (currentWave > maxWaves)
        {
            Debug.Log("Você venceu todas as waves!");
        }

        StartNextWave();
    }

    private void StartNextWave()
    {
        Debug.Log("Iniciando Wave " + currentWave);

        enemiesInWave = currentWave + 2;

        SpawnEnemies();

        currentWave++;


        if (currentWave > maxWaves)
        {
            Debug.Log("Voce Ganhou!!!!");
        }

    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemiesInWave; i++)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            InstantiateEnemy(spawnPosition);
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 spawnPosition;

        do {
            spawnPosition = GetRandomNavMeshPoint();
        } while(Vector3.Distance(playerTransform.position, spawnPosition) < minSpawnDistance);

        return spawnPosition;
    }

    private void InstantiateEnemy(Vector3 spawnPosition)
    {
        Quaternion spawnRotation = Quaternion.identity; 
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        newEnemy.GetComponent<EnemyMovement>().target = playerTransform;
        newEnemy.GetComponent<HealthComponent>().onDeath.AddListener(OnEnemyDie);
        enemiesAlive++;
    }

    private void OnEnemyDie() {
        enemiesAlive--;

        if (enemiesAlive == 0) {
            EnemyDefeated();
        }
    }

    public Vector3 GetRandomNavMeshPoint()
    {
        // Generate a random point within the cube.
        Vector3 randomPoint = new Vector3(
            Random.Range(-VolumeX / 2, VolumeX / 2),
            Random.Range(-VolumeZ / 2, VolumeZ / 2),
            Random.Range(-VolumeY / 2, VolumeY / 2)
        );

        randomPoint += transform.position;

        // Initialize a NavMeshHit object to store the output.
        NavMeshHit hit;

        // Get the closest point on NavMesh.
        if (NavMesh.SamplePosition(randomPoint, out hit, Mathf.Max(VolumeX, VolumeZ, VolumeY), NavMesh.AllAreas))
        {
            // If the point is found, return it.
            return hit.position;
        }

        // If no point is found, return Vector3.zero or any fallback position.
        return Vector3.zero;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(VolumeX, VolumeZ, VolumeY));

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerTransform.position, minSpawnDistance);
    }
}
