using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PetAttributeType 
{
    None,
    Gender,
    Head,
    Eyes,
    Mouth,
    Ears,
    Tail,
    Hair
}

public enum PetPaletteType 
{
    Coat,
    Patch,
    Eye
}

[Serializable]
public class Pet
{

    public Dictionary<PetAttributeType, int> attributes = new Dictionary<PetAttributeType, int> {
        { PetAttributeType.Gender,  0 },
        { PetAttributeType.Head,    0 },
        { PetAttributeType.Eyes,    0 },
        { PetAttributeType.Mouth,   0 },
        { PetAttributeType.Ears,    0 },
        { PetAttributeType.Tail,    0 },
        { PetAttributeType.Hair,    0 },
    };

    public string name;
    public string species;
    public int subSpecies;
    public DateTime adoptionDate = DateTime.Now;
    
    public Dictionary<PetPaletteType, PaletteColor> colors = new Dictionary<PetPaletteType, PaletteColor> {
        { PetPaletteType.Coat, PaletteColor.white },
        { PetPaletteType.Patch, PaletteColor.white},
        { PetPaletteType.Eye, PaletteColor.white }
    };
 

}

[Serializable]
public class PetPaletteDict : SerializableDictionary<PetPaletteType, PaletteColor> 
{

    public static implicit operator System.Collections.Generic.Dictionary<PetPaletteType, PaletteColor>(PetPaletteDict dict)
    {
        var newDict = new System.Collections.Generic.Dictionary<PetPaletteType, PaletteColor>();
        foreach (var kvp in dict)
            newDict[kvp.Key] = kvp.Value;
        return newDict;
    }

    public static implicit operator PetPaletteDict(System.Collections.Generic.Dictionary<PetPaletteType, PaletteColor> dict)
    {
        PetPaletteDict petDict = new PetPaletteDict();
        foreach (var kvp in dict)
            petDict[kvp.Key] = kvp.Value;
        return petDict;
    }

}

