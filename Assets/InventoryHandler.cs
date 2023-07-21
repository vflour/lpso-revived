using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    public GameObject[] Buttons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <Buttons.Length; i++){
            addButton(i);
        }
    }

    void addButton(int itemID){
        Debug.Log("Attemtping to make button " + itemID);
        if(itemID < GameDataManager.Instance.inventory.Count){
            ItemData ItemData = GameDataManager.Instance.itemList[GameDataManager.Instance.inventory[itemID]];
            Buttons[itemID].GetComponent<Image>().sprite = ItemData.icon;
            Debug.Log("Button " + itemID + " complete.");
        }
     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
