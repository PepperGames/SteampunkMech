using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteRoom : MonoBehaviour
{
    
    public void InitializeRoom()
    {
        Debug.Log("Initialize Room on: " + gameObject.name);
        SpawnPlayer();
        SpawnEnemies();
        SpawnResources();
    }

    public void SpawnPlayer()
    {
        Debug.Log("SpawnPlayer");
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
