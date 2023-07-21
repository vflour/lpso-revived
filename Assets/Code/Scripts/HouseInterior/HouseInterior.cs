using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameObject[,] rooms = new GameObject[3,3];
    public GameObject grass;
    public GameObject room_square;
    private float size = 10f;
    public GameObject cursorTile;
    public Vector2 selection;

    public Camera cam;
    
    public float xOrigin;
    public float yOrigin;
    public int selectedFurniture;

    public GameObject currentItem;
    public int currentID;
    public GameObject inventoryButton;
    public GameObject[] inventoryButtonsGUI;
    public GameObject canvas;
    public List<ItemData> FurnitureList;

    // Start is called before the first frame update
    void Start()
    {
        GameDataManager GameData = GameDataManager.Instance;
        Transform temp_transform = this.transform;
        //generate grass around the house
        for (int x = 0; x<=2; x++){
            for (int y = 0; y<=2; y++){
                Vector2 pos = IsoMath.screenPos(x * size,y * size,xOrigin,yOrigin);
                GameObject tempGrass = Instantiate(grass, temp_transform);
                tempGrass.transform.position = new Vector3(pos.x,pos.y,pos.y+3f);
            }
        }

        

        for (int x = 0; x < 10; x++){
            for (int y = 0; y < 10; y++){
                Debug.Log("placing " + x + "/" + y);
                if(GameData.levelData[x,y] > 0){
                    GameObject tempFurniture = Instantiate(GameData.itemList[GameData.levelData[x,y]].objects[0]);
                    tempFurniture.transform.position = IsoMath.screenPos(x,y,xOrigin,yOrigin);
                    tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - y;
                }
            }
        }
        //generate the first room
        rooms[0,0] = Instantiate(room_square);
        rooms[0,0].transform.position = new Vector3(0,0,-5);

        //adds a button for each inventory item
        SetButtons();
    }

    void SetButtons(){
        Debug.Log(GameDataManager.Instance.inventory.Count);
        FurnitureList.Clear();
        for(int i = 0; i < GameDataManager.Instance.inventory.Count; i++){
            if(GameDataManager.Instance.inventory[i].category == "furniture"){
                FurnitureList.Add(GameDataManager.Instance.inventory[i]);
            }
        }
        for(int i = 0; i < inventoryButtonsGUI.Length; i++){
            Debug.Log(i);
            Debug.Log("Attempt to add Inventory Button " + i);
            addInventoryButton(i);
        }
    }

    
    void addInventoryButton(int invSlot){
        
        if(invSlot < FurnitureList.Count){
            inventoryButtonsGUI[invSlot].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = FurnitureList[invSlot].icon;
            inventoryButtonsGUI[invSlot].transform.GetChild(0).gameObject.SetActive(true);
            inventoryButtonsGUI[invSlot].GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log(invSlot);
            currentItem = FurnitureList[invSlot].objects[0];
            currentID = FurnitureList[invSlot].ID;
            selectedFurniture = invSlot;
        }); }else {
            inventoryButtonsGUI[invSlot].transform.GetChild(0).gameObject.SetActive(false);
        }
     }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
        cursorTile.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);
    	GameDataManager GameData = GameDataManager.Instance;

        //place furniture item on mouse click (should be by dragging)
        if (Input.GetMouseButtonDown(0) && currentItem != null){
            Vector2 selectedTile = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
            Debug.Log(selectedTile);
            if( selectedTile.x >= 0 && selectedTile.x < GameData.levelData.GetLength(0) &&
                selectedTile.y >= 0 && selectedTile.y < GameData.levelData.GetLength(1) &&
                GameData.levelData[(int)selectedTile.x,(int)selectedTile.y] == 0){
                GameObject tempFurniture = Instantiate(currentItem);
                tempFurniture.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);
                tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - (int)pos.y;
                GameData.levelData[(int)selectedTile.x,(int)selectedTile.y] = currentID;
                GameData.inventory.Remove(GameDataManager.Instance.inventory[selectedFurniture]);
                currentItem = null;
            }
            SetButtons();
        }

        //rotate the item on click
        //if (Input.GetMouseButtonDown(0) && currentItem == null){
        //    Vector2 selectedTile = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
        //    Debug.Log(selectedTile);
        //    if( selectedTile.x >= 0 && selectedTile.x < GameDataManager.Instance.levelData.GetLength(0) &&
        //        selectedTile.y >= 0 && selectedTile.y < GameDataManager.Instance.levelData.GetLength(1) &&
        //        GameDataManager.Instance.levelData[(int)selectedTile.x,(int)selectedTile.y] != null){
        //        GameObject oldFurniture = GameDataManager.Instance.levelData[(int)selectedTile.x,(int)selectedTile.y];
        //        GameObject tempFurniture = Instantiate(oldFurniture.GetComponent<FurnitureProperties>().nextObject);
        //        tempFurniture.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);
        //        tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - (int)pos.y;
        //        GameDataManager.Instance.levelData[(int)selectedTile.x,(int)selectedTile.y] = tempFurniture.invSlot;
        //        Destroy(oldFurniture);
        //    }
        //}
    }

    void SetCurrentItem(int invSlot){
        Debug.Log(invSlot);
        currentItem = GameDataManager.Instance.inventory[invSlot].objects[0];
    }
}
