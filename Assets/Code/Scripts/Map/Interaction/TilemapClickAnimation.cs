using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapClickAnimation : MonoBehaviour
{   

    private GameObject _currentGraphic;
    public GameObject animatedClickGraphic;
    public MapMovement mapMovement;

    
    public void SpawnGraphic(Vector3Int coordinates)
    {
        Vector3 tilePosition = mapMovement.GetPosition(coordinates);
        RemoveGraphic();
        _currentGraphic = Instantiate(animatedClickGraphic, tilePosition, Quaternion.identity, transform);
        StartCoroutine(RemoveOnTimeout());    
    }
    
    private IEnumerator RemoveOnTimeout()
    {
        var currentRef = _currentGraphic;
        yield return new WaitForSeconds(0.95f);
        if (_currentGraphic == currentRef)
            Destroy(currentRef);
    }

    private void RemoveGraphic()
    {
        if (_currentGraphic)
        {
            Destroy(_currentGraphic);
            _currentGraphic = null;
        }
    }
}
