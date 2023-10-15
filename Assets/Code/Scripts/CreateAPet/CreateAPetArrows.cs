using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for moving the platforms in CrAP with the arrow controls
/// </summary>
public class CreateAPetArrows : MonoBehaviour
{
    public CreateAPetPages pages;
    public CreateAPetSprites sprites;

    public bool free = true;

    // Handle the arrow direction change based on page
    private int _index;
    public int DirectionIndex {
        get { return _index; }
        set {
            var pageSprites = sprites.GetPageSprites(pages.Page);
            var spriteCount = pageSprites.Length;

            _index = (value + spriteCount) % spriteCount;
            switch(pages.Page) {
                case 1:
                    sprites.currentPet.species = sprites.database[_index].name;
                    break;
                case 2:
                    sprites.currentPet.subSpecies = _index;
                    break;
                default: break;
            }
        }
    }
    
    // 
    public void Move(string direction) 
    {
        if (free)
        {
            free = false;
            DirectionIndex += direction == "Left" ? -1 : 1;
            sprites.stands.Move(direction);
        }
    }    

    // Delegate Move animations
    // intended to be triggered by graphs
    public void MoveLeft() 
    {
        Move("Left");
    }

    public void MoveRight() 
    {
        Move("Right");
    }

    public void Start()
    {
        pages.Page = 1;
        DirectionIndex = 0;
        sprites.UpdateSprites();
    }

}
