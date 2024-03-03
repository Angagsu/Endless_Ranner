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

        highScoreText.text = "Highscore : " + "TBD";
        fishCountText.text = "Fish : " + "TBD";

        menuUI.SetActive(true);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        gameManager.ChangeState(GetComponent<GameStateGame>());
    }

    public void OnShopClick()
    {
        gameManager.ChangeState(GetComponent<GameStateShop>());
        Debug.Log("Shop button has been clicked!");
    }
}
