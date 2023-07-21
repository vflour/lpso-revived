using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public GameObject[] objects;

    public int ID;
    public string itemName;
    public Sprite icon;
    public int price;
    public string category;
    public GameObject rotations;
}
