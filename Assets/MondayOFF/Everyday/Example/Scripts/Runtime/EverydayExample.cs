using UnityEngine;
using MondayOFF;

public class EverydayExample : MonoBehaviour {
    [SerializeField] GameObject _bigBadCanvas = default;
    [SerializeField] Transform _quad = default;
    [SerializeField] Transform _landscape = default;


    private void Start() {
        AdsManager.OnRewardedAdLoaded -= OnRewarededReady;
        AdsManager.OnRewardedAdLoaded += OnRewarededReady;

        AdsManager.OnBeforeInterstitial -= ShowBadCanvas;
        AdsManager.OnBeforeInterstitial += ShowBadCanvas;

        AdsManager.OnAfterInterstitial -= HideBadCanvas;
        AdsManager.OnAfterInterstitial += HideBadCanvas;


        AdsManager.OnBeforeRewarded -= ShowBadCanvas;
        AdsManager.OnBeforeRewarded += ShowBadCanvas;

        AdsManager.OnAfterRewarded -= HideBadCanvas;
        AdsManager.OnAfterRewarded += HideBadCanvas;

        Adverty.AdvertySettings.SetMainCamera(Camera.main);
    }

    private void Update() {
        _quad.rotation = Quaternion.Euler(0f, Mathf.Sin(Time.timeSinceLevelLoad) * 60f, 0f);
        _landscape.rotation = Quaternion.Euler(0f, Mathf.Sin(Time.timeSinceLevelLoad) * 60f, 0f);
    }

    private void ShowBadCanvas() {
        Debug.Log("[EVERYDAY] SHOW LOADING CANVAS");
        _bigBadCanvas.SetActive(true);
    }

    private void HideBadCanvas() {
        Debug.Log("[EVERYDAY] HIDE LOADING CANVAS");
        _bigBadCanvas.SetActive(false);
    }

    private void OnRewarededReady() {
        Debug.Log("[EVERYDAY] REWARED IS READY");
    }

    public void Ads_InitializeAdsManager() {
        AdsManager.Initialize();
    }

    public void Ads_ShowIS() {
        AdsManager.ShowInterstitial();
    }

    public void Ads_ShowRV() {
        AdsManager.ShowRewarded(() => Debug.Log("-- Your Reward --"));
    }

    public void Ads_ShowBN() {
        AdsManager.ShowBanner();
    }

    public void Ads_HideBN() {
        AdsManager.HideBanner();
    }

    public void Ads_ShowPlayOn() {
        AdsManager.ShowPlayOn();
    }

    public void Ads_HidePlayOn() {
        AdsManager.HidePlayOn();
    }

    public void Ads_DisableIS() {
        // Both method works. DisableAdType(AdType) is useful when disabling multiple ad types.
        // AdsManager.DisableIS();
        AdsManager.DisableAdType(AdType.Interstitial);
    }

    public void Ads_DisableRV() {
        // AdsManager.DisableRV();
        AdsManager.DisableAdType(AdType.Rewarded);
    }

    public void Ads_DisableBN() {
        // AdsManager.DisableBN();
        AdsManager.DisableAdType(AdType.Banner);
    }

    public void Events_TryStage(int stageNum) {
        EventTracker.TryStage(stageNum);
    }

    public void Events_ClearStage(int stageNum) {
        EventTracker.ClearStage(stageNum);
    }
}
