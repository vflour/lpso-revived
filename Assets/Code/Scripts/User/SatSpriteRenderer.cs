using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SatSpriteRenderer : MonoBehaviour 
{
    private bool _dirty = false;
    public SpriteRenderer renderer;

    private PaletteColor _paletteColor;
    
    public PaletteColor PaletteColor 
    {
        set 
        { 
            _paletteColor = value; 
            _dirty = true;
        }
        get { return _paletteColor; }
    }
    
    public void Update()
    {
        if (_dirty && renderer != null)
        {
            _dirty = false;
            renderer.color = PaletteColor.color;
            renderer.material.SetFloat("SatMulti", PaletteColor.saturationMultiplier);
        }
    }
}
