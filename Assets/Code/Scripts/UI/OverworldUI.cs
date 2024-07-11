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

    public PetMenu petMenu;
    public InventoryHandler invHandler;

    public string[] scenes;
    [SerializeField] Button[] mainUIButtons;
    public GameObject[] mainUI;
    int currentUI;
    [Header("UI GameObjects")]
    public GameObject petUI;
    public GameObject inventory;
    public GameObject PDA;
    public GameObject userInfoMenu;
    public GameObject map;
    public GameObject scrapbook;
    public GameObject nothing;
    public GameObject HouseConfirm;
    public GameObject messagebar;

    public Image petBarIcon;

    public GameObject GemDiving;


    [Header("Pink Pet Case")]
    [SerializeField] GameObject SideInv;
    [SerializeField] Button SideInvButton;

    [Header("Map")]
    [SerializeField] GameObject travelConfirmPopup;
    int SelectedLocation;
    [SerializeField] string[] mapLocations;
    [SerializeField] Button[] mapIcons;
    [SerializeField] TMPro.TextMeshProUGUI LocationName;

    [Header("General")]
    public TextPopup textPopup;
    public GameObject player;

    // temporary testing list
    public int maxPageCount = 1;
    public int petCount;
    public int finalPetScreenCount;
    public string[] petlist = new string[]
    {
        "Mew",
        "DJ",
        "Princess",
        "Juniper",
        "Holly",
        "Superstar",
        "cheeseburger",
        "Jackie",
        "Willow",
        "Mienfoo",
        "Arlecchino",
        "Miku",
        "Len",
        "Rin",
        "GUMI"
    }; 
    public Sprite[] petSprites;
    public bool[] petMemberStatus;

    public class MapIcon
    { public string AreaName; }

    // Start is called before the first frame update
    void Awake()
    {
        petUI.SetActive(true);
        userInfoMenu.SetActive(false);
        inventory.SetActive(false);
        PDA.SetActive(false);
        messagebar.SetActive(true);
        map.SetActive(false);
        scrapbook.SetActive(false);
        nothing.SetActive(false);
        SideInvButton.interactable = false;
    }

    void Update() {
        kibblecount.SetText($"{GameDataManager.Instance.kibble:n0}");
        kibblecountinv.SetText($"{GameDataManager.Instance.kibble:n0}");
    }

    public void UIToggles(int buttonID)
    {
        currentUI = buttonID;
        if (buttonID == 0)
        {
            HouseConfirm.SetActive(false);
            petMenu.pageCount = 1;
            // TODO: make default pet whatever pet youre playing as
            petMenu.petMenuUpdate();
        }
        else if (buttonID == 3)
        {
            travelConfirmPopup.SetActive(false);
        }
        else if (buttonID == 5)
        {
            invHandler.pageCount = 1;
            invHandler.inventoryUpdate();
            // if (inventory.activeSelf)
            // {
            //    invHandler.swoopOut();
            // }
            //else
            // {
            //    invHandler.swoopIn();
            // }
        }
        if (buttonID != 5)
        {
            inventory.SetActive(false);
        }
        mainUI[buttonID].SetActive(!mainUI[buttonID].activeSelf);
    }
    // TODO: add side inventory logic


    public void Popup(string str)
    {
        textPopup.SpawnText(player.transform.position.x, player.transform.position.y, str, textPopup.ArialBlack, textPopup.ABGreen, 40, 0.2f, 0.5f, 1, 5);
    }

    public void TravelTo(int buttonID)
    {
        travelConfirmPopup.SetActive(true);
        LocationName.text = mapLocations[buttonID] + "?";
        SelectedLocation = buttonID;
    }
    public void TravelConfirm()
    {
        GameDataManager.Instance.OldLocation = player.transform.position;
        SceneManager.LoadScene(scenes[SelectedLocation].ToString(), LoadSceneMode.Single);
    }

}
