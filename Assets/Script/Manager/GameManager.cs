using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public cPlayer player;

    void Awake()
    {
        Application.targetFrameRate = 60;

        instance = this;

        AudioManager.instance.StartBgm();
    }
}
