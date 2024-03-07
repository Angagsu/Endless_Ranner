using UnityEngine;
using TMPro;
using System;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI fishCountText;

    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.PlayerMotor.ResumePlayer();
        GameManager.Instance.ChangeCamera(Cameras.Game);

        GameStats.Instance.FishCollected += OnFishCollected;
        GameStats.Instance.ScoreChanged += OnScoreChanged;

        gameUI.SetActive(true);
    }

    private void OnScoreChanged(float score)
    {
        highscoreText.text = GameStats.Instance.ScoreToText();
    }

    private void OnFishCollected(int fish)
    {
        fishCountText.text = GameStats.Instance.FishToText();
    }

    public override void Destruct()
    {
        gameUI.SetActive(false);
        GameStats.Instance.FishCollected -= OnFishCollected;
        GameStats.Instance.ScoreChanged -= OnScoreChanged;
    }

    public override void UpdateState()
    {
        GameManager.Instance.WorldGeneration.ScanPosition();
        GameManager.Instance.SceneChunkGeneration.ScanPosition();
    }
}
