using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreateAPetPages : MonoBehaviour
{
    public UnityEvent<int> PageChanged;

    private int _page;
    public int Page {
        get { return _page; }
        set { 
            _page = value;
            PageChanged.Invoke(_page);
        }
    }


}
