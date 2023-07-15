using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogLogic : MonoBehaviour
{
    public GameObject nextState;
    public bool North;
    public bool East;
    public bool South;
    public bool West;
    public bool Walkable;
    public bool isPath;
    public bool isStart;
    public bool foundPath;
    public bool tempPath;
    public bool isEnd;
    public int x;
    public int y;
    public bool stable;

    public List<GameObject> breadcrumb;
    public List<GameObject> nextTiles;
    public Color NormalColor;
    public Color WalkableColor;
    public Color PathColor;

    public int level = 100;
    void Update() {
        if (Walkable && !isPath){
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = WalkableColor;
        } else if (Walkable && (isPath)) {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = PathColor;
        } else {
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = NormalColor;
        }
    }
}
