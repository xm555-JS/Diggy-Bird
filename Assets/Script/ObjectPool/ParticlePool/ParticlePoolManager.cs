using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlePoolManager : PoolManger
{
    public static ParticlePoolManager instance;

    void Start()
    {
        objectPool = cParticlePool.pool;
        instance = this;
    }
}
