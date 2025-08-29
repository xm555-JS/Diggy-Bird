using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cPlayer : MonoBehaviour
{
    [SerializeField] FloatingJoystick joystick;

    //float speed = 5f;

    void Update()
    {
        Vector2 input = new Vector2(joystick.input.x, joystick.input.y); 
    }

    //void FixedUpdate()
    //{
        
    //}
}
