using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cMagnatic : MonoBehaviour
{
    Button btn;
    [SerializeField] Image cooltime;

    void Start()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() => GameManager.instance.player.OnAreaCollider());
        btn.onClick.AddListener(() => Cooltime());
    }

    void Cooltime()
    {
        // 몇 초 뒤에 OffAreaCollider
        StartCoroutine(SetFillAmount());
    }

    IEnumerator SetFillAmount()
    {
        cooltime.fillAmount = 1f;

        float time = 5f;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            cooltime.fillAmount = time / 5f;
            yield return null;
        }

        GameManager.instance.player.OffAreaCollider();
    }
}
