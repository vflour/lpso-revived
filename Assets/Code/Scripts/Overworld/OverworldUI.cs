using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class OverworldUI : MonoBehaviour
{
    public TMP_Text kibblecount;
    public TMP_Text kibblecountinv;

    public GameObject inventory;
    public GameObject PDA;
    public GameObject userInfoMenu;
    public GameObject map;

    public GameObject xButton;
    public GameObject petButton;
    public GameObject messagebutton;
    public GameObject messagebar;
    public GameObject collectapetbutton;

    public GameObject PDAButton;
    public GameObject PowerButton;

    public GameObject invButton;
    public GameObject closeInvButton;
    public Button InvLeftButton;
    public Button InvRightButton;
    public GameObject SideInv;
    public Button SideInvButton;
    public GameObject SideInvArrow;

    public GameObject mapXButton;
    public GameObject mapOpenButton;
    public GameObject travelConfirmPopup;
    public string[] mapLocations;
    public Button[] mapIcons;
    public TMPro.TextMeshProUGUI LocationName;

    public class MapIcon
    { public string AreaName; }

    // Start is called before the first frame update
    void Start()
    {
        userInfoMenu.SetActive(false);
        inventory.SetActive(false);
        PDA.SetActive(false);
        messagebar.SetActive(true);
        map.SetActive(false);
        SideInvButton.interactable = false;
        InvLeftButton.interactable = false;
        InvRightButton.interactable = false;
        
    }

    void Update(){
        kibblecount.SetText(GameDataManager.Instance.kibble.ToString());
        kibblecountinv.SetText(GameDataManager.Instance.kibble.ToString());
    }
    
	public void toggleInventory()
    { inventory.SetActive(!inventory.activeSelf); }

    // this function isnt what i want but i need to remember to change it later
    public void toggleSideInv()
    { SideInv.SetActive(!SideInv.activeSelf); }

    public void toggleUserMenu()
    { userInfoMenu.SetActive(!userInfoMenu.activeSelf); }

    public void toggleMessage()
    { messagebar.SetActive(!messagebar.activeSelf); }

    public void togglePhone()
    { PDA.SetActive(!PDA.activeSelf); }

    public void toggleMap()
    {
        map.SetActive(!map.activeSelf);
        travelConfirmPopup.SetActive(false);
    }

    public void TravelTo(int buttonID)
    {
        travelConfirmPopup.SetActive(true);
        LocationName.text = mapLocations[buttonID] + "?";
    }

    public void TravelCancel()
    {
        travelConfirmPopup.SetActive(false);
    }

}
