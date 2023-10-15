using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveData
{
    //public List<FurnitureData> inventory;
    public int[,] rotationData;
    public int[,] levelData;
    public List<int> inventory;
	public int kibble;
    public List<Pet> pets;
    public int currentPetIndex;

    public int mnmhighscore = 0;
    public int pdhighscore = 0;
    
}
