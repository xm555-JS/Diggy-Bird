using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    void Start()
    {
        // Google Mobile Ads SDK 초기화
        MobileAds.Initialize((InitializationStatus initiStatus) => { });
        //MobileAds.Initialize(initStatus => { });


        // 이벤트 설정
        // 광고가 전체 화면 콘첸츠를 닫았을 때
        // 보상형 광고 미리 로드
    }

    public void LoadAd()
    {
        var adRequest = new AdRequest();

        RewardedAd.Load("ca-app-pub-3940256099942544/5224354917", adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
                Debug.Log("광고로드 실패!");
            Debug.Log("광고 로드 성공!");
        });
    }    

    void OnDestroy()
    {
        // RewardedAd 삭제
    }
}
