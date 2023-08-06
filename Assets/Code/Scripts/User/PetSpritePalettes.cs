using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetSpritePalettes : MonoBehaviour
{
    public SpritePaletteDict spritePalettes;

    public void UpdatePalette(PetPaletteType paletteType, PaletteColor color)
    {
        foreach (SpriteRenderer sprite in spritePalettes[paletteType])
        {
            sprite.GetComponent<SatSpriteRenderer>().PaletteColor = color;
        }
    }

}

[Serializable]
public class SpriteListStorage : SerializableDictionary.Storage<List<SpriteRenderer>> {}
[Serializable]
public class SpritePaletteDict : SerializableDictionary<PetPaletteType, List<SpriteRenderer>, SpriteListStorage> {}

