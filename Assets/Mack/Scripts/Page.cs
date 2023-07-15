using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Page", menuName = "Page")]
public class Page : ScriptableObject
{
    public string pagename;

    public GameObject[] clickables;
    public Sprite background;
}
