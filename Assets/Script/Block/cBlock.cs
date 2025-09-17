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

    static GameObject foodParent;
    static GameObject particleParent;

    [SerializeField] Material groundMat;
    [SerializeField] Material surfaceMat;
    //Color surfaceColor = new Color(0.5f, 0.25f, 1f);
    //Color grounndColor = new Color(0.3f, 0.15f, 1f);

    void Awake()
    {
        if (!foodParent)
            foodParent = GameObject.FindWithTag("FoodParent");
        if (!particleParent)
            particleParent = GameObject.FindWithTag("ParticleParent");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == BlockState.GROUND)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            InstanteFood();

            InstanteParticle();

            OnDestroyed?.Invoke(this);
            RelaeseBlock();
        }
    }

    void RelaeseBlock()
    {
        // return & release
        state = BlockState.GROUND;
        BlockPoolManager.instance.Release(gameObject);
    }

    public void InstanteFood()
    {
        bool randValue = System.Convert.ToBoolean(Random.Range(0, 2));
        if (randValue == true)
        {
            GameObject insFood = FoodPoolManager.instance.Get();
            insFood.transform.SetParent(foodParent.transform);
            insFood.transform.position = transform.position;
        }
    }

    public void InstanteParticle()
    {
        GameObject particle = ParticlePoolManager.instance.Get();
        particle.transform.SetParent(particleParent.transform);
        particle.transform.position = transform.position;
    }

    public void ChangeMat()
    {
        MeshRenderer curMeshRenderer = GetComponent<MeshRenderer>();
        curMeshRenderer.material = surfaceMat;
    }
}
