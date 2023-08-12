using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PetPaletteType;

public class PetSpriteColorizer : MonoBehaviour
{
    public CreateAPetStands stands;

    public PetSpritePalettes SpritePalettes => stands.sprites[0].GetComponent<PetSpritePalettes>();

    public void SwitchPalette(PetPaletteType paletteType, PetPaletteDict color)
    {
        SwitchPalette(paletteType, color, SpritePalettes);
    }

    private PaletteColor GetDefaultPalette(PetPaletteType paletteType, PetData petData)
    {
        return petData.palette[paletteType][0];
    }

    public PetPaletteDict GetDefaultPalettes(PetData petData)
    {
        return new PetPaletteDict {
            [Coat] = GetDefaultPalette(Coat, petData),        
            [Patch] = GetDefaultPalette(Patch, petData),        
            [Eye] = GetDefaultPalette(Eye, petData),        
        };
    }

    public void SwitchAllPalettesToDefault(PetSpritePalettes palettes, PetData petData)
    {
        SwitchPaletteToDefault(palettes, petData, PetPaletteType.Coat); 
        SwitchPaletteToDefault(palettes, petData, PetPaletteType.Patch); 
        SwitchPaletteToDefault(palettes, petData, PetPaletteType.Eye); 
    }

    public void SwitchPaletteToDefault(PetSpritePalettes palettes, PetData petData, PetPaletteType paletteType)
    {
        SwitchPalette(paletteType, GetDefaultPalettes(petData), palettes); 
    }

    public void SwitchPalette(PetPaletteType paletteType, PetPaletteDict color, PetSpritePalettes palettes)
    {
        palettes.UpdatePalette(paletteType, color);
    }
}
