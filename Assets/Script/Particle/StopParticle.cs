using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopParticle : MonoBehaviour
{
    void OnParticleSystemStopped()
    {
        ParticlePoolManager.instance.Release(gameObject);
    }
}
