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
    public GameObject BuyTag;
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
        BuyTag.SetActive(true);
    }

    public void BuyItem(){
        if (GameDataManager.Instance.kibble >= CurrentItem.price) {
            Debug.Log(CurrentItem.ID);
            GameDataManager.Instance.AddInventory(CurrentItem.ID);
            GameDataManager.Instance.SubtractKibble(CurrentItem.price);
            MyKibble.SetText(GameDataManager.Instance.kibble.ToString());
            BuyTag.SetActive(false);
        }
    }
    public void CloseTag(){
        BuyTag.SetActive(false);
    }
}
