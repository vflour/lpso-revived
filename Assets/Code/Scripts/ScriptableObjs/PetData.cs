using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PetData", menuName = "ScriptableObjects/PetData", order = 2)]
public class PetData : ScriptableObject
{
    public GameObject[] sprites;
    public PaletteColorsDict palette;
}

[Serializable]
public class ColorStorage : SerializableDictionary.Storage<List<PaletteColor>> {}
[Serializable]
public class PaletteColorsDict : SerializableDictionary<PetPaletteType, List<PaletteColor>, ColorStorage> {}
[Serializable]
public struct PaletteColor {
    public Color color;
    public float saturationMultiplier;
}
