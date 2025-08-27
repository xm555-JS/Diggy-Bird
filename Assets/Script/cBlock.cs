using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState { GROUND, SURFACE }

public class cBlock : MonoBehaviour
{
    public BlockState state;
    public event System.Action<cBlock> OnDestroyed;

    void OnCollisionEnter(Collision collision)
    {
        if (state == BlockState.GROUND)
            return;

        if (collision.collider.CompareTag("Player"))
        {
            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
