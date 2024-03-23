using UnityEngine;


[CreateAssetMenu(fileName = "Hat", menuName = "ShopItems")]
public class Hat : ScriptableObject
{
    public string ItemName;
    public int ItemPrice;
    public Sprite Thumbnail;
    public GameObject Model;
}
