using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public Moveable moveable;
    
    public GameObject characterModelPrefab; 
    public GameObject currentCharacterObject;
    public Animator animator;
    public Transform characterParent;
    public UnityEvent Spawned;

    public void Spawn(Vector3Int coordinates) 
    {
        currentCharacterObject = Instantiate(characterModelPrefab, characterParent);
        moveable.coordinates = coordinates;
        animator = currentCharacterObject.GetComponent<Animator>();
        Spawned.Invoke();
    }

    // added here incase, might remove
    public void Despawn()
    {
        Destroy(currentCharacterObject);
    }
}
