using UnityEngine;
using UnityEngine.Advertisements;

public class RevardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitID;
    [SerializeField] private string iosAdUnitID;

    private string adUnitID;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_ANDROID
        adUnitID = androidAdUnitID;
#else
        adUnitID = iosAdUnitID;
#endif
    }

    public void LoadRevardedAd()
    {
        Advertisement.Load(adUnitID, this);
    }

    #region LoadCallBacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Revarded Ad Is Loaded!");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message){ }
    #endregion

    public void ShowRevardedAd()
    {
        Advertisement.Show(adUnitID, this);
        LoadRevardedAd();
    }

    #region ShowCallBacks

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string placementId){ }
    public void OnUnityAdsShowClick(string placementId){ }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        GameManager.Instance.gameObject.GetComponent<GameStateDeath>().DisableReviving();
        
        if (placementId == adUnitID && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            GameManager.Instance.gameObject.GetComponent<GameStateDeath>().ResumeGame();
        }
        else if (showCompletionState.Equals(UnityAdsShowCompletionState.SKIPPED) || showCompletionState.Equals(UnityAdsShowCompletionState.UNKNOWN))
        {
            GameManager.Instance.GetComponent<GameStateDeath>().ToMenu();
        }
    }

    #endregion      
}
