using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnsLogic : MonoBehaviour
{
    public TMP_Text MyKibble;
    public GameObject ItemIcon;
    public TMP_Text ItemCost;
    public GameObject tag;
    public ItemData CurrentItem;
    public GameObject[] Buttons;
    void Start(){
        for (int i = 0; i <Buttons.Length; i++){
            addButton(i);
        }
    }

    void addButton(int furnitureID){
        Debug.Log("Attemtping to make button " + furnitureID);
        if(furnitureID < GameDataManager.Instance.furniture.Count-1){
            ItemData CatalogItemsData = GameDataManager.Instance.furniture[furnitureID+1];
            Buttons[furnitureID].transform.Find("item hitbox/price box/price").GetComponent<TMP_Text>().SetText(CatalogItemsData.price.ToString());
            Buttons[furnitureID].transform.Find("item hitbox/item image").GetComponent<Image>().sprite = CatalogItemsData.icon;
            Buttons[furnitureID].transform.Find("item hitbox").GetComponent<Button>().onClick.AddListener(() => {
                SetTag(furnitureID+1);
        });
        Debug.Log("Button " + furnitureID+1 + " complete.");
        }
     }

    public void SetTag(int FurnitureID){
        CurrentItem = GameDataManager.Instance.furniture[FurnitureID];
        MyKibble.SetText(GameDataManager.Instance.kibble.ToString());
        ItemIcon.GetComponent<Image>().sprite = CurrentItem.icon;
        ItemCost.SetText(CurrentItem.price.ToString());
        tag.SetActive(true);
    }

    public void BuyItem(){
        if (GameDataManager.Instance.kibble >= CurrentItem.price) {
            GameDataManager.Instance.AddInventory(CurrentItem);
            GameDataManager.Instance.kibble -= CurrentItem.price;
            MyKibble.SetText(GameDataManager.Instance.kibble.ToString());
        }
    }
    public void CloseTag(){
        tag.SetActive(false);
    }
}
