using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    RewardedAd rewardAd;

    void Start()
    {
        // Google Mobile Ads SDK �ʱ�ȭ
        MobileAds.Initialize((InitializationStatus initiStatus) => { });

        // ���� �غ�
        LoadReward();
    }

    void LoadReward()
    {
        var adRequest = new AdRequest();
        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("����ε� ����!" + error);
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
            Debug.Log("���� �غ� �ȵ�");
    }

    public void ShowDiggyAd() => ShowRewardedAd(() => SkillManager.instance.DiggySkillUp());
    public void ShowMagneticAd() => ShowRewardedAd(() => SkillManager.instance.MagneticSkillUp());
    public void ShowHasteAd() => ShowRewardedAd(() => SkillManager.instance.HasteSkillUp());
}
