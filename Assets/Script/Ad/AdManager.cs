using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    RewardedAd rewardAd;

    void Start()
    {
        // Google Mobile Ads SDK ÃÊ±âÈ­
        MobileAds.Initialize((InitializationStatus initiStatus) => { });

        // ±¤°í ÁØºñ
        LoadReward();
    }

    void LoadReward()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("±¤°í·Îµå ½ÇÆÐ!" + error);
                return;
            }

            rewardAd = ad;
            rewardAd.OnAdFullScreenContentClosed += () =>
            {
                rewardAd.Destroy();
                LoadReward();
            };
        });
    }

    void ShowRewardedAd(Action onReward)
    {
        if (rewardAd != null && rewardAd.CanShowAd())
            rewardAd.Show((Reward reward) => onReward?.Invoke());
        else
            Debug.Log("±¤°í ÁØºñ ¾ÈµÊ");
    }

    public void ShowDiggyAd() => ShowRewardedAd(() => SkillManager.instance.DiggySkillUp());
    public void ShowMagneticAd() => ShowRewardedAd(() => SkillManager.instance.MagneticSkillUp());
    public void ShowHasteAd() => ShowRewardedAd(() => SkillManager.instance.HasteSkillUp());
}
