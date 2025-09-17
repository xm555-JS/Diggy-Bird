using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BlockPoolManager : PoolManger
{
    public static BlockPoolManager instance;

    //void Start()
    //{
    //    objectPool = cBlockPool.pool;
    //    instance = this;
    //}

    void Awake()
    {
        objectPool = cBlockPool.pool;
        instance = this;
    }
}
