using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;
using System;

public class ObjectTagger : MonoBehaviour
{
    public ObjectMetadataList metadataList;
    public GameObject mapObject;

    private ObjectMetadata GetMetadata(string name) 
    {
        ObjectMetadata metadata = Array.Find(metadataList.list, data => data.name == name);
        if (!metadata) Debug.LogWarning($"No metadata exists for {name}");
        return metadata;
    }

    public void Start()
    {
        foreach (SuperCustomProperties properties in mapObject.GetComponentsInChildren<SuperCustomProperties>())
        {
            CustomProperty property;
            bool hasProperty = properties.TryGetCustomProperty("Type", out property);
            
            if (hasProperty)
            {   
                var metadata = GetMetadata(property.m_Value);
                if (metadata)
                {
                    var collider = properties.GetComponentInChildren<PolygonCollider2D>();
                    if (!collider)
                    {
                        Debug.LogError($"Missing collision data for: {metadata.name}");
                        continue;
                    }
                    ObjectTag tag = collider.gameObject.AddComponent<ObjectTag>();
                   
                    tag.metadata = metadata;
                }
            }
        }
    }
}
