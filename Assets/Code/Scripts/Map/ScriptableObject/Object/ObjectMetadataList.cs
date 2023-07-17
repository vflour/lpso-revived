using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class ObjectMetadataList : ScriptableObject
{
    public ObjectMetadata[] list;

    void OnValidate()
    {
        //Debug.Log(AssetDatabase.GetAssetPath(this));
    }
}
