using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolObject : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    protected int defaultCapacity;
    protected int maxSize;
    protected bool collectionChecks = false;

    protected GameObject CreatePool()
    {
        GameObject poolObject = Instantiate(prefab);
        return poolObject;
    }

    protected void SetActiveOn(GameObject obj)
    {
        obj.SetActive(true);
    }

    protected void SetActiveOff(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected void Destroy(GameObject poolObject)
    {
        Destroy(poolObject);
    }
}
