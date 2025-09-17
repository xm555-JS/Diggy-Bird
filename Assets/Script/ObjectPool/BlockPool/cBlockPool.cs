using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class cBlockPool : PoolObject
{
    public static ObjectPool<GameObject> pool;
    void Awake()
    {
        defaultCapacity = 2300;
        maxSize = 10000;
        pool = new ObjectPool<GameObject>(CreatePool,
                            SetActiveOn,
                            SetActiveOff,
                            Destroy,
                            collectionChecks,
                            defaultCapacity,
                            maxSize);
    }
}