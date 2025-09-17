using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCamera : MonoBehaviour
{
    cPlayer player;
    float baseY;

    void Start()
    {
        player = GameManager.instance.player;
        baseY = transform.position.y - player.transform.position.y;
    }

    void FixedUpdate()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        float gapY = transform.position.y - playerPos.y;
        if (baseY < gapY)
        {
            float y = Mathf.Lerp(transform.position.y, playerPos.y + baseY, Time.deltaTime * 3f);
            transform.position = new Vector3(playerPos.x, y, transform.position.z);
        }
        else
            transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);
    }
}
