using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IPauseListener
{
    public void StopThisObject();
    public void ResumeThisObject();
}

public class PauseManager : MonoBehaviour
{
    private IEnumerable<IPauseListener> pauseListeners;
    
    private void UpdateListeners()
    {
        pauseListeners = FindObjectsOfType<MonoBehaviour>().OfType<IPauseListener>();
    }

    public void CallGamePause()
    {
        UpdateListeners();
        
        foreach (var listener in pauseListeners)
        {
            listener.StopThisObject(); // Вызываем метод остановки для каждого слушателя
        }
    }

    public void ResumeGame()
    {
        UpdateListeners();
        
        foreach (var listener in pauseListeners)
        {
            listener.ResumeThisObject(); // Вызываем метод возобновления для каждого слушателя
        }
    }
}
