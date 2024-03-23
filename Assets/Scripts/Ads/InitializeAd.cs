using UnityEngine.Advertisements;
using UnityEngine;

public class InitializeAd : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameID;
    [SerializeField] private string iosGameID;
    [SerializeField] private bool isTestMode;

    private string gameID;


    private void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        gameID = androidGameID;
#else
        gameID = iosGameID;
#endif
        
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, isTestMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ad Initialized!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message){ }
}
