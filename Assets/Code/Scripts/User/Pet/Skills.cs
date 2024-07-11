using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    int gemLvlThreshold;
    int luckyLvlThreshold;

    private Pet pet;

    private void Start()
    {
        pet = GameDataManager.Instance.CurrentPet;
    }

    public void GemSkillUp()
    {
        if (!pet.gemMaxLvlReached)
        {
            gemLvlThreshold = pet.gemLevel * 4;
            pet.gemExp += 1;
            if (pet.gemExp == gemLvlThreshold)
            {
                pet.gemExp = 0;
                pet.gemLevel += 1;
                if (pet.gemLevel == 7)
                {
                    pet.gemMaxLvlReached = true;
                }
                else
                {
                    gemLvlThreshold = pet.gemLevel * 4;
                }
            }
        }
    }

    public void LuckSkillUp()
    {
        if (!pet.luckMaxLvlReached)
        {
            luckyLvlThreshold = pet.luckyLevel * 4;
            pet.luckyExp += 1;
            if (pet.luckyExp == luckyLvlThreshold)
            {
                pet.luckyExp = 0;
                pet.luckyLevel += 1;
                if (pet.luckyLevel == 7)
                {
                    pet.luckMaxLvlReached = true;
                }
                else
                {
                    luckyLvlThreshold = pet.luckyLevel * 4;
                }
            }
        }
    }
}
