using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public CursorTypesPrefabs prefabs;
    private GameObject _currentCursor;

    private CursorType _type = CursorType.Idle;
   
    private void SetCursorObject()
    {
        if (_currentCursor)
        {
            Destroy(_currentCursor);
        }
        _currentCursor = Instantiate(prefabs.cursors[(int)_type], transform);
    }

    public CursorType Type
    {
        get { return _type; }
        
        set 
        {
            bool changed = _type != value;
            _type = value;
            if (changed)
                SetCursorObject();
        }

    }

    void Start()
    {
        SetCursorObject();
    }

    void Update()
    {
        // cursors are stored as sprites
        // since they like, need particle physics
        var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var oldPosition = _currentCursor.transform.position;
        var targetPosition = new Vector3(cursorPosition.x, cursorPosition.y, 1); 
        
        var newPosition = (oldPosition - targetPosition).magnitude < 0.15f ? Vector3.Lerp(oldPosition, targetPosition, 10*Time.deltaTime) : targetPosition;
        _currentCursor.transform.position = newPosition;
    }
}
