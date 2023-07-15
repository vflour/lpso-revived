using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CursorLogic : MonoBehaviour
{
    public GameObject CursorObj;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;  
    }

    // Update is called once per frame
    void Update()
    {
        CursorObj.transform.position = Input.mousePosition;
    }
}
