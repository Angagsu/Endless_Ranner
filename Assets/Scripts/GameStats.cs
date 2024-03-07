using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get; private set; }

    public float score;
    public float highscore;
    public float distanceModifier = 1.5f;

    public int totalFish;
    public int currentFishCount;
    public float pointsPerFish;

    public Action<int> FishCollected;
    public Action<float> ScoreChanged;

    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float s = GameManager.Instance.PlayerMotor.transform.position.z * distanceModifier;
        s += currentFishCount * pointsPerFish;

        if (s > score)
        {
            score = s;
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                ScoreChanged?.Invoke(score);
            }
        }
    }

    public void OnCollectFish()
    {
        currentFishCount++;
        FishCollected?.Invoke(currentFishCount);
    }

    public void ResetSession()
    {
        score = 0;
        currentFishCount = 0;
        ScoreChanged?.Invoke(score);
        FishCollected?.Invoke(currentFishCount);
    }

    public string ScoreToText()
    {
        return score.ToString("0000000");
    }

    public string FishToText()
    {
        return currentFishCount.ToString("0000");
    }
}
