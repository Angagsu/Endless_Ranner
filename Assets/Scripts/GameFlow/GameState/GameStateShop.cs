using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    [SerializeField] private GameObject shopUI;
    [SerializeField] private TextMeshProUGUI totalFish;
    [SerializeField] private TextMeshProUGUI currentHatName;
    [SerializeField] private HatLogic hatLogic;

    [SerializeField] private GameObject hatPrefab;
    [SerializeField] private Transform hatContainer;

    [SerializeField] private Image completionCircle;
    [SerializeField] private TextMeshProUGUI completionText;

    private Hat[] hats;
    private int hatCount;
    private int unlockedHatCount;
    private bool isInit = false;


    public override void Construct()
    {
        gameManager.ChangeCamera(Cameras.Shop);
        hats = Resources.LoadAll<Hat>("Hat/");
        shopUI.SetActive(true);
        totalFish.text = SaveManager.Instance.SaveState.Fish.ToString("0000");

        if (!isInit)
        {    
            currentHatName.text = hats[SaveManager.Instance.SaveState.CurrentHatIndex].ItemName;
            PopulateShop();
            hatCount = hats.Length - 1;
            isInit = true;
        }   
        
        ResetCompletionCircle();
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        gameManager.ChangeState(gameStateInit);
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
                unlockedHatCount++;
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
            unlockedHatCount++;
            ResetCompletionCircle();
        }
        else
        {
            Debug.Log("Not enough fish");
        } 
    }

    private void ResetCompletionCircle()
    {  
        int currentlyUnlockedCount = unlockedHatCount - 1;

        completionCircle.fillAmount = (float)currentlyUnlockedCount / (float)hatCount;
        completionText.text = currentlyUnlockedCount + " / " + hatCount;
    }
}
