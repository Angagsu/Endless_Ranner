using UnityEngine;
using TMPro;

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

        fishCountText.text = "xTBD";
        highscoreText.text = "TBD";

        gameUI.SetActive(true);
    }

    public override void Destruct()
    {
        gameUI.SetActive(false);
    }

    public override void UpdateState()
    {
        GameManager.Instance.WorldGeneration.ScanPosition();
        GameManager.Instance.SceneChunkGeneration.ScanPosition();
    }
}
