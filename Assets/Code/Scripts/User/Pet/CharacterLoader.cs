using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    public Character character;
    
    public void Load()
    {
        var petCharacter = character.currentCharacterObject.GetComponent<PetCharacter>();
        petCharacter.pet = GameDataManager.Instance.CurrentPet;
        petCharacter.Draw();
    }
}
