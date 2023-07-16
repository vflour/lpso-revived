using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPivotSort : MonoBehaviour
{
    // TODO: Make this an editor thing
    public void Start()
    {
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.spriteSortPoint = SpriteSortPoint.Pivot;
            renderer.sortingLayerName = "Character";
            renderer.sortingOrder = 0;
        }
    }
}
