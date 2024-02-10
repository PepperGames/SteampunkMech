using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public List<RoomPreset> roomPresets;
    
    public GameObject GetRandomRoomPrefab()
    {
        float totalChance = roomPresets.Sum(preset => preset.chance);
        float randomPoint = Random.value * totalChance;
        
        foreach (var preset in roomPresets)
        {
            if (randomPoint < preset.chance)
                return preset.roomPrefab;
            else
                randomPoint -= preset.chance;
        }
        return null; // На случай, если что-то пойдет не так
    }
}

