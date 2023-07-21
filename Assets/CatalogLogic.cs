using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogLogic : MonoBehaviour
{
    public TMP_Text MyKibble;
    public GameObject ItemIcon;
    public TMP_Text ItemCost;
    public GameObject tag;
    public ItemData CurrentItem;
    public GameObject[] Buttons;
    public ItemData[] StoreInventory;

    void Start(){
        for (int i = 0; i <Buttons.Length; i++){
            addButton(i);
        }
    }

    void addButton(int buttonID){
        Debug.Log("Attemtping to make button " + buttonID);
        if(buttonID < StoreInventory.Length){
            ItemData CatalogItemsData = StoreInventory[buttonID];
            Buttons[buttonID].transform.Find("item hitbox/price box/price").GetComponent<TMP_Text>().SetText(CatalogItemsData.price.ToString());
            Buttons[buttonID].transform.Find("item hitbox/item image").GetComponent<Image>().sprite = CatalogItemsData.icon;
            Buttons[buttonID].transform.Find("item hitbox").GetComponent<Button>().onClick.AddListener(() => {
                SetTag(buttonID);
        });
        Debug.Log("Button " + buttonID + " complete.");
        }
     }

    public void SetTag(int buttonID){
        CurrentItem = StoreInventory[buttonID];
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
