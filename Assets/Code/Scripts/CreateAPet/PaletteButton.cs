using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaletteButton : MonoBehaviour
{
    public PaletteColor _color;
    public PalettePageGenerator generator;
    public GameObject selected;
    
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
        generator.UpdatePetColor(_color);
    }

    public void ToggleSelected(bool isSelected)
    {
        selected.SetActive(isSelected);
    }

}
