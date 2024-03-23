using System;
using UnityEngine;


public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get; private set; }

    public float Score { get; private set; }
    public int CurrentFishCount { get; private set; }

    public Action<int> FishCollected;
    public Action<float> ScoreChanged;

    [SerializeField] private float distanceModifier = 1.5f;
    [SerializeField] private float pointsPerFish;
    [SerializeField] private AudioClip collectFishSFX;

    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float s = GameManager.Instance.PlayerMotor.transform.position.z * distanceModifier;
        s += CurrentFishCount * pointsPerFish;

        if (s > Score)
        {
            Score = s;
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                ScoreChanged?.Invoke(Score);
            }
        }
    }

    public void OnCollectFish()
    {
        CurrentFishCount++;
        FishCollected?.Invoke(CurrentFishCount);
        AudioManager.Instance.PlaySFX(collectFishSFX);
    }

    public void ResetSession()
    {
        Score = 0;
        CurrentFishCount = 0;
        ScoreChanged?.Invoke(Score);
        FishCollected?.Invoke(CurrentFishCount);
    }

    public string ScoreToText()
    {
        return Score.ToString("0000000");
    }

    public string FishToText()
    {
        return CurrentFishCount.ToString("0000");
    }
}
