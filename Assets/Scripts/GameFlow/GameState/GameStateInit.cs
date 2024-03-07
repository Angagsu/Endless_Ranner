using UnityEngine;
using TMPro;
public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI fishCountText;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(Cameras.Init);

        highScoreText.text = "Highscore: " + SaveManager.Instance.SaveState.Highscore.ToString();
        fishCountText.text = "Fish: " + SaveManager.Instance.SaveState.Fish.ToString();

        menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        gameManager.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }

    public void OnShopClick()
    {
        gameManager.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop button has been clicked!");
    }
}
