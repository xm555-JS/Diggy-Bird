using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayer : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] GameObject area;
    Rigidbody rigid;
    Animator anim;
    Vector2 input;
    //int foodCount = 0;
    float speed = 5f;
    float fallingSpeed = 0.2f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        input = new Vector2(-joystick.input.x, -joystick.input.y);
        Vector3 forward = new Vector3(input.x, 0f, input.y);
        //transform.forward = Vector3.Lerp(transform.forward, forward, Time.deltaTime * 3f);
        if (forward != Vector3.zero)
            transform.forward = forward;
        // 애니메이션 전환
        anim.SetFloat("isWalk", input.magnitude);
        anim.SetFloat("isFly", rigid.velocity.y);
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(input.normalized.x, 0f, input.normalized.y);

        if (!Physics.Raycast(transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground")))
            dir.y = rigid.velocity.y * fallingSpeed;
        else
            dir.y = 0f;

        rigid.velocity = dir * speed;
    }

    public void OnAreaCollider()
    {
        SphereCollider collider = area.GetComponent<SphereCollider>();
        collider.enabled = true;
    }

    public void OffAreaCollider()
    {
        SphereCollider collider = area.GetComponent<SphereCollider>();
        collider.enabled = false;
    }

    public void SpeedUp()
    {
        speed = 10f;
    }

    public void ReturnSpeed()
    {
        speed = 5f;
    }
}
