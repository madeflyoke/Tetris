using UnityEngine.Advertisements;
using UnityEngine;
using System.Collections.Generic;

public class AdsController : IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private const string androidGameId = "4726421";
    private const string iOSGameId = "4726420";

    private const string androidInterstitialAdId = "Interstitial_Android";
    private const string iOSInterstitialAdId = "Interstitial_iOS";
    private const string androidRewardedAdId = "Rewarded_Android";
    private const string iOSRewardedAdId = "Rewarded_iOS";
    private const string androidBannerAdId = "Banner_Android";
    private const string iOSBannerAdId = "Banner_iOS";

    public enum AdType
    {
        Interstitial,
        Rewarded,
        Banner
    }

    private bool testMode = false;
    private string gameId;
    private Dictionary<AdType, string> adTypeAdId;

    #region Initialization
    public void Initialize()
    {
        adTypeAdId = new Dictionary<AdType, string>();
#if UNITY_ANDROID
        adTypeAdId[AdType.Interstitial] = androidInterstitialAdId;
        adTypeAdId[AdType.Rewarded] = androidRewardedAdId;
        adTypeAdId[AdType.Banner] = androidBannerAdId;
        gameId = androidGameId;
        Advertisement.Banner.SetPosition(BannerPosition.TOP_LEFT);
#elif UNITY_IOS
        adTypeInAdId[AdType.Interstitial] = iOSInterstitialAdId;
        adTypeInAdId[AdType.Rewarded] = iOSRewardedAdId;
        adTypeInAdId[AdType.Banner] = iOSBannerAdId;
        Advertisement.Banner.SetPosition(BannerPosition.TOP_LEFT);
        gameId = iOSGameId;
#endif     
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads Initialization is complete");
        LoadAd(AdType.Interstitial);
        LoadAd(AdType.Rewarded);
        LoadAd(AdType.Banner);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads Initialization is failed: ERROR - " + error + " MESSAGE - " + message);
    }
    #endregion

    #region Ads Load

    private void LoadAd(AdType adType)
    {
        if (adTypeAdId.ContainsKey(adType) == false)
        {
            Debug.LogWarning("Load Ad AdType parameter is invalid");
            return;
        }
        LoadAd(adTypeAdId[adType]);
    }

    private void LoadAd(string placementId)
    {
        if (string.IsNullOrEmpty(placementId))
        {
            Debug.LogWarning("Load Ad id is Null");
            return;
        }
        if (placementId == adTypeAdId[AdType.Banner])
        {
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerLoadError
            };
            Advertisement.Banner.Load(placementId, options);
            Debug.Log("LOAD BANNER: " + placementId);
            return;
        }
        Advertisement.Load(placementId, this);
        Debug.Log("LOAD AD: " + placementId);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad loaded: " + placementId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Ad load is failed: " + placementId + " ERROR - " +
            error + " MESSAGE - " + message);
    }

    #endregion

    #region Ads Show

    public void ShowAd(AdType adType)
    {
        if (adTypeAdId.ContainsKey(adType) == false)
        {
            Debug.LogWarning("Show Ad AdType parameter is invalid");
            return;
        }
        ShowAd(adTypeAdId[adType]);
    }

    public void ShowAd(string placementId)
    {
        if (string.IsNullOrEmpty(placementId))
        {
            Debug.LogWarning("Show Ad id is Null");
            return;
        }
        if (placementId == adTypeAdId[AdType.Banner])
        {
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(placementId, options);
            Debug.Log("SHOW BANNER: " + placementId);
            return;
        }
        Advertisement.Show(placementId, this);
        Debug.Log("SHOW AD: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad click: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad show completed: " + placementId + " SHOW COMPLETION STATE: " + showCompletionState);
        LoadAd(placementId);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Ad show failied: " + placementId
            + " ERROR: " + error + " MESSAGE: " + message);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad show start: " + placementId);
    }

    #endregion

    #region Banner

    private void OnBannerLoaded()
    {
        Debug.Log("BANNER LOAD SUCCESSFULL");
    }

    private void OnBannerLoadError(string message)
    {
        Debug.LogWarning("BANNER LOAD ERROR, MSG: " + message);
    }

    private void OnBannerClicked()
    {
        Debug.Log("BANNER HAVE BEEN CLICKED");
    }

    private void OnBannerHidden()
    {
        Debug.Log("BANNER HAVE BEEN HIDDEN");
        LoadAd(AdType.Banner);
    }

    private void OnBannerShown()
    {
        Debug.Log("BANNER HAVE BEEN SHOWN");
    }

    #endregion
}
