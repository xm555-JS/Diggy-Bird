using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FoodPoolManager : PoolManger
{
    public static FoodPoolManager instance;

    void Start()
    {
        objectPool = cFoodPool.pool;
        instance = this;
    }
}
