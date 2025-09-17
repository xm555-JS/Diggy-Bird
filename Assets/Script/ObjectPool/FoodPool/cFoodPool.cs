using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class cFoodPool : PoolObject
{
    public static ObjectPool<GameObject> pool;

    void Awake()
    {
        defaultCapacity = 300;
        maxSize = 1000;
        pool = new ObjectPool<GameObject>(CreatePool,
                    SetActiveOn,
                    SetActiveOff,
                    Destroy,
                    collectionChecks,
                    defaultCapacity,
                    maxSize);
    }
}
