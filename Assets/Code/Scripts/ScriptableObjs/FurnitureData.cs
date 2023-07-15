using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureData", menuName = "ScriptableObjects/FurnitureData", order = 1)]
public class FurnitureData : ScriptableObject
{
    public GameObject[] objects;

    public string itemName;
    public Sprite icon;
    public int price;
    public string category;
    public int rotation;
}
