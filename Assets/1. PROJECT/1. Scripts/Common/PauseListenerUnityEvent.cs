using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseListenerUnityEvent : MonoBehaviour, IPauseListener
{
    public UnityEvent OnObjectPaused;
    public UnityEvent OnObjectResumed;

    public void StopThisObject()
    {
        OnObjectPaused.Invoke();
    }

    public void ResumeThisObject()
    {
        OnObjectResumed.Invoke();
    }
}
