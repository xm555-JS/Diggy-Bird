using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    RewardedAd rewardAd;

    void Start()
    {
        // Google Mobile Ads SDK 초기화
        MobileAds.Initialize((InitializationStatus initiStatus) => { });

        // 광고 준비
        Load();
    }

    void Load()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
                Debug.Log("광고로드 실패!");

            rewardAd = ad;
            rewardAd.OnAdFullScreenContentClosed += () =>
            {
                rewardAd.Destroy();
                Load();
            };
        });
    }

    public void ShowDiggyAd()
    {
        rewardAd.Show((Reward reward) => { SkillManager.instance.DiggySkillUp(); });
    }
    public void ShowMagneticAd()
    {
        rewardAd.Show((Reward reward) => { SkillManager.instance.MagneticSkillUp(); });
    }
    public void ShowHasteAd()
    {
        rewardAd.Show((Reward reward) => { SkillManager.instance.HasteSkillUp(); });
    }
}
