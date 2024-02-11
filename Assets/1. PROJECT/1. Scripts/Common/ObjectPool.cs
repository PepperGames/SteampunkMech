using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 20;
    public Transform poolHolder;
    private Queue<GameObject> objectPool = new Queue<GameObject>();
    

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, poolHolder);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            obj.transform.parent = null; // Убираем из под родителя при активации
            return obj;
        }

        // Создаем новый объект, если пул пуст
        GameObject newObj = Instantiate(objectPrefab);
        newObj.SetActive(true);
        newObj.transform.parent = null;
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = poolHolder; // Возвращаем в холдер
        objectPool.Enqueue(obj);
    }
}
