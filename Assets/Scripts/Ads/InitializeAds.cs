using GoogleMobileAds.Api;
using UnityEngine;

public class InitializeAds : MonoBehaviour
{
    private void Start()
    {
        MobileAds.Initialize(initstatus => { Debug.Log("Ads are initialized"); });
    }
}
