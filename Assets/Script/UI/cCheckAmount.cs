using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cCheckAmount : MonoBehaviour
{
    [SerializeField] int lessValue;
    Button btn;
    cPlayer player;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.interactable = false;
        player = GameManager.instance.player;
    }

    void LateUpdate()
    {
        if (player.FoodCount >= lessValue)
            btn.interactable = true;
        else
            btn.interactable = false;
    }
}
