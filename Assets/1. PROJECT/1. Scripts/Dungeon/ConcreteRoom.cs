using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteRoom : MonoBehaviour
{
    public Transform player;
    public Transform[] spawnPositions;

    
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
    
    public void SpawnEnemies()
    {
        Debug.Log("SpawnEnemies");
    }
    
    public void SpawnResources()
    {
        Debug.Log("SpawnResources");
    }
}
