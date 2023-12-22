using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class WaveControl : MonoBehaviour
{
    public class RandonNavMeshPointException : System.Exception
    {
        public RandonNavMeshPointException() : base("Não foi possivel encontrar um ponto valido") {}
    }
    //[SerializeField] private int enemiesAtStart = 3;
    public int maxWaves;
    private int currentWave = 0;
    private int enemiesInWave;
    public Transform playerTransform;
    public GameObject enemyPrefab;
    // Distância mínima entre o jogador e a posição de spawn dos inimigos
    public float minSpawnDistance = 10f;
    private int enemiesAlive;

    [Header("Spawn Volume")]
    [SerializeField] private float VolumeX = 2f;
    [SerializeField] private float VolumeY = 2f;
    [SerializeField] private float VolumeZ = 2f;

    void Start()
    {
        currentWave = 0;
        //enemiesAlive = enemiesAtStart;
        //enemiesInWave = enemiesAtStart;
        //Debug.Log("Iniciando Wave " + currentWave);
        //GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        StartNextWave();
    }

    private void StartNextWave()
    {

        Debug.Log("Wave " + currentWave);
        if (currentWave > maxWaves)
        {
            SceneManager.LoadScene("Scenes/Win");
        }
        else 
        {
            Debug.Log("Inimigos vivos: " + enemiesAlive);
            currentWave++;
            Debug.Log("Iniciando Wave " + currentWave);
            enemiesInWave = currentWave + 2;
            //enemiesAlive = enemiesInWave;
            SpawnEnemies();
            
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
            StartNextWave();
        }
    }

    public Vector3 GetRandomNavMeshPoint()
    {
        // Gera um ponto aleatório dentro de um cubo (tenta 4 vezes)
        for (int i = 0; i < 4; i++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-VolumeX / 2, VolumeX / 2),
                Random.Range(-VolumeZ / 2, VolumeZ / 2),
                Random.Range(-VolumeY / 2, VolumeY / 2)
            );

            randomPoint += transform.position;
            NavMeshHit NavMeshhit;

            // Ponto mais proximo no navMesh
            if (NavMesh.SamplePosition(randomPoint, out NavMeshhit, Mathf.Max(VolumeX, VolumeZ, VolumeY), NavMesh.AllAreas))
            {
                // checa se o o ponto gerado está dentro de uma parede ???? porque unity????
                if (!Physics.Raycast(NavMeshhit.position, Vector2.up, out RaycastHit hit)) {
                    return NavMeshhit.position;
                }
                Debug.DrawLine(NavMeshhit.position, NavMeshhit.position + Vector3.up * 100f , Color.green, 1f);
            }
        }

        throw new RandonNavMeshPointException();
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(VolumeX, VolumeZ, VolumeY));

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerTransform.position, minSpawnDistance);
        }
    }
}
