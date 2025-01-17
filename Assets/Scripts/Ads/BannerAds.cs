using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour
{
    BannerView bannerView;

    private const string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
    //[SerializeField]
    //int heightOfBannerAd = 60;
    //[SerializeField]
    //int widhtOfBannerAd = 400;
    private void Start()
    {
        LoadAndShowBannerAds();
    }
    public void CreateBannerView()
    {
        if (bannerView != null)
        {
            Debug.Log("Ad has been destroyed");
            DestroyAd();
        }

        var adSize = new AdSize(728, 90);
        bannerView = new BannerView(_adUnitId, adSize, AdPosition.Bottom);
    }

    public void LoadAndShowBannerAds()
    {
        if (bannerView == null)
        {
            CreateBannerView();
        }

        var requestAd = new AdRequest();

        Debug.Log("Requesting banner ad");
        bannerView.LoadAd(requestAd);
    }

    public void DestroyAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
            Debug.Log("BannerView is destroyed");
        }

    }
}
