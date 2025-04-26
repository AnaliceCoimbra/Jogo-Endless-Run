using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // agora é um array de obstáculos
    public Transform player;
    public float spawnDistance = 30f;
    public float minDelay = 1.5f;
    public float maxDelay = 2.5f;
    public float minSpacing = 10f; // distância mínima entre os obstáculos

    private float lastSpawnZ = 0f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Espera um tempo aleatório entre obstáculos
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            // Garante espaçamento suficiente (opcional para evitar bug)
            if (player.position.z + spawnDistance - lastSpawnZ >= minSpacing)
            {
                SpawnObstacle();
            }
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0) return;

        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject obstacle = Instantiate(prefab);

        float spawnX = 0f; // Fixo no centro se quiser
        float spawnZ = player.position.z + 30f; // Exemplo: spawnar 30 unidades na frente do jogador
        float spawnY = prefab.transform.localPosition.y; // Altura padrão do prefab

        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

        obstacle.transform.position = spawnPos;
    }


}