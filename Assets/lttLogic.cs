using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lttLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[,] level;
    public GameObject shell;
    public GameObject[] icons;
    public GameObject cursor;
    public Camera Camera;

    public List<int[]> shells;

    public Vector2 shellOffset = new Vector2(-6.3f,-3.6f);

    void Start()
    {
        int[,] map = {{0,0,0,0,0,0,0,0,0,0},{0,1,1,0,0,0,0,1,1,0},{0,1,1,0,0,0,0,1,1,0},{0,1,1,0,0,0,0,1,1,0},{0,0,1,1,1,1,1,1,0,0},{0,0,0,0,1,1,0,0,0,0},{0,0,0,1,1,1,1,0,0,0},{0,0,1,1,1,1,1,1,0,0}};
     
        for (int x = 0; x < 10; x++){
            for (int y = 0; y < 8; y++){
                if (map[7-y,9-x] == 1){
                    var tile = Instantiate(shell,this.transform);
                    tile.transform.position = new Vector2(x + shellOffset.x,y + shellOffset.y);
                    tile.GetComponent<shellLogic>().icon.GetComponent<SpriteRenderer>().sprite = icons[Random.Range(0,icons.Length)].GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Destroy(objectHit.gameObject);
        }
    }
}
