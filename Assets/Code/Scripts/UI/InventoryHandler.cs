using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryHandler : MonoBehaviour
{
    public GameObject[] Buttons;
    public GameObject[] ItemSlotBg;
    public TMPro.TextMeshProUGUI[] inventoryItemStackAmount;
    public int itemCount;
    public int finalPageItemCount;
    public int pageCount = 1;
    public int maxPageCount;
    public TMPro.TextMeshProUGUI pageCountDisplay;
    public Button pageLeft;
    public Button pageRight;
    public ItemData itemData;

    public OverworldUI owUI;

    void Awake()
    {
        pageCount = 1;
    }

    void addButton(int itemID)
    {
        GameDataManager Data = GameDataManager.Instance;
        if (itemID < GameDataManager.Instance.inventory.Count)
        {
            ItemData ItemData = Data.itemList[Data.inventory[Data.inventory.Count - itemID - 1]];
            Buttons[itemID].GetComponent<Image>().sprite = ItemData.icon;
            if (inventoryItemStackAmount[itemID].text == "1")
            {
                inventoryItemStackAmount[itemID].text = "";
            }
            Debug.Log("Button " + itemID + " complete.");
        }
        else
        {
            ItemSlotBg[itemID].gameObject.SetActive(false);
            Debug.Log("Button " + itemID + " empty.");
        }
    }

    public void inventoryUpdate()
    {
        itemCount = GameDataManager.Instance.inventory.Count;
        // neutralizes state of slots
        for (int i = 0; i < ItemSlotBg.Length; i++)
        {
            ItemSlotBg[i].SetActive(true);
        }
        // sets max page count
        if (itemCount % 12 == 0)
        {
            maxPageCount = itemCount / 12;
        }
        else
        {
            maxPageCount = (itemCount / 12) + 1;
        }
        pageCountDisplay.text = pageCount.ToString() + "/" + maxPageCount.ToString();
        if (maxPageCount > 1)
        {
            pageRight.interactable = true;
            pageLeft.interactable = true;
        }
        else
        {
            pageRight.interactable = false;
            pageLeft.interactable = false;
        }
        // sets final page amount, display logic
        finalPageItemCount = itemCount - ((itemCount/12) * 12);
        if (pageCount == maxPageCount)
        {
            if (finalPageItemCount != 0)
            {
                for (int i = 0; i < finalPageItemCount; i++)
                {
                    Buttons[i].GetComponent<Image>().sprite = GameDataManager.Instance.itemList[GameDataManager.Instance.inventory[(((pageCount * 12) - 12) + i)]].icon;
                    inventoryItemStackAmount[i].text = GameDataManager.Instance.invItemCounts[(((pageCount * 12) - 12) + i)].ToString();
                    if (inventoryItemStackAmount[i].text == "1")
                    {
                        inventoryItemStackAmount[i].text = "";
                    }
                }
                for (int i = ItemSlotBg.Length; i > finalPageItemCount; i--)
                {
                    ItemSlotBg[i - 1].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < ItemSlotBg.Length; i++)
                {
                    Buttons[i].GetComponent<Image>().sprite = GameDataManager.Instance.itemList[GameDataManager.Instance.inventory[(((pageCount * 12) - 12) + i)]].icon;
                    ItemSlotBg[i].SetActive(true);
                    inventoryItemStackAmount[i].text = GameDataManager.Instance.invItemCounts[(((pageCount * 12) - 12) + i)].ToString();
                    if (inventoryItemStackAmount[i].text == "1")
                    {
                        inventoryItemStackAmount[i].text = "";
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < ItemSlotBg.Length; i++)
            {
                Buttons[i].GetComponent<Image>().sprite = GameDataManager.Instance.itemList[GameDataManager.Instance.inventory[(((pageCount * 12) - 12) + i)]].icon;
                inventoryItemStackAmount[i].text = GameDataManager.Instance.invItemCounts[(((pageCount * 12) - 12) + i)].ToString();
                if (inventoryItemStackAmount[i].text == "1")
                {
                    inventoryItemStackAmount[i].text = "";
                }
            }
        }
    }
    public void menuIncrement()
    {
        pageCount += 1;
        if (pageCount > maxPageCount)
        {
            pageCount = 1;
        }
        inventoryUpdate();
    }
    public void menuDecrement()
    {
        pageCount -= 1;
        if (pageCount <= 0)
        {
            pageCount = maxPageCount;
        }
        inventoryUpdate();
    }
}

