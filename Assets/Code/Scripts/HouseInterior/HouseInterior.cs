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
    //selected in inventory
    public int selectedFurniture;
    //selected in room for editing
    public Vector2 focussedFurniture;

    public GameObject currentItem;
    public int currentID;
    public GameObject inventoryButton;
    public GameObject[] inventoryButtonsGUI;
    public GameObject canvas;
    public List<ItemData> FurnitureList;
    public GameObject[,] furniture = new GameObject[10,10];
    public GameObject ItemUIPanel;

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
                    GameObject tempFurniture = Instantiate(GameData.itemList[GameData.levelData[x,y]].objects[GameData.rotationData[x,y]]);
                    tempFurniture.transform.position = IsoMath.screenPos(x,y,xOrigin,yOrigin);
                    tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - y;
                    furniture[x,y] = tempFurniture;
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
        GameDataManager Data = GameDataManager.Instance;
        Debug.Log(Data.inventory.Count);
        FurnitureList.Clear();
        for(int i = 0; i < Data.inventory.Count; i++){
            if(Data.itemList[Data.inventory[i]].category == "furniture"){
                FurnitureList.Add(Data.itemList[Data.inventory[i]]);
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

        cursorTile.transform.position = IsoMath.screenPos(focussedFurniture.x,focussedFurniture.y,xOrigin,yOrigin);
        
        GameDataManager GameData = GameDataManager.Instance;
        Vector2 selectedTile = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);

        //place furniture item on mouse click (should be by dragging)
        if (Input.GetMouseButtonDown(0) && currentItem != null){
            Debug.Log(selectedTile);
            if( selectedTile.x >= 0 && selectedTile.x < GameData.levelData.GetLength(0) &&
                selectedTile.y >= 0 && selectedTile.y < GameData.levelData.GetLength(1) &&
                GameData.levelData[(int)selectedTile.x,(int)selectedTile.y] == 0){
                GameObject tempFurniture = Instantiate(currentItem);
                tempFurniture.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);
                tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - (int)pos.y;
                GameData.levelData[(int)selectedTile.x,(int)selectedTile.y] = currentID;
                furniture[(int)selectedTile.x,(int)selectedTile.y] = tempFurniture;
                GameData.inventory.Remove(currentID);
                currentItem = null;
            }
            SetButtons();
        }

        if (Input.GetMouseButtonDown(0)){
            if( selectedTile.x >= 0 && selectedTile.x < GameData.levelData.GetLength(0) &&
                selectedTile.y >= 0 && selectedTile.y < GameData.levelData.GetLength(1) &&
                GameData.levelData[(int)selectedTile.x,(int)selectedTile.y] > 0){
                focussedFurniture = selectedTile;
                //ItemUIPanel.transform.position = IsoMath.screenPos(focussedFurniture.x,focussedFurniture.y,xOrigin,yOrigin);
            }
            SetButtons();
        }
    }

    public void RemoveFurniture(){
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
    	GameDataManager GameData = GameDataManager.Instance;

        Destroy(furniture[(int)focussedFurniture.x,(int)focussedFurniture.y]);
        GameData.inventory.Add(GameData.levelData[(int)focussedFurniture.x,(int)focussedFurniture.y]);
        GameData.levelData[(int)focussedFurniture.x,(int)focussedFurniture.y] = 0;
        currentItem = null;
        SetButtons();
    }

    public void RotateFurniture(int direction){
    	GameDataManager GameData = GameDataManager.Instance;

        Destroy(furniture[(int)focussedFurniture.x,(int)focussedFurniture.y]);
        int x = (int)focussedFurniture.x;
        int y = (int)focussedFurniture.y;
        GameData.rotationData[x,y] += direction;
        if(GameData.rotationData[x,y] >= GameData.itemList[GameData.levelData[x,y]].objects.Length){
            GameData.rotationData[x,y] = 0;
        }
        if(GameData.rotationData[x,y] < 0){
            GameData.rotationData[x,y] = GameData.itemList[GameData.levelData[x,y]].objects.Length-1;
        }
        GameObject tempFurniture = Instantiate(GameData.itemList[GameData.levelData[x,y]].objects[GameData.rotationData[x,y]]);
        tempFurniture.transform.position = IsoMath.screenPos(x,y,xOrigin,yOrigin);
        tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - y;
        furniture[x,y] = tempFurniture;
        SetButtons();

    }

    void SetCurrentItem(int invSlot){
        Debug.Log(invSlot);
        GameDataManager Data = GameDataManager.Instance;
        currentItem = Data.itemList[Data.inventory[invSlot]].objects[0];
    }
}
