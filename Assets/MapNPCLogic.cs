using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNPCLogic : MonoBehaviour
{
    public int scene;
    public GameObject uiReference;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
    // Update is called once per frame
    if(Input.GetMouseButtonDown(0)){
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            GameDataManager GameData = GameDataManager.Instance;

             if(hit.collider == this.GetComponent<BoxCollider2D>())
            {
                Debug.Log("Clicked on NPC");
                uiReference.GetComponent<OverworldUI>().TravelTo(scene);
            }
         }

    }
}
