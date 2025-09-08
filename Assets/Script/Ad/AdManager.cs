using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    void Start()
    {
        // Google Mobile Ads SDK �ʱ�ȭ
        MobileAds.Initialize((InitializationStatus initiStatus) => { });
        //MobileAds.Initialize(initStatus => { });


        // �̺�Ʈ ����
        // ���� ��ü ȭ�� ��þ���� �ݾ��� ��
        // ������ ���� �̸� �ε�
    }

    public void LoadAd()
    {
        var adRequest = new AdRequest();

        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
                Debug.Log("����ε� ����!");
            Debug.Log("���� �ε� ����!");
        });
    }    

    void OnDestroy()
    {
        // RewardedAd ����
    }
}
