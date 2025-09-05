using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFood : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider collider;
    bool isEat = false;
    bool isVacuum = false;
    float time = 0f;
    public float power = 50f;

    public void setVaccum(bool value) { isVacuum = value; }

    void Start()
    {
        float rotate = Random.Range(0f, 359f);
        transform.Rotate(new Vector3(0f, rotate, 0f));

        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * power, ForceMode.Impulse);

        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            collider.enabled = true;

            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
                isEat = true;
        }
    }

    void FixedUpdate()
    {
        if (isVacuum)
        {
            Vector3 dir = (GameManager.instance.player.transform.position - transform.position).normalized;
            rigid.MovePosition(transform.position + dir * 10f * Time.deltaTime);
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
