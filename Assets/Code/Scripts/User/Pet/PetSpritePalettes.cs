using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PetSpritePalettes : MonoBehaviour
{
    private SatSpriteRenderer[] renderers;

    void Awake()
    {
        renderers = gameObject.GetComponentsInChildren<SatSpriteRenderer>();
    }

    public void UpdatePalette(PetPaletteType paletteType, PetPaletteDict color)
    {
        foreach (SatSpriteRenderer sprite in renderers)
        {
            sprite.GetComponent<SatSpriteRenderer>().PaletteColor = color;
        }
    }

}

[Serializable]
public class SpriteListStorage : SerializableDictionary.Storage<List<SpriteRenderer>> {}
[Serializable]
public class SpritePaletteDict : SerializableDictionary<PetPaletteType, List<SpriteRenderer>, SpriteListStorage> {}

