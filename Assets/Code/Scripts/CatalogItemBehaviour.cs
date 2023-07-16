using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogItemBehaviour : MonoBehaviour
{
    public FurnitureData CatalogItemsData;
    public TMP_Text priceText;
    public GameObject itemSprite;

    // Start is called before the first frame update
    void Start()
    {
       priceText.SetText(CatalogItemsData.price.ToString());
       itemSprite.GetComponent<Image>().sprite = CatalogItemsData.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyFurniture(){
        GameDataManager.Instance.AddInventory(CatalogItemsData);
    }
}
