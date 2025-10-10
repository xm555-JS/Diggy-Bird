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
        // Google Mobile Ads SKD 초기화
        MobileAds.Initialize((InitializationStatus status) => { });

        // 광고 Load
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
                // Load 실패 시 다시 로드(단 횟수 제한)
                Debug.Log("광고로드 실패!");
                LoadRewardForFailed();
                return;
            }

            ad.OnAdFullScreenContentClosed += () =>
            {
                // 광고가 닫힐 때
                ad.Destroy();
                LoadReward();
            };

            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                // 광고 실행이 실패했을 때
                Debug.Log("광고 실행 실패");
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
    //    // Google Mobile Ads SDK 초기화
    //    MobileAds.Initialize((InitializationStatus initiStatus) => { });

    //    // 광고 준비
    //    LoadReward();

    //    // 1시간이 지나면 다시 Load
    //    InvokeRepeating("RefreshRewardAd", oneHour, oneHour);
    //}

    //void LoadReward()
    //{
    //    var adRequest = new AdRequest();
    //    RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
    //    {
    //        if (error != null || ad == null)
    //        {
    //            Debug.Log("광고로드 실패!" + error);
    //            // 다시로드하는 함수 필요
    //            return;
    //        }

    //        rewardAd = ad;

    //        // 광고창을 닫았을 때
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
