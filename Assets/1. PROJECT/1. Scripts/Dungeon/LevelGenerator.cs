using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<Floor> floors; // Список всех этажей с пресетами
    public GameObject startRoomPrefab;
    public GameObject bossRoomPrefab;
    
    private ConcreteRoom currentRoomPrefab;
    private int currentFloor = 0;

    private void Start()
    {
        OpenNextRoom();
    }

    public void OpenNextRoom()
    {
        if (currentRoomPrefab != null)
        {
            Destroy(currentRoomPrefab.gameObject); // Удаляем предыдущую комнату
        }

        currentFloor++; // Переходим к следующему этажу

        GameObject roomPrefab;

        if (currentFloor == 1)
        {
            roomPrefab = startRoomPrefab; // Если это первый этаж, то стартовая комната
        }
        else if (currentFloor == floors.Count + 2) // Проверяем, является ли этот этаж босс комнатой
        {
            roomPrefab = bossRoomPrefab; // Если да, то босс комната
        }
        else
        {
            // Выбираем случайную комнату для обычного этажа, учитывая, что первый и последний этажи уже заняты
            roomPrefab = floors[currentFloor - 2].GetRandomRoomPrefab();
        }
    
        GameObject spawnedRoom = Instantiate(roomPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        if (spawnedRoom != null)
        {
            currentRoomPrefab = spawnedRoom.GetComponent<ConcreteRoom>();
            currentRoomPrefab.InitializeRoom();
        }
    }


    // Для сохранения текущей комнаты в формате floor-N, roomPreset;
    private string GetCurrentRoomInfo()
    {
        return $"floor-{currentFloor}, {currentRoomPrefab.gameObject.name};";
    }
}

