using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PetDatabase", menuName = "ScriptableObjects/PetDatabase", order = 3)]
public class PetDatabase : ScriptableObject 
{
    public PetData[] data;

    public PetData this[string name] => Array.Find(data, entry => entry.name == name); 
    public PetData this[int index] => data[index];
    
}
