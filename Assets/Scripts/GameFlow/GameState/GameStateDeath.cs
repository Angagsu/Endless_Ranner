using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateDeath : GameState
{
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI fishTotalScoreText;
    [SerializeField] private TextMeshProUGUI fishCurrentScoreText;

    [SerializeField] private Image timerImage;
    [SerializeField] private float timer;

    private float deathTime;

    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.PlayerMotor.PausePlayer();

        deathTime = Time.time;
        deathUI.SetActive(true);
        timerImage.gameObject.SetActive(true);

        if (SaveManager.Instance.SaveState.Highscore < (int)GameStats.Instance.score)
        {
            SaveManager.Instance.SaveState.Highscore = (int)GameStats.Instance.score;
            currentScoreText.color = Color.green;
        }
        else
        {
            currentScoreText.color = Color.white;
        }

        SaveManager.Instance.SaveState.Fish += GameStats.Instance.currentFishCount;

        SaveManager.Instance.Save();

        highscoreText.text = "Highscore: " + SaveManager.Instance.SaveState.Highscore;
        currentScoreText.text = GameStats.Instance.ScoreToText();
        fishTotalScoreText.text = "Total fish: " + SaveManager.Instance.SaveState.Fish;
        fishCurrentScoreText.text = GameStats.Instance.FishToText();
    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timer;
        timerImage.color = Color.Lerp(Color.green, Color.red, ratio);
        timerImage.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            timerImage.gameObject.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        gameManager.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.PlayerMotor.RespawnPlayer();
    }

    public void ToMenu()
    {
        gameManager.ChangeState(GetComponent<GameStateInit>());
        GameManager.Instance.PlayerMotor.ResetPlayer();
        GameManager.Instance.WorldGeneration.ResetWorld();
        GameManager.Instance.SceneChunkGeneration.ResetWorld();
    }
}
