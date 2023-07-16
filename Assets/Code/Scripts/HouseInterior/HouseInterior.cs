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
    
    public int inventoryID;
    public Camera cam;
    
    public float xOrigin;
    public float yOrigin;

    public GameObject currentItem;
    public GameObject inventoryButton;

    public GameObject canvas;
    public GameObject[,] furniture = new GameObject[10,10];
    public GameObject[,] level = new GameObject[10,10];
    // Start is called before the first frame update
    void Start()
    {
        Transform temp_transform = this.transform;
        //generate grass around the house
        for (int x = 0; x<=2; x++){
            for (int y = 0; y<=2; y++){
                Vector2 pos = IsoMath.screenPos(x * size,y * size,xOrigin,yOrigin);
                GameObject tempGrass = Instantiate(grass, temp_transform);
                tempGrass.transform.position = new Vector3(pos.x,pos.y,pos.y+3f);
            }
        }

        //generate the first room
        rooms[0,0] = Instantiate(room_square);
        rooms[0,0].transform.position = new Vector3(0,0,-5);
        
        currentItem = Instantiate(GameDataManager.Instance.inventory[0].objects[0]);

        //adds a button for each inventory item
        for(int i = 0; i < GameDataManager.Instance.inventory.Count-1; i++){
            addInventoryButton(i);
        }
    }

    
    void addInventoryButton(int ID){
        GameObject tempButton = Instantiate(inventoryButton);
        tempButton.transform.position = new Vector3(520+ID*64,40,0);
        tempButton.transform.parent = canvas.transform;
        tempButton.GetComponent<Image>().sprite = GameDataManager.Instance.inventory[ID].icon;
        tempButton.GetComponent<Button>().onClick.AddListener(() => {
        Debug.Log(ID);
        currentItem = GameDataManager.Instance.inventory[ID].objects[0];
     });
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
        cursorTile.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);

        //place furniture item on mouse click (should be by dragging)
        if (Input.GetMouseButtonDown(0) && currentItem != null){
            Vector2 selectedTile = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
            Debug.Log(selectedTile);
            if( selectedTile.x >= 0 && selectedTile.x < furniture.GetLength(0) &&
                selectedTile.y >= 0 && selectedTile.y < furniture.GetLength(1) &&
                furniture[(int)selectedTile.x,(int)selectedTile.y] == null){
                GameObject tempFurniture = Instantiate(currentItem);
                tempFurniture.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);
                tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - (int)pos.y;
                furniture[(int)selectedTile.x,(int)selectedTile.y] = tempFurniture;
                currentItem = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && currentItem == null){
            Vector2 selectedTile = IsoMath.tilePos(mousePos.x,mousePos.y,xOrigin,yOrigin);
            Debug.Log(selectedTile);
            if( selectedTile.x >= 0 && selectedTile.x < furniture.GetLength(0) &&
                selectedTile.y >= 0 && selectedTile.y < furniture.GetLength(1) &&
                furniture[(int)selectedTile.x,(int)selectedTile.y] != null){
                GameObject oldFurniture = furniture[(int)selectedTile.x,(int)selectedTile.y];
                GameObject tempFurniture = Instantiate(oldFurniture.GetComponent<FurnitureProperties>().nextObject);
                tempFurniture.transform.position = IsoMath.screenPos(pos.x,pos.y,xOrigin,yOrigin);
                tempFurniture.GetComponent<SpriteRenderer>().sortingOrder = 20 - (int)pos.y;
                furniture[(int)selectedTile.x,(int)selectedTile.y] = tempFurniture;
                Destroy(oldFurniture);
            }
        }
    }

    void SetCurrentItem(int ID){
        Debug.Log(ID);
        currentItem = GameDataManager.Instance.inventory[ID].objects[0];
    }
}
