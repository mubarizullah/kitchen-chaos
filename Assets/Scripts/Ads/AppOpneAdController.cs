using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AppOpneAdController : MonoBehaviour
{
    private string _adUnitId = "ca-app-pub-3940256099942544/9257395921";

    private AppOpenAd appOpenAd;

    private void Awake()
    {
        LoadOpenAd();
    }

    private void Start()
    {
        Invoke(nameof(ShowAppOpenAd), 2f);
    }
    public void LoadOpenAd()
    {
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        Debug.Log("Loading Ap Open Id");

        // creating request for ad

        var adRequest = new AdRequest();

        //send the request to load the ad

        AppOpenAd.Load
        (_adUnitId, adRequest, (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                Debug.Log("App open Ad failed to run due to : " + error);
                return;
            }
            Debug.Log("Ad open ad is loaded with response : " + ad.GetResponseInfo());

            appOpenAd = ad;
            RegisterEventHandler(ad);
        });
    }

    private void RegisterEventHandler(AppOpenAd ad)
    {
        ad.OnAdClicked += () => { Debug.Log("ad is clicked"); };
        ad.OnAdFullScreenContentClosed += () => { { Debug.Log("Full screen ad is closed");}; };
    }

    public void ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.Show();
            LoadOpenAd();
        }
        else
        {
            Debug.Log("Ad open Ad was not loaded properly");
            LoadOpenAd();
        }
    }

    
}
