using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAPetStands : MonoBehaviour
{
    public GameObject[] stands;
    public CreateAPetPages pages;
    public PetSpriteColorizer colorizer;

    public GameObject PetSprite => sprites[0];
    public PetData[] petData;

    private Transform GetStandHolder(string name)
    {
        return CurrentStand.transform.Find(name).Find("Render").Find("PetRigPlacement");
    }

    private int _previousPage = 0;
    public void UpdateStandHolders()
    {

        int page = pages.Page;
        if (_previousPage > 0) CurrentStand.SetActive(false); 
        _previousPage = page;
        CurrentStand.SetActive(true);
            
        // lazy way of getting stands from any object
        if (page < 3 ) {
            _spriteHolders[0] = GetStandHolder("StandFurtherLeft");
            _spriteHolders[1] = GetStandHolder("StandLeft");
            _spriteHolders[2] = GetStandHolder("StandMiddle");
            _spriteHolders[3] = GetStandHolder("StandRight");
            _spriteHolders[4] = GetStandHolder("StandFurtherRight");
        } else {
            _spriteHolders[0] = GetStandHolder("StandMiddle");
        }
    }

    public GameObject CurrentStand => stands[_previousPage - 1];
    private Animator StandAnimator => CurrentStand.GetComponent<Animator>();
   
    private GameObject[] _sprites;
    public GameObject[] sprites {
        get {
            return _sprites;
        }
        set {
            // remove old
            if (_sprites != null)
                foreach(GameObject sprite in _sprites)
                    Destroy(sprite);
            // add new ones
            _sprites = new GameObject[value.Length];
            for(int i = 0; i < value.Length; i++)
            {
                _sprites[i] = Instantiate(value[i], _spriteHolders[i]);
                foreach (SpriteRenderer sprite in _sprites[i].GetComponentsInChildren<SpriteRenderer>())
                    sprite.gameObject.layer = _spriteHolders[i].gameObject.layer;

                colorizer.SwitchAllPalettesToDefault(_sprites[i].GetComponent<PetSpritePalettes>(), petData[i]);
            }
        }
    }
    public Transform[] _spriteHolders = new Transform[5];

    // Play the platform anim
    public void Move(string direction) 
    {
        StandAnimator.SetTrigger(direction);
    }

    // Delegate Move animations
    // intended to be triggered by buttons
    public void MoveLeft() 
    {
        Move("Left");
    }

    public void MoveRight() 
    {
        Move("Right");
    }

}
