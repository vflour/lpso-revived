using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class ObjectMetadata : ScriptableObject
{
    public GameObject behaviour;
    public TooltipData tooltipData;

    #if UNITY_EDITOR
    // Updates the metadata list in the same directory
    void OnValidate()
    {
        string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
        string[] guids = AssetDatabase.FindAssets("t:ObjectMetadataList");
        
        foreach (string guid in guids)
        {
            // get metadatalist in same dir
            string listPath = AssetDatabase.GUIDToAssetPath(guid);
            if (Path.GetDirectoryName(listPath) == path) 
            {
                ObjectMetadataList metadataList = AssetDatabase.LoadAssetAtPath<ObjectMetadataList>(listPath);
                if (metadataList)
                {
                    // this is so stupid but i can see arrays in the editor
                    // TODO: Change this when MetaDataList isn't in the editor
                    ObjectMetadata find = Array.Find(metadataList.list, metadata => metadata.name == this.name);
                    if (find == null)
                    {
                        var list = metadataList.list.Concat(new ObjectMetadata[] {this}).ToArray();
                        Debug.Log(list.Length);
                        metadataList.list = list;
                        EditorUtility.SetDirty(metadataList);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    } 
                }
            }
        }
    }
    #endif
}
