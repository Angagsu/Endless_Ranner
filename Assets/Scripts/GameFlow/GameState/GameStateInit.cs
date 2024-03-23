using UnityEngine;
using TMPro;

public class GameStateInit : GameState
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI fishCountText;
    [SerializeField] private AudioClip menuMusic;

    public override void Construct()
    {
        gameManager.ChangeCamera(Cameras.Init);

        highScoreText.text = "Highscore: " + SaveManager.Instance.SaveState.Highscore.ToString();
        fishCountText.text = "Fish: " + SaveManager.Instance.SaveState.Fish.ToString();

        menuUI.SetActive(true);

        AudioManager.Instance.PlayMusicWithXFade(menuMusic, 0.5f);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        gameManager.ChangeState(gameStateGame);
        GameStats.Instance.ResetSession();
        gameStateDeath.EnableRevive();
    }

    public void OnShopClick()
    {
        gameManager.ChangeState(gameStateShop);
        Debug.Log("Shop button has been clicked!");
    }

    public void OnAchievementsClick()
    {

    }

    public void OnLeaderboardClick()
    {

    }
}
