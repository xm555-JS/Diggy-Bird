using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cFood : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    bool isEat = false;
    bool isVacuum = false;
    float time = 0f;
    public float power = 50f;

    public void setVaccum(bool value) { isVacuum = value; }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnEnable()
    {
        float rotate = Random.Range(0f, 359f);
        transform.Rotate(new Vector3(0f, rotate, 0f));

        rigid.AddForce(Vector3.up * power, ForceMode.Impulse);
        boxCollider.enabled = false;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.5f)
        {
            boxCollider.enabled = true;

            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            if (Physics.Raycast(transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
                isEat = true;
        }
        if (time > 30f)
            ResetFood();
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
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.EAT, 0.3f);

            GameManager.instance.player.FoodCount = 1;
            ResetFood();
        }
    }

    void ResetFood()
    {
        isVacuum = false;
        isEat = false;
        time = 0f;
        cFoodPool.pool.Release(gameObject);
    }
}
