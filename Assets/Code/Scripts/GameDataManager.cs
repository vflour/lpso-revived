using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public List<FurnitureData> inventory;
    public int kibble = 100;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
