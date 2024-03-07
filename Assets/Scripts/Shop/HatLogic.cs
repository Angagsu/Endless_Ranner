using System.Collections.Generic;
using UnityEngine;

public class HatLogic : MonoBehaviour
{

    [SerializeField] private Transform hatContainer;
    private Hat[] hats;
    private List<GameObject> hatModels = new List<GameObject>();
    private void Start()
    {
        hats = Resources.LoadAll<Hat>("Hat");
        SpawnHats();
        SelectHat(SaveManager.Instance.SaveState.CurrentHatIndex);
    }

    private void SpawnHats()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            hatModels.Add(Instantiate(hats[i].Model, hatContainer));
        }
        
    }

    public void DisableAllHats()
    {
        for (int i = 0; i < hatModels.Count; i++)
        {
            hatModels[i].SetActive(false);
        }
    }

    public void SelectHat(int index)
    {
        DisableAllHats();
        hatModels[index].SetActive(true);
    }
}
