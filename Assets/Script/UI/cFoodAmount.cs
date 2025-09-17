using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cFoodAmount : MonoBehaviour
{
    Text txt;
    cPlayer player;

    void Start()
    {
        txt = GetComponent<Text>();
        player = GameManager.instance.player;
    }

    void LateUpdate()
    {
        txt.text = "x" + player.FoodCount.ToString();
    }
}
