using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IsoMath : MonoBehaviour
{
    static float tilewidth = 1.06f;
    static float tileheight = 0.54f;

    public static Vector2 screenPos(float x, float y, float xOrigin, float yOrigin){
        float xpos = (x+xOrigin) * (tilewidth/2) - (y+yOrigin) * (tilewidth/2);
        float ypos = (x+xOrigin) * (tileheight/2) + (y+yOrigin) * (tileheight/2);
        Vector2 position = new Vector2(xpos,ypos);
        return position;
    }

    public static Vector2 tilePos(float x, float y, float xOrigin, float yOrigin){
        Vector2 position;
        position.x = Mathf.Round((x / (tilewidth/2) + y / (tileheight/2))/2 -xOrigin);
        position.y = Mathf.Round((y / (tileheight/2) - x / (tilewidth/2))/2 -yOrigin);
        return position;
    }
}
