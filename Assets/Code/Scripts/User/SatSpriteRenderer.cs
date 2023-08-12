using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine;
using static PetPaletteType;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SatSpriteRenderer : MonoBehaviour 
{
    private bool _dirty = false;
    public SpriteRenderer renderer;
    
    private PetPaletteDict _paletteColor;    
    public PetPaletteDict PaletteColor 
    {
        set 
        {
            _paletteColor = value; 
            _dirty = true;
        }
        get { return _paletteColor; }
    }

    public void Start()
    {
        _dirty = true;
        renderer = GetComponent<SpriteRenderer>();
    }   

    public void Update()
    {
        if (_dirty && renderer != null)
        {
            _dirty = false;
            UpdateColor();
        }
    }

    public void UpdateColor()
    {
        SetupColor();
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);

            block.SetTexture("_MainTex", renderer.sprite.texture);
            block.SetFloat("_CSatMulti", PaletteColor[Coat].saturationMultiplier);
            block.SetFloat("_PSatMulti", PaletteColor[Patch].saturationMultiplier);
            block.SetFloat("_ESatMulti", PaletteColor[Eye].saturationMultiplier);

            block.SetColor("_CColor", PaletteColor[Coat].color);
            block.SetColor("_PColor", PaletteColor[Patch].color);
            block.SetColor("_EColor", PaletteColor[Eye].color);
        renderer.SetPropertyBlock(block);
    }

    public void SetupColor()
    {
        if (PaletteColor == null)
            _InitColor();           
        if (PaletteColor.Count <= 0)
            _InitColor();
        
    }

    private void _InitColor()
    {
        PaletteColor white = new PaletteColor(SerializableColor.white, 1); 
        _paletteColor = new PetPaletteDict {
            { Coat, white },
            { Patch, white },
            { Eye, white }
        };

    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SatSpriteRenderer)), CanEditMultipleObjects]
public class SatSpriteInspector : Editor
{
    private Dictionary<PetPaletteType, PaletteColor> palettes = new Dictionary<PetPaletteType, PaletteColor>();

    private void AddPetColorField(string label, PetPaletteType paletteType)
    {
        SatSpriteRenderer firstRenderer = (SatSpriteRenderer)target;
        if (firstRenderer == null) return;
        firstRenderer.SetupColor();
        
        PaletteColor paletteColor = firstRenderer.PaletteColor[paletteType];
        
        Color color = EditorGUILayout.ColorField(label, paletteColor.color);
        float saturation = EditorGUILayout.FloatField($"{label} Saturation", paletteColor.saturationMultiplier);
        palettes[paletteType] = new PaletteColor(color, saturation);

    }

    public void UpdatePalette(SatSpriteRenderer renderer, PetPaletteType paletteType)
    {
        renderer.PaletteColor[paletteType] = palettes[paletteType];
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AddPetColorField("Coat Color", Coat);
        AddPetColorField("Patch Color", Patch);
        AddPetColorField("Eye Color", Eye);
        
        foreach(var target in targets)
        {
            var renderer = (SatSpriteRenderer)target;
            if (renderer == null) return;
            renderer.SetupColor();
            UpdatePalette(renderer, Coat);
            UpdatePalette(renderer, Patch);
            UpdatePalette(renderer, Eye);
            renderer.UpdateColor();
            
        }
        
    }
}
#endif

