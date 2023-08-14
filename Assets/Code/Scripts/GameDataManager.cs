using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public bool Loaded {get; private set;}
    public List<ItemData> itemList;
    public List<int> inventory = new List<int>();
    public int kibble = 100;
    public int[,] levelData = new int[10,10];
    public int[,] rotationData = new int[10,10];
    public Vector3 OldLocation;
    public bool FreshSpawn = true;
    public List<Pet> pets = new List<Pet>();
    public int currentPetIndex = 0;    
    public Pet CurrentPet => pets[currentPetIndex];

    public int mnmhighscore = 0;
    public int pdhighscore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for(int i = 1; i<itemList.Count; i++){
            itemList[i].ID = i;
        }

        loadGame();
    }
    
    public void saveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
	    FileStream file = File.Create(Application.persistentDataPath  + "/MySaveData.dat"); 
	    SaveData data = new SaveData();
	    data.inventory = inventory;
        data.rotationData = rotationData;
        data.levelData = levelData;
	    data.kibble = kibble;
        data.pets = pets;
        data.currentPetIndex = currentPetIndex;

        data.mnmhighscore = mnmhighscore;
        data.pdhighscore = pdhighscore;
	    bf.Serialize(file, data);
	    file.Close();
	    Debug.Log("Game data saved!");
    }

    public void loadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
	    {
		    BinaryFormatter bf = new BinaryFormatter();
		    FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
		    SaveData data = (SaveData)bf.Deserialize(file);
		    file.Close();
            if (data.inventory != null){
		        inventory = data.inventory;
            }
            rotationData = data.rotationData;
            levelData = data.levelData;
	        kibble = data.kibble;
            pets = data.pets ?? pets;
            currentPetIndex = data.currentPetIndex;
            mnmhighscore = data.mnmhighscore;
            pdhighscore = data.pdhighscore;
		    Debug.Log("Game data loaded!");
	    }
	    else
		    Debug.LogError("There is no save data!");

        Loaded = true;
    }
    
    public void AddInventory(int ID)
    {
        GameDataManager.Instance.inventory.Add(ID);
    }
    
    public void RemoveInventory(int ID)
    {
        GameDataManager.Instance.inventory.Remove(ID);
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
