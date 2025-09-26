using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManger : MonoBehaviour
{
    protected IObjectPool<GameObject> objectPool = null;

    public GameObject Get()
    {
        return objectPool.Get();
    }

    public void Release(GameObject obj)
    {
        objectPool.Release(obj);
    }
}
