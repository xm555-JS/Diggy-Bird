using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFood : MonoBehaviour
{
    Rigidbody rigid;
    bool isEat = false;
    float time = 0f;
    public float power = 50f;

    void Start()
    {
        float rotate = Random.Range(0f, 359f);
        transform.Rotate(new Vector3(0f, rotate, 0f));

        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * power, ForceMode.Impulse);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
                isEat = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isEat)
        {
            Destroy(gameObject);
        }
    }
}
