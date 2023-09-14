using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class OverworldUI : MonoBehaviour
{
    public TMP_Text kibblecount;
    public TMP_Text kibblecountinv;

    public string[] scenes;

    public GameObject inventory;
    public GameObject PDA;
    public GameObject userInfoMenu;
    public GameObject map;
    public GameObject nothing;

    public Button CoAPButtonUserMenu;
    public Button ScrapbookButtonUserMenu;
    public Button ClothesButton;
    public Button HouseButtonUserMenu;
    public GameObject HouseConfirm;
    public Button CrAPButton;

    public Button xButton;
    public Button petButton;
    public Button messagebutton;
    public GameObject messagebar;
    public Button collectapetbutton;

    public Button PDAButton;
    public Button PowerButton;

    public Button ScrapbookButton;
    public Button ScrapbookClose;

    public Button invButton;
    public Button closeInvButton;
    public Button InvLeftButton;
    public Button InvRightButton;
    public GameObject SideInv;
    public Button SideInvButton;
    public GameObject SideInvArrow;

    public Button mapXButton;
    public Button mapOpenButton;
    public GameObject travelConfirmPopup;
    public int SelectedLocation;
    public string[] mapLocations;
    public Button[] mapIcons;
    public TMPro.TextMeshProUGUI LocationName;

    public Button nothingOk;

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
        nothing.SetActive(false);
        SideInvButton.interactable = false;
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
    { userInfoMenu.SetActive(!userInfoMenu.activeSelf);
    inventory.SetActive(false);
    HouseConfirm.SetActive(false);}

    public void toggleHouseConfirm()
    { HouseConfirm.SetActive(!HouseConfirm.activeSelf); }

    public void toggleMessage()
    { messagebar.SetActive(!messagebar.activeSelf); }

    public void togglePhone()
    { PDA.SetActive(!PDA.activeSelf);
    inventory.SetActive(false);}

    public void toggleMap()
    { map.SetActive(!map.activeSelf);
    travelConfirmPopup.SetActive(false);
    inventory.SetActive(false);}

    public void toggleNothing()
    { nothing.SetActive(!nothing.activeSelf);
    inventory.SetActive(false); }

    public void TravelTo(int buttonID)
    {
        Debug.Log("Attemtping to travel to " + mapLocations[buttonID]);
        travelConfirmPopup.SetActive(true);
        LocationName.text = mapLocations[buttonID] + "?";
        SelectedLocation = buttonID;
    }

    public void TravelCancel()
    {
        travelConfirmPopup.SetActive(false);
    }

    public void TravelConfirm()
    {
        SceneManager.LoadScene(scenes[SelectedLocation].ToString(), LoadSceneMode.Single);
    }

}
