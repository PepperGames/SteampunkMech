using UnityEngine;
using UnityEngine.Events;

public class ColliderEventTrigger : MonoBehaviour
{
    public string targetTag; // Тэг объектов, на которые будет реагировать коллайдер
    public UnityEvent onTargetEnter; // Событие при входе объекта в коллайдер
    public UnityEvent onTargetExit;  // Событие при выходе объекта из коллайдера
    public UnityEvent onTargetStay;  // Событие, когда объект находится в коллайдере

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            onTargetEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            onTargetExit.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            onTargetStay.Invoke();
        }
    }
}



