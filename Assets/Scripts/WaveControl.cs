using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Vector2 randomOffset = Random.insideUnitCircle.normalized * minSpawnDistance;
        Vector3 spawnPosition = new Vector3(playerTransform.position.x + randomOffset.x, playerTransform.position.y, playerTransform.position.z + randomOffset.y);
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
}
