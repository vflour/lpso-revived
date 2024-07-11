using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFunComfort : MonoBehaviour
{
    public float DecreaseStatTimer;
    private float Timer;

    private void Awake()
    {
        Timer = 20.0f;
        DecreaseStatTimer = Timer;
    }

    void Update()
    {
        if (DecreaseStatTimer > 0)
        {
            DecreaseStatTimer -= Time.deltaTime;
        }
        else
        {
            if (GameDataManager.Instance.CurrentPet.foodLevel > 0)
            {
                GameDataManager.Instance.CurrentPet.foodLevel -= 1;
            }
            if (GameDataManager.Instance.CurrentPet.funLevel > 0)
            {
                GameDataManager.Instance.CurrentPet.funLevel -= 1;
            }
            if (GameDataManager.Instance.CurrentPet.comfortLevel > 0)
            { 
                GameDataManager.Instance.CurrentPet.comfortLevel -= 1;
            }
            DecreaseStatTimer = Timer;
        }
    }
}
