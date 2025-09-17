using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class cCooltime : MonoBehaviour
{
    Button btn;
    [SerializeField] Image cooltimeImg;
    [SerializeField] float cooltime;
    [SerializeField] UnityEvent onCooltimeEnd;

    void Start()
    {
        cooltimeImg.raycastTarget = false;

        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => Cooltime());
    }

    void Cooltime()
    {
        StartCoroutine(SetFillAmount());
    }

    IEnumerator SetFillAmount()
    {
        cooltimeImg.raycastTarget = true;

        cooltimeImg.fillAmount = 1f;

        float time = cooltime;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            cooltimeImg.fillAmount = time / 5f;
            yield return null;
        }

        onCooltimeEnd?.Invoke();
        cooltimeImg.raycastTarget = false;
    }
}
