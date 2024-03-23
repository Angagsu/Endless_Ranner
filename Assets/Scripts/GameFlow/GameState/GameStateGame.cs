using UnityEngine;
using TMPro;


public class GameStateGame : GameState
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private AudioClip gameMusic;

    public override void Construct()
    {
        gameManager.PlayerMotor.ResumePlayer();
        gameManager.ChangeCamera(Cameras.Game);

        GameStats.Instance.FishCollected += OnFishCollected;
        GameStats.Instance.ScoreChanged += OnScoreChanged;

        gameUI.SetActive(true);

        AudioManager.Instance.PlayMusicWithXFade(gameMusic, 0.5f);
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
        gameManager.WorldGeneration.ScanPosition();
        gameManager.SceneChunkGeneration.ScanPosition();
    }
}
