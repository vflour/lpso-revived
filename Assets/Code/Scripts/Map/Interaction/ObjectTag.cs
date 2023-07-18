using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTag : MonoBehaviour
{
    public ObjectMetadata metadata;
    
    public void Start()
    {
        GameObject container = gameObject;
        if (metadata.behaviour)
        {
            container = Instantiate(metadata.behaviour, transform);
            Destroy(GetComponent<PolygonCollider2D>());
        }
        var tooltip = container.AddComponent<ObjectTooltip>();
        tooltip.metadata = metadata;

        var cursorModifier = container.AddComponent<MapCursorModifier>();
        cursorModifier.cursorType = CursorType.Interact;
    }
}
