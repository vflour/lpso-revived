using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCharacter : MonoBehaviour
{
    public PetSpriteAttributes petSpriteAttributes;
    public PetSpritePalettes petSpritePalettes;
    public Pet pet;

    public void Draw()
    {
        DrawSprites();
        DrawColors();
    }

    public void DrawSprites()
    {

        foreach (var kvp in pet.attributes)
            DrawSprite(kvp.Key);
    }

    public void UpdateSprite(PetAttributeType petAttributeType, int petAttribute)
    {
        pet.attributes[petAttributeType] = petAttribute;
        DrawSprite(petAttributeType);
    }

    public void DrawSprite(PetAttributeType petAttributeType)
    {
        DrawSprite(petAttributeType, pet.attributes[petAttributeType]);
    }

     public void DrawSprite(PetAttributeType petAttributeType, int spriteIndex)
    {
        petSpriteAttributes.Resolve(petAttributeType, spriteIndex);
    }


    public void DrawColors()
    {
        foreach (var kvp in pet.colors)
            DrawColor(kvp.Key);
    }

    public void UpdateColor(PetPaletteType petPaletteType, PaletteColor paletteColor)
    {
        pet.colors[petPaletteType] = paletteColor;
        DrawColor(petPaletteType);
    }

    public void DrawColor(PetPaletteType petPaletteType)
    {
        DrawColor(petPaletteType, pet.colors);
    }

    public void DrawColor(PetPaletteType petPaletteType, PetPaletteDict paletteColor)
    {
       petSpritePalettes.UpdatePalette(petPaletteType, paletteColor); 
    }
    
}
