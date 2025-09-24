using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0414

public class cPlayer : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] GameObject area;
    Rigidbody rigid;
    Animator anim;
    Vector2 input;
    int foodCount = 0;
    float speed = 5f;
    float fallingSpeed = 0.5f;

    public int FoodCount { get => foodCount; set => foodCount += value; }

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.freezeRotation = true;

        anim = GetComponent<Animator>();

        foodCount = 9000;
    }

    void Update()
    {
        input = new Vector2(-joystick.input.x, -joystick.input.y);
        Vector3 forward = new Vector3(input.x, 0f, input.y);
        if (forward != Vector3.zero)
            transform.forward = forward;
        // 애니메이션 전환
        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.fullPathHash == Animator.StringToHash("Spin") ||
            animInfo.fullPathHash == Animator.StringToHash("Roll"))
            return;

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
        anim.SetBool("isRoll", true);
        speed = 10f;
    }

    public void ReturnSpeed()
    {
        anim.SetBool("isRoll", false);
        speed = 5f;
    }

    public void StartSpin()
    {
        anim.SetTrigger("isSpin");
    }
}
