using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using System;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public bool Loaded {get; private set;}
    public List<ItemData> itemList;
    public string displayName;
    public List<int> inventory = new List<int>();
    public List<int> invItemCounts = new List<int>();
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
    public int fshnhighscore = 0;

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
        data.displayName = displayName;
	    data.inventory = inventory;
        data.invItemCounts = invItemCounts;
        data.rotationData = rotationData;
        data.levelData = levelData;
	    data.kibble = kibble;
        data.pets = pets;
        data.currentPetIndex = currentPetIndex;

        data.mnmhighscore = mnmhighscore;
        data.pdhighscore = pdhighscore;
        data.fshnhighscore = fshnhighscore;
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
            displayName = data.displayName;
            if (data.inventory != null){
		        inventory = data.inventory;
                invItemCounts = data.invItemCounts;
            }
            rotationData = data.rotationData;
            levelData = data.levelData;
	        kibble = data.kibble;
            pets = data.pets ?? pets;
            currentPetIndex = data.currentPetIndex;
            mnmhighscore = data.mnmhighscore;
            pdhighscore = data.pdhighscore;
            fshnhighscore = data.fshnhighscore;
            Debug.Log("Game data loaded!");
	    }
	    else
		    Debug.LogError("There is no save data!");

        Loaded = true;
    }
    
    public void AddInventory(int ID)
    {
        if (GameDataManager.Instance.inventory.Contains(ID))
        {
            Debug.Log($"{ID} in inventory, adding to item count");
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == ID)
                {
                    invItemCounts[i] += 1; break;
                }
            }
        }
        else
        {
            GameDataManager.Instance.inventory.Add(ID);
            GameDataManager.Instance.invItemCounts.Add(1);
            Debug.Log(inventory.Count + "\n" + invItemCounts.Count);
        }
    }
    
    public void RemoveInventory(int ID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == ID)
            {
                invItemCounts[i]--;
                if (invItemCounts[i] < 1)
                {
                    GameDataManager.Instance.inventory.Remove(ID);
                    GameDataManager.Instance.invItemCounts.Remove(invItemCounts[i]);
                }
                break;
            }
        }
    }

    public void AddKibble(int kibble)
    {
        GameDataManager.Instance.kibble += kibble;
    }

    public void SubtractKibble(int kibble)
    {
        GameDataManager.Instance.kibble -= kibble;
    }

    public void AddStat(int amount, ItemData.itemCategory type)
    {
        if (type == ItemData.itemCategory.Food)
        {
            if (GameDataManager.Instance.CurrentPet.foodLevel <= 100 - amount)
            {
                GameDataManager.Instance.CurrentPet.foodLevel += amount;
            }
            else
            {
                int overflow = amount + GameDataManager.Instance.CurrentPet.foodLevel;
                int reverse = overflow - 100;
                GameDataManager.Instance.CurrentPet.foodLevel += amount - reverse;
            }
        }
        else if (type == ItemData.itemCategory.Toy)
        {
            if (GameDataManager.Instance.CurrentPet.foodLevel <= 100 - amount)
            {
                GameDataManager.Instance.CurrentPet.foodLevel += amount;
            }
            else
            {
                int overflow = amount + GameDataManager.Instance.CurrentPet.foodLevel;
                int reverse = overflow - 100;
                GameDataManager.Instance.CurrentPet.foodLevel += amount - reverse;
            }
        }
        else if (type == ItemData.itemCategory.Grooming)
        {
            if (GameDataManager.Instance.CurrentPet.foodLevel <= 100 - amount)
            {
                GameDataManager.Instance.CurrentPet.foodLevel += amount;
            }
            else
            {
                int overflow = amount + GameDataManager.Instance.CurrentPet.foodLevel;
                int reverse = overflow - 100;
                GameDataManager.Instance.CurrentPet.foodLevel += amount - reverse;
            }
        }
    }
}
