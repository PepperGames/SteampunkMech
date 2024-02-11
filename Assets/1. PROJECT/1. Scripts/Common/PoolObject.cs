using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolObject : MonoBehaviour
{
    public ObjectPool myPool;
    public float autoReturnTime;
    
    public void Initialize(ObjectPool _pool)
    {
        myPool = _pool;
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(autoReturnTime);
        ReturnToPoolImmediate();
    }

    public void ReturnToPoolImmediate()
    {
        myPool.ReturnToPool(this.gameObject);
    }
}
