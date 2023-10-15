using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TooltipData : ScriptableObject
{
    /// <summary>
    /// Title of the tooltip item
    /// </summary>
    public string title;

    /// <summary>
    /// Icon for the tooltip item
    /// </summary>
    public Sprite icon;

    /// <summary>
    /// The type/category of the tooltip item.
    /// Won't appear if empty.
    /// </summary>
    public string category;

    /// <summary>
    /// The description of the item. 
    /// Won't appear if empty.
    /// </summary>
    public string description;

    /// <summary>
    /// Will show the member disclaimer if set to true
    /// </summary>
    public bool hasMemberDescription;
}
