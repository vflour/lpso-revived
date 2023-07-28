using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;

public class ObjectPivotSort : MonoBehaviour
{
    // TODO: Make this an editor thing
    public void Start()
    {
        SpriteSorterForEachChild(transform);
    }


    void SpriteSorterForEachChild(Transform trans){
        foreach(Transform child in trans)
        {
            
            
            SuperCustomProperties properties = child.gameObject.GetComponent<SuperCustomProperties>();
            SuperObject obj = child.gameObject.GetComponent<SuperObject>();

            if (properties != null){

                IsoSpriteSorting IsoSort = child.gameObject.AddComponent<IsoSpriteSorting>();

                CustomProperty isLine;
                CustomProperty x1;
                CustomProperty x2;
                CustomProperty y1;
                CustomProperty y2;
                CustomProperty below;
            
                properties.TryGetCustomProperty("isLine",out isLine);
                properties.TryGetCustomProperty("x1",out x1);
                properties.TryGetCustomProperty("x2",out x2);
                properties.TryGetCustomProperty("y1",out y1);
                properties.TryGetCustomProperty("y2",out y2);
                properties.TryGetCustomProperty("below",out below);
                
                if(below != null){
                    IsoSort.renderBelowAll = true;
                }

                if(isLine != null){
                    IsoSort.sortType = IsoSpriteSorting.SortType.Line;
                } else {
                    IsoSort.sortType = IsoSpriteSorting.SortType.Point;
                }
            if(obj != null && obj.m_Rotation == 0){
                if(x1 != null && y1 != null){
                    IsoSort.SorterPositionOffset = new Vector3(float.Parse(x1.m_Value),float.Parse(y1.m_Value),0);
                 } else
                 {
                    IsoSort.SorterPositionOffset = new Vector3(0,0,0);
                 }
                 if(x2 != null && y2 != null){
                      IsoSort.SorterPositionOffset2 = new Vector3(float.Parse(x2.m_Value),float.Parse(y2.m_Value),0);
                  }
            } else if(obj != null && obj.m_Rotation == 180)  {
                if(x1 != null && y1 != null){
                    IsoSort.SorterPositionOffset = new Vector3(-float.Parse(x1.m_Value),float.Parse(y1.m_Value)-1.7f,0);
                 } else
                 {
                    IsoSort.SorterPositionOffset = new Vector3(0,0,0);
                 }
                 if(x2 != null && y2 != null){
                      IsoSort.SorterPositionOffset2 = new Vector3(-float.Parse(x2.m_Value),float.Parse(y2.m_Value)-1.7f,0);
                  }
            }
            } else {
                //IsoSort.sortType = IsoSpriteSorting.SortType.Point;
                //IsoSort.SorterPositionOffset = new Vector3(0,0,0);
            }
            SpriteSorterForEachChild(child.gameObject.transform);
        }
    }
}
