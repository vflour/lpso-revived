using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public List<FurnitureData> furniture;
    public List<FurnitureData> inventory;
    public int kibble = 100;
    public int[,] levelData = new int[10,10];
    public int[,] rotationData = new int[10,10];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for(int i = 1; i<furniture.Count; i++){
            furniture[i].ID = i;
        }

        loadGame();
    }
    
    public void saveGame(){
        BinaryFormatter bf = new BinaryFormatter(); 
	    FileStream file = File.Create(Application.persistentDataPath  + "/MySaveData.dat"); 
	    SaveData data = new SaveData();
	    //data.inventory = inventory;
        data.rotationData = rotationData;
        data.levelData = levelData;
	    data.kibble = kibble;
	    bf.Serialize(file, data);
	    file.Close();
	    Debug.Log("Game data saved!");
    }

    public void loadGame(){
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
	    {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
		SaveData data = (SaveData)bf.Deserialize(file);
		file.Close();
		//inventory = data.inventory;
        rotationData = data.rotationData;
        levelData = data.levelData;
	    kibble = data.kibble;
		Debug.Log("Game data loaded!");
	}
	else
		Debug.LogError("There is no save data!");
    }

    public void AddInventory(FurnitureData item)
    {
        GameDataManager.Instance.inventory.Add(item);
    }
    
    public void RemoveInventory(FurnitureData item)
    {
        GameDataManager.Instance.inventory.Remove(item);
    }

    public void AddKibble(int kibble)
    {
        GameDataManager.Instance.kibble += kibble;
    }

    public void SubtractKibble(int kibble)
    {
        GameDataManager.Instance.kibble -= kibble;
    }
}
