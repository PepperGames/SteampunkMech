using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public UnityEvent OnBeginLoad;
    public UnityEvent OnSceneReady; // Событие, вызываемое когда сцена готова к активации

    private AsyncOperation sceneAsync; // Асинхронная операция загрузки сцены

    public void Load(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        OnBeginLoad.Invoke();
        // Начало загрузки сцены в фоне
        sceneAsync = SceneManager.LoadSceneAsync(sceneName);
        sceneAsync.allowSceneActivation = false; // Отключаем автоматическую активацию сцены

        // Ждем окончания загрузки сцены
        while (!sceneAsync.isDone)
        {
            if (sceneAsync.progress >= 0.9f) // Сцена загружена на 90%
            {
                // Сцена готова к активации
                OnSceneReady.Invoke();
                break;
            }
            yield return null;
        }
    }

    public void PlayScene()
    {
        if (sceneAsync != null)
        {
            sceneAsync.allowSceneActivation = true; // Активируем сцену
        }
    }
}