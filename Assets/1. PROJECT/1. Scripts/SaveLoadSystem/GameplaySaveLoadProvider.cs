using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;

using System.Collections;
using UnityEngine;

public class GameplaySaveLoadProvider : MonoBehaviour, ISaveComponent
{
    public int saveComponentId;
    public int saveInteger = 3;
    public int loadedInteger = 0;

    // Start is called before the first frame update

    public void SaveThisObject()
    {
        StartCoroutine(SaveLoadManager.SaveGameCoroutine(OnSaveProgress));
    }

    public void LoadToThisObject()
    {
        StartCoroutine(SaveLoadManager.LoadGameCoroutine(OnLoadProgress));
    }

    private void OnSaveProgress(float progress)
    {
        // Обработка прогресса сохранения (например, обновление UI)
        Debug.Log("Save progress: " + progress);
    }

    private void OnLoadProgress(float progress)
    {
        // Обработка прогресса загрузки (например, обновление UI)
        Debug.Log("Load progress: " + progress);
    }

    public int GetSaveComponentID()
    {
        return saveComponentId;
    }

    public string SaveData()
    {
        return saveInteger.ToString();
    }

    public void LoadData(string data)
    {
        if (int.TryParse(data, out int result))
        {
            loadedInteger = result;
        }
        else
        {
            Debug.LogError("Failed to parse loaded data");
        }
    }
}

