using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState { GROUND, SURFACE }

public class cBlock : MonoBehaviour
{
    public BlockState state;
    public event System.Action<cBlock> OnDestroyed;

    [SerializeField] GameObject food;
    [SerializeField] ParticleSystem popParticle;

    void OnCollisionEnter(Collision collision)
    {
        if (state == BlockState.GROUND)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            InstanteFood();

            InstanteParticle();

            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void InstanteFood()
    {
        GameObject insFood = Instantiate(food);
        insFood.transform.position = transform.position;
    }

    public void InstanteParticle()
    {
        ParticleSystem particle = Instantiate(popParticle);
        particle.transform.position = transform.position;
    }
}
