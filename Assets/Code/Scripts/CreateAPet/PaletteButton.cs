using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PaletteButton : MonoBehaviour
{
    public PetPaletteType paletteType;
    public PaletteColor _color;
    public PetSpriteColorizer colorizer;
    
    public PaletteColor Color {
        get { return _color; }
        set 
        {  
           _color = value;
           GetComponent<Image>().color = _color.color;
        }
    }

    public void UpdatePalette()
    {
        colorizer.SwitchPalette(paletteType, Color);
    }

}
