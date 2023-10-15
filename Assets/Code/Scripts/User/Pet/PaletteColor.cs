using UnityEngine;
using System;

[Serializable]
public class PaletteColor 
{
    
    public SerializableColor color;
    public float saturationMultiplier;

    public static PaletteColor white = new PaletteColor(SerializableColor.white, 1);
    public PaletteColor(SerializableColor color, float saturationMultiplier)
    {
        this.color = color;
        this.saturationMultiplier = saturationMultiplier;
    }

}
