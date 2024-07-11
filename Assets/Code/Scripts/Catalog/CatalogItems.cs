using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatalogItems : MonoBehaviour
{
    public TMP_Text MyKibble;
    public GameObject ItemIcon;
    public TMP_Text ItemCost;
    public TMP_Text pageTitle;
    public GameObject BuyTag;
    ItemData CurrentItem;
    public GameObject[] ItemDisplay;
    public ItemData[] StoreInventory;
    public List<ItemData> filteredStoreInventory;
    public GameObject[] filterTabs;
    public string[] titleStrings = new string[]
    {
        "All",
        "Miscellaneous",
        "Furniture",
        "Hats",
        "Glasses",
        "Bottoms",
        "Tops",
        "Gloves",
        "Wrist Items",
        "Collars",
        "Shoes",
        "Toys",
        "Food",
    };
    public GameObject arrowRight;
    public GameObject arrowLeft;
    int currentTab = 0;
    int pageCount = 1;
    int maxPageCount = 1;
    int finalPageItemCount;
    public TMPro.TextMeshProUGUI pageNumberLeftText;
    int pageNumberLeft;
    public TMPro.TextMeshProUGUI pageNumberRightText;
    int pageNumberRight;
    public Sprite itemBgMember;
    public Sprite itemBgNonmember;

    void Start()
    {
        pageTitle.text = titleStrings[0];
        pageCount = 1;
        // neutralizes tab state
        for (int i = 1; i < filterTabs.Length; i++)
        {
            filterTabs[i].SetActive(false);
        }
        // sees which different categories there are in the book
        for (int i = 0; i < StoreInventory.Length; i++)
        {
            // miscellaneous
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Miscellaneous)
            {
                filterTabs[1].SetActive(true);
            }
            // furniture
            if (StoreInventory[i].ItemCategory == ItemData.itemCategory.Furniture)
            {
                filterTabs[2].SetActive(true);
            }
            // hat
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Hat)
            {
                filterTabs[3].SetActive(true);
            }
            // glasses
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Glasses)
            {
                filterTabs[4].SetActive(true);
            }
            // bottoms
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Bottoms)
            {
                filterTabs[5].SetActive(true);
            }
            // tops
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Tops)
            {
                filterTabs[6].SetActive(true);
            }
            // gloves
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Gloves)
            {
                filterTabs[7].SetActive(true);
            }
            // wrist items
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.WristItems)
            {
                filterTabs[8].SetActive(true);
            }
            // collar
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Collar)
            {
                filterTabs[9].SetActive(true);
            }
            // shoes
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Shoes)
            {
                filterTabs[10].SetActive(true);
            }
            // toys
            if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Toy)
            {
                filterTabs[11].SetActive(true);
            }
            // food
            if (StoreInventory[i].ItemCategory == ItemData.itemCategory.Food)
            {
                filterTabs[12].SetActive(true);
            }
        }
        // sets viewed store inventory to all items
        filteredStoreInventory.Clear();
        for (int i = 0; i < StoreInventory.Length; i++)
        {
            filteredStoreInventory.Add(StoreInventory[i]);
        }
        UpdatePages();
    }

    void UpdatePages()
    {
        // sets max page count
        if (filteredStoreInventory.Count % 10 == 0)
        {
            maxPageCount = filteredStoreInventory.Count / 10;
        }
        else
        {
            maxPageCount = (filteredStoreInventory.Count / 10) + 1;
        }
        UpdateArrows();
        // neutralizes slot state
        for (int i = 0; i < ItemDisplay.Length; i++)
        {
            ItemDisplay[i].SetActive(true);
            ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(false);
        }
        // gets final page item count
        finalPageItemCount = filteredStoreInventory.Count - ((filteredStoreInventory.Count / 10) * 10);
        // deactivates unoccupied slots on final page
        if (pageCount == maxPageCount)
        {
            if (finalPageItemCount != 0)
            {
                for (int i = ItemDisplay.Length; i > finalPageItemCount; i--)
                {
                    ItemDisplay[i - 1].SetActive(false);
                }
            }
        }
        // sets item visuals per displayed button
        if (pageCount != maxPageCount)
        {
            for (int i = 0; i < ItemDisplay.Length; i++)
            {
                if (filteredStoreInventory[((pageCount * 10) - 10) + i].membersOnly)
                {
                    ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(true);
                    ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgMember;
                }
                else
                {
                    ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(false);
                    ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgNonmember;
                }
                ItemDisplay[i].transform.Find("item hitbox/item image").gameObject.GetComponent<Image>().sprite = filteredStoreInventory[((pageCount * 10) - 10) + i].icon;
                ItemDisplay[i].transform.Find("item hitbox/price box/price").gameObject.GetComponent<TextMeshProUGUI>().text = $"{filteredStoreInventory[((pageCount * 10) - 10) + i].price:n0}";
            }
        }
        else
        {
            if (finalPageItemCount != 0)
            {
                for (int i = 0; i < finalPageItemCount; i++)
                {
                    if (filteredStoreInventory[((pageCount * 10) - 10) + i].membersOnly)
                    {
                        ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(true);
                        ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgMember;
                    }
                    else
                    {
                        ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(false);
                        ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgNonmember;
                    }
                    ItemDisplay[i].transform.Find("item hitbox/item image").gameObject.GetComponent<Image>().sprite = filteredStoreInventory[((pageCount * 10) - 10) + i].icon;
                    ItemDisplay[i].transform.Find("item hitbox/price box/price").gameObject.GetComponent<TextMeshProUGUI>().text = $"{filteredStoreInventory[((pageCount * 10) - 10) + i].price:n0}";
                }
            }
            else
            {
                for (int i = 0; i < ItemDisplay.Length; i++)
                {
                    if (filteredStoreInventory[((pageCount * 10) - 10) + i].membersOnly)
                    {
                        ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(true);
                        ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgMember;
                    }
                    else
                    {
                        ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(false);
                        ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgNonmember;
                    }
                    ItemDisplay[i].transform.Find("item hitbox/item image").gameObject.GetComponent<Image>().sprite = filteredStoreInventory[((pageCount * 10) - 10) + i].icon;
                    ItemDisplay[i].transform.Find("item hitbox/price box/price").gameObject.GetComponent<TextMeshProUGUI>().text = $"{filteredStoreInventory[((pageCount * 10) - 10) + i].price:n0}";
                }
            }
        }
    }

    public void SetTag(int buttonID)
    {
        CurrentItem = filteredStoreInventory[((pageCount * 10) - 10) + buttonID];
        MyKibble.SetText($"{GameDataManager.Instance.kibble:n0}");
        ItemIcon.GetComponent<Image>().sprite = CurrentItem.icon;
        ItemCost.SetText($"{CurrentItem.price:n0}");
        BuyTag.SetActive(true);
    }


    public void BuyItem()
    {
        if (GameDataManager.Instance.kibble >= CurrentItem.price)
        {
            Debug.Log(CurrentItem.ID);
            GameDataManager.Instance.AddInventory(CurrentItem.ID);
            GameDataManager.Instance.SubtractKibble(CurrentItem.price);
            MyKibble.SetText(GameDataManager.Instance.kibble.ToString());
            BuyTag.SetActive(false);
        }
    }
    public void CloseTag()
    {
        BuyTag.SetActive(false);
    }
    void UpdateArrows()
    {
        // sets page number text
        pageNumberRight = pageCount * 2;
        pageNumberLeft = pageNumberRight - 1;
        pageNumberRightText.text = pageNumberRight.ToString();
        pageNumberLeftText.text = pageNumberLeft.ToString();
        // sets arrow active state
        if (maxPageCount == 1)
        {
            arrowRight.SetActive(false);
            arrowLeft.SetActive(false);
        }
        else
        {
            if (pageCount == maxPageCount)
            {
                arrowRight.SetActive(false);
                arrowLeft.SetActive(true);
            }
            else if (pageCount == 1)
            {
                arrowRight.SetActive(true);
                arrowLeft.SetActive(false);
            }
            else
            {
                arrowRight.SetActive(true);
                arrowLeft.SetActive(true);
            }
        }
    }
    public void PageRight()
    {
        pageCount += 1;
        UpdatePages();
    }
    public void PageLeft()
    {
        pageCount -= 1;
        UpdatePages();
    }

    public void FilterInventory(int buttonID)
    {
        pageCount = 1;
        currentTab = buttonID;
        filteredStoreInventory.Clear();
        for (int i = 0; i < StoreInventory.Length; i++)
        {
            if (currentTab == 0)
            {
                filteredStoreInventory.Add(StoreInventory[i]);
            }
            else if (currentTab == 1)
            {
                if (StoreInventory[i].ItemCategory == ItemData.itemCategory.Miscellaneous)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 2)
            {
                if (StoreInventory[i].ItemCategory == ItemData.itemCategory.Furniture)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 3)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Hat)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 4)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Glasses)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 5)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Bottoms)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 6)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Tops)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 7)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Gloves)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 8)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.WristItems)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 9)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Collar)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 10)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Shoes)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 11)
            {
                if (StoreInventory[i].ItemCategoryText == ItemData.itemCategoryText.Toy)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else if (currentTab == 12)
            {
                if (StoreInventory[i].ItemCategory == ItemData.itemCategory.Food)
                {
                    filteredStoreInventory.Add(StoreInventory[i]);
                }
            }
            else
            {
                filteredStoreInventory.Add(StoreInventory[i]);
                Debug.Log("Error filtering items");
            }
        }
        pageTitle.text = titleStrings[buttonID];
        UpdatePages();
    }
}
