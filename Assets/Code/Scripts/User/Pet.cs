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

    public string species;
    public int subSpecies;
    
    public Dictionary<PetPaletteType, Color> colors = new Dictionary<PetPaletteType, Color> {
        { PetPaletteType.Coat, Color.white },
        { PetPaletteType.Patch, Color.white },
        { PetPaletteType.Eye, Color.white }
    };
 
    public GameObject rig;

}
