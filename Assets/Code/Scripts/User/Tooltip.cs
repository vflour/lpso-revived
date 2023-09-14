using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private TooltipData _data; 
    public TooltipData data {
        
        set 
        { 
            
            _data = value;
            // If basic tooltip w/o category
            if (String.IsNullOrEmpty(_data.category))
            {
                mapLine.SetActive(true);
                mapLineTitle.text = _data.title;
                mapLineTitle.ForceMeshUpdate();
                mapLineIcon.sprite = _data.icon;
            }
            // Has category
            else 
            {
                itemLine.SetActive(true);
                itemLineTitle.text = _data.title;
                itemLineTitle.ForceMeshUpdate();
                itemLineIcon.sprite = _data.icon;
                itemLineCategory.text = _data.category;
            }

            // Set membership dialog
            if (_data.hasMemberDescription)
            {
                membershipLine.SetActive(true);
            }

            // Set description
            if (!String.IsNullOrEmpty(_data.description))
            {
                descriptionLineText.text = _data.description;
            }

        }
        
        get { return _data; }   
    
    }

    [Header("UI Components")]
    public GameObject itemLine;
    public GameObject mapLine;
    public GameObject membershipLine;
    public GameObject descriptionLine;

    private TextMeshProUGUI itemLineTitle;
    private Image itemLineIcon;
    private TextMeshProUGUI itemLineCategory;

    private TextMeshProUGUI mapLineTitle;
    private Image mapLineIcon;
    
    private TextMeshProUGUI descriptionLineText;

    void Awake()
    {

        itemLineTitle = itemLine.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        itemLineIcon = itemLine.transform.Find("IconContainer/Icon").GetComponent<Image>();
        itemLineCategory = itemLine.transform.Find("Text/CategoryBox/Category").GetComponent<TextMeshProUGUI>();

        mapLineTitle = mapLine.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        mapLineIcon = mapLine.transform.Find("Icon").GetComponent<Image>();
        
        descriptionLineText = descriptionLine.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    
    }

}
