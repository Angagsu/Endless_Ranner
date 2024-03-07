using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;

    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;

    private bool isInit = false;

    protected override void Awake()
    {
        base.Awake();
        
    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(Cameras.Shop);
        hats = Resources.LoadAll<Hat>("Hat/");
        shopUI.SetActive(true);
        totalFish.text = SaveManager.Instance.SaveState.Fish.ToString("0000");
        if (!isInit)
        {
            
            currentHatName.text = hats[SaveManager.Instance.SaveState.CurrentHatIndex].ItemName;
            PopulateShop();
            isInit = true;
        }    
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        gameManager.ChangeState(GetComponent<GameStateInit>());
    }

    private void PopulateShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer);
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;

            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;

            if (SaveManager.Instance.SaveState.UnlockedHatFlag[i] == 0)
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            }
            else
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            }
            
        }
    }

    private void OnHatClick(int i)
    {
        if (SaveManager.Instance.SaveState.UnlockedHatFlag[i] == 1)
        {
            SaveManager.Instance.SaveState.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            SaveManager.Instance.Save();
        }
        else if (hats[i].ItemPrice <= SaveManager.Instance.SaveState.Fish)
        {
            SaveManager.Instance.SaveState.Fish -= hats[i].ItemPrice;
            SaveManager.Instance.SaveState.UnlockedHatFlag[i] = 1;
            SaveManager.Instance.SaveState.CurrentHatIndex = i;

            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            totalFish.text = SaveManager.Instance.SaveState.Fish.ToString("000");
            SaveManager.Instance.Save();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";

        }
        else
        {
            Debug.Log("Not enough fish");
        }

        
    }
}
