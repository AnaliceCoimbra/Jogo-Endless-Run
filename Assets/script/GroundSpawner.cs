using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public Transform player;
    public int tilesOnScreen = 4;
    public float tileLength = 20f;
    public float spawnZ = 0f;
    private float safeZone = 2f;

    private List<GameObject> activeTiles = new List<GameObject>();

    [Header("Obstáculos")]
    public GameObject[] obstaclePrefabs;
    [Range(0, 1f)]
    public float initialObstacleSpawnChance = 0.4f;
    private float obstacleSpawnChance;
    public float spawnChanceIncreaseRate = 0.02f;
    public float maxSpawnChance = 0.95f;

    private float timeElapsed = 0f;

    void Start()
    {
        obstacleSpawnChance = initialObstacleSpawnChance;

        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        obstacleSpawnChance = Mathf.Min(
            initialObstacleSpawnChance + timeElapsed * spawnChanceIncreaseRate,
            maxSpawnChance
        );

        if (activeTiles.Count > 0)
        {
            GameObject oldestTile = activeTiles[0];
            float oldestTileEnd = oldestTile.transform.position.z + tileLength;

            if (player.position.z > oldestTileEnd + safeZone)
            {
                DeleteTile();
                SpawnTile();
            }
        }
    }

    void SpawnTile()
    {
        GameObject tile = Instantiate(groundPrefab, Vector3.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(tile);

        SpawnObstaclesOnTile(tile);

        spawnZ += tileLength;
    }

    void SpawnObstaclesOnTile(GameObject tile)
    {
        // Eu alterei esse script para colocar o outro
    }

    void DeleteTile()
    {
        if (activeTiles.Count > 0)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}
