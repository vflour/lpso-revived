using UnityEngine;

public class PlayerCharacter : Character
{

    public PetDatabase database;
    private Pet currentPet => GameDataManager.Instance.CurrentPet;
    private PetData petSpeciesData => database[currentPet.species];

    protected override GameObject GetCharacterModelPrefab()
    {
        var characterPrefab = petSpeciesData.sprites[currentPet.subSpecies];
        return Instantiate(characterPrefab, characterParent);
    }
}