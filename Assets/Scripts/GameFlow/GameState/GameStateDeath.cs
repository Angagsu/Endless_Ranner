using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameStateDeath : GameState
{
    [SerializeField] private GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI fishTotalScoreText;
    [SerializeField] private TextMeshProUGUI fishCurrentScoreText;

    [SerializeField] private Image reviveImage;
    [SerializeField] private float timer;

    private float deathTime;

    public override void Construct()
    {
        gameManager.PlayerMotor.PausePlayer();

        deathTime = Time.time;
        deathUI.SetActive(true);

        if (SaveManager.Instance.SaveState.Highscore < (int)GameStats.Instance.Score)
        {
            SaveManager.Instance.SaveState.Highscore = (int)GameStats.Instance.Score;
            currentScoreText.color = Color.green;
        }
        else
        {
            currentScoreText.color = Color.white;
        }

        highscoreText.text = "Highscore: " + SaveManager.Instance.SaveState.Highscore;
        currentScoreText.text = GameStats.Instance.ScoreToText();
        fishTotalScoreText.text = "Total fish: " + (SaveManager.Instance.SaveState.Fish + GameStats.Instance.CurrentFishCount);
        fishCurrentScoreText.text = GameStats.Instance.FishToText();
    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timer;
        reviveImage.color = Color.Lerp(Color.green, Color.red, ratio);
        reviveImage.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            DisableReviving();
        }
    }

    public void TryResumeGame()
    {
        AdManager.Instance.ShowRevardedAd();
    }

    public void ResumeGame()
    {
        gameManager.ChangeState(gameStateGame);
        gameManager.PlayerMotor.RespawnPlayer();
    }

    public void ToMenu()
    {
        SaveManager.Instance.SaveState.Fish += GameStats.Instance.CurrentFishCount;
        SaveManager.Instance.Save();

        gameManager.ChangeState(gameStateInit);
        gameManager.PlayerMotor.ResetPlayer();
        gameManager.WorldGeneration.ResetWorld();
        gameManager.SceneChunkGeneration.ResetWorld();
    }

    public void DisableReviving()
    {
        reviveImage.gameObject.SetActive(false);
    }

    public void EnableRevive()
    {
        reviveImage.gameObject.SetActive(true);
    }
}
