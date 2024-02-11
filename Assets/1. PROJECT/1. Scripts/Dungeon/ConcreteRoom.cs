using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteRoom : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    [Header("Enemies")]
    public Transform[] spawnPositions;
    public BoxCollider[] enemySpawnZones;
    public GameObject[] enemyPrefabs;
    public float minimumDistanceBetweenEnemies;
    [Header("Resources")]
    public GameObject[] resourcePrefabs; // Массив всех возможных префабов ресурсов
    public BoxCollider[] resourceSpawnZones; // Массив зон спавна ресурсов
    public int resourcesToSpawnCount; // Количество ресурсов, которое нужно спавнить
    public float minimumDistanceBetweenResources; // Минимальное расстояние между ресурсами

    public List<GameObject> currentAliveEnemies;

    private List<Vector3> spawnedEnemyPositions;
    private List<Vector3> spawnedResourcePositions;

    private void Awake()
    {
        spawnedEnemyPositions = new List<Vector3>();
        currentAliveEnemies = new List<GameObject>();
        spawnedResourcePositions = new List<Vector3>();
    }

    
    public void InitializeRoom()
    {
        Debug.Log("Initialize Room on: " + gameObject.name);
        SpawnPlayer();
        SpawnEnemies();
        SpawnResources();
    }

    public void SpawnPlayer()
    {
        if (spawnPositions == null || spawnPositions.Length == 0)
        {
            Debug.LogError("Spawn positions are not set for " + gameObject.name);
            return;
        }

        // Выбираем случайную позицию из доступных
        int randomIndex = UnityEngine.Random.Range(0, spawnPositions.Length);
        Transform spawnPoint = spawnPositions[randomIndex];

        // Перемещаем игрока на выбранную позицию
        if (player != null)
        {
            player.transform.position = spawnPoint.position;
        }
        else
        {
            Debug.LogError("Player object not found");
        }
    }
    
    private void SpawnObjects(GameObject[] prefabs, BoxCollider[] spawnZones, List<Vector3> spawnedPositions, float minimumDistance, int countToSpawn)
    {
        for (int i = 0; i < countToSpawn; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(spawnZones, spawnedPositions, minimumDistance);
            if (spawnPosition != Vector3.zero) // Проверяем, что мы нашли действительное место для спавна
            {
                GameObject selectedPrefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
                Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
                spawnedPositions.Add(spawnPosition);
            }
            else
            {
                Debug.LogError("Failed to find a position for spawning that satisfies minimum distance requirement.");
            }
        }
    }

    private Vector3 GetSpawnPosition(BoxCollider[] spawnZones, List<Vector3> spawnedPositions, float minimumDistance)
    {
        Vector3 spawnPosition = Vector3.zero;
        bool positionFound = false;

        int maxAttempts = 10;
        while (!positionFound && maxAttempts > 0)
        {
            maxAttempts--;
            BoxCollider spawnZone = spawnZones[UnityEngine.Random.Range(0, spawnZones.Length)];
            Vector3 randomPoint = new Vector3(
                UnityEngine.Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
                UnityEngine.Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y),
                UnityEngine.Random.Range(spawnZone.bounds.min.z, spawnZone.bounds.max.z)
            );

            if (IsPositionValid(randomPoint, spawnedPositions, minimumDistance))
            {
                spawnPosition = randomPoint;
                positionFound = true;
            }
        }
        return positionFound ? spawnPosition : Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position, List<Vector3> spawnedPositions, float minimumDistance)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minimumDistance)
            {
                return false;
            }
        }
        return true;
    }

    public void SpawnEnemies()
    {
        spawnedEnemyPositions.Clear(); // Очищаем позиции перед новым спавном
        SpawnObjects(enemyPrefabs, enemySpawnZones, spawnedEnemyPositions, minimumDistanceBetweenEnemies, enemyPrefabs.Length);
    }

    public void SpawnResources()
    {
        spawnedResourcePositions.Clear(); // Очищаем позиции перед новым спавном
        SpawnObjects(resourcePrefabs, resourceSpawnZones, spawnedResourcePositions, minimumDistanceBetweenResources, resourcesToSpawnCount);
    }
}
