using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    Queue<RewardedAd> rewardPool = new Queue<RewardedAd>();

    const float oneHour = 3600f;
    const int defaultPoolCount = 3;

    int loadCount = 0;
    int maxLoadCount = 5;

    void Start()
    {
        // Google Mobile Ads SKD �ʱ�ȭ
        MobileAds.Initialize((InitializationStatus status) => { });

        // ���� Load
        for (int i = 0; i < 3; i++)
            LoadReward();

        InvokeRepeating(nameof(RefreshRewardAd), oneHour, oneHour);
    }

    void LoadReward()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                // Load ���� �� �ٽ� �ε�(�� Ƚ�� ����)
                Debug.Log("����ε� ����!");
                LoadRewardForFailed();
                return;
            }

            ad.OnAdFullScreenContentClosed += () =>
            {
                // ���� ���� ��
                ad.Destroy();
                LoadReward();
            };

            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                // ���� ������ �������� ��
                Debug.Log("���� ���� ����");
                ad.Destroy();
                LoadReward();
            };

            rewardPool.Enqueue(ad);
            loadCount = 0;
        });
    }

    void LoadRewardForFailed()
    {
        loadCount++;
        if (loadCount <= maxLoadCount)
            LoadReward();
    }

    void RefreshRewardAd()
    {
        foreach (var rewardAd in rewardPool)
            rewardAd?.Destroy();

        rewardPool.Clear();

        for (int i = 0; i < 3; i++)
            LoadReward();
    }

    void ShowRewardedAd(Action onReward)
    {
        if (rewardPool.Count > 0)
        {
            var rewardAd = rewardPool.Dequeue();
            if (rewardAd != null && rewardAd.CanShowAd())
                rewardAd.Show((Reward reward) => onReward?.Invoke());
            else
                LoadReward();
        }
        else
        {
            for (int i = 0; i < 3; i++)
                LoadReward();
        }
    }

    void MaintainPool()
    {
        while (rewardPool.Count < defaultPoolCount)
            LoadReward();
    }

    public void ShowDiggyAd() => ShowRewardedAd(() => SkillManager.instance.DiggySkillUp());
    public void ShowMagneticAd() => ShowRewardedAd(() => SkillManager.instance.MagneticSkillUp());
    public void ShowHasteAd() => ShowRewardedAd(() => SkillManager.instance.HasteSkillUp());

    //void Start()
    //{
    //    // Google Mobile Ads SDK �ʱ�ȭ
    //    MobileAds.Initialize((InitializationStatus initiStatus) => { });

    //    // ���� �غ�
    //    LoadReward();

    //    // 1�ð��� ������ �ٽ� Load
    //    InvokeRepeating("RefreshRewardAd", oneHour, oneHour);
    //}

    //void LoadReward()
    //{
    //    var adRequest = new AdRequest();
    //    RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
    //    {
    //        if (error != null || ad == null)
    //        {
    //            Debug.Log("����ε� ����!" + error);
    //            // �ٽ÷ε��ϴ� �Լ� �ʿ�
    //            return;
    //        }

    //        rewardAd = ad;

    //        // ����â�� �ݾ��� ��
    //        rewardAd.OnAdFullScreenContentClosed += () =>
    //        {
    //            rewardAd.Destroy();
    //            LoadReward();
    //        };
    //    });
    //}

    //void ShowRewardedAd(Action onReward)
    //{
    //    if (rewardAd != null && rewardAd.CanShowAd())
    //        rewardAd.Show((Reward reward) => onReward?.Invoke());
    //    else
    //        LoadReward();
    //}

    //void RefreshRewardAd()
    //{
    //    if (rewardAd != null)
    //    {
    //        rewardAd.Destroy();
    //        LoadReward();
    //    }
    //}
}
