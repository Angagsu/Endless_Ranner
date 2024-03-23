using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    [SerializeField] private InitializeAd initializeAd;
    [SerializeField] private RevardedAd revardedAd;


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        revardedAd.LoadRevardedAd();
    }

    public void ShowRevardedAd()
    {
        revardedAd.ShowRevardedAd();
    }
}
