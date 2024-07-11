using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MunchItem : MonoBehaviour
{
    public int ItemNumber;
    public bool REdge = false;
    public bool LEdge = false;
    public bool TEdge = false;
    public bool BEdge = false;

    public GameObject lid;
    public bool selected = false;
    public bool revealed = false;

    public MatchnmunchLogic logic;
}
