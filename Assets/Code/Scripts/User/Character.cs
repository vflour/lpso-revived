using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Moveable moveable;
    
    public GameObject characterModelPrefab; 
    private GameObject currentCharacterObject;
    public Animator animator;

    public void Spawn(Vector3Int coordinates) 
    {
        currentCharacterObject = Instantiate(characterModelPrefab, transform);
        moveable.coordinates = coordinates;
        animator = currentCharacterObject.GetComponent<Animator>();
    }
    
    // added here incase, might remove
    public void Despawn()
    {
        Destroy(currentCharacterObject);
    }
}
