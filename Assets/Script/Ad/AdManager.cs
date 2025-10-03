using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    RewardedAd rewardAd;
    Action intervalRewardAction;

    const float oneHour = 3600f;

    void Start()
    {
        // Google Mobile Ads SDK 초기화
        MobileAds.Initialize((InitializationStatus initiStatus) => { });

        // 광고 준비
        LoadReward();

        // 1시간이 지나면 다시 Load
        InvokeRepeating("RefreshRewardAd", oneHour, oneHour);
    }

    void LoadReward()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("광고로드 실패!" + error);
                return;
            }

            rewardAd = ad;

            // 광고창을 닫았을 때
            rewardAd.OnAdFullScreenContentClosed += () =>
            {
                rewardAd.Destroy();
                LoadReward();
            };

            // 광고 실행이 되지 않았을 때
            if (intervalRewardAction != null)
            {
                rewardAd.Show((Reward reward) => intervalRewardAction?.Invoke());
                intervalRewardAction = null;
            }
        });
    }

    void ShowRewardedAd(Action onReward)
    {
        if (rewardAd != null && rewardAd.CanShowAd())
            rewardAd.Show((Reward reward) => onReward?.Invoke());
        else
        {
            intervalRewardAction = onReward;
            LoadReward();
        }
    }

    void RefreshRewardAd()
    {
        if (rewardAd != null)
        {
            rewardAd.Destroy();
            LoadReward();
        }
    }

    public void ShowDiggyAd() => ShowRewardedAd(() => SkillManager.instance.DiggySkillUp());
    public void ShowMagneticAd() => ShowRewardedAd(() => SkillManager.instance.MagneticSkillUp());
    public void ShowHasteAd() => ShowRewardedAd(() => SkillManager.instance.HasteSkillUp());
}
