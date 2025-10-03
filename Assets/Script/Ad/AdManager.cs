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
        // Google Mobile Ads SDK �ʱ�ȭ
        MobileAds.Initialize((InitializationStatus initiStatus) => { });

        // ���� �غ�
        LoadReward();

        // 1�ð��� ������ �ٽ� Load
        InvokeRepeating("RefreshRewardAd", oneHour, oneHour);
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

            // ����â�� �ݾ��� ��
            rewardAd.OnAdFullScreenContentClosed += () =>
            {
                rewardAd.Destroy();
                LoadReward();
            };

            // ���� ������ ���� �ʾ��� ��
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
