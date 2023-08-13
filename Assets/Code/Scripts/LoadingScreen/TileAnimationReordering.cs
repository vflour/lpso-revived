using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimationReordering : MonoBehaviour
{
    public Transform[] tiles;

    public void InitiaReorder()
    {
        tiles[0].SetAsFirstSibling();
        tiles[2].SetAsLastSibling();
    }

    public void SecondReorder()
    {
        tiles[3].SetAsFirstSibling();
        tiles[1].SetAsLastSibling();
    }
}
