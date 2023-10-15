using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Added only for the purposes of the vslice, remove later when u want more editable pets
/// </summary>
public class SinglePetResetter : MonoBehaviour
{
    void Start()
    {
        if (GameDataManager.Instance.pets.Count > 0)
        {
            GameDataManager.Instance.pets = new List<Pet>();
        }
    }
}
