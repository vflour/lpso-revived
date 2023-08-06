using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreateAPetSprites : MonoBehaviour
{
    public CreateAPetStands stands;
    public PetDatabase database;
    public CreateAPetPages pages;
    public PetCreator petCreator;
    
    public Pet currentPet = new Pet();
    public PetData SpeciesData => database[currentPet.species];
   
    public GameObject[] GetPageSprites()
    {
        return GetPageSprites(pages.Page);
    }

    public GameObject[] GetPageSprites(int page) 
    {
        
        GameObject[] returned = null;
        switch(pages.Page) {
            case 1:
                returned = database.data.Select(data => data.sprites[0]).ToArray();
                break;
            case 2:
                returned = SpeciesData.sprites;
                break;
            case 3: 
                returned = new GameObject[] { SpeciesData.sprites[currentPet.subSpecies] };
                break;
            default: break;
        }
        return returned;
        
    }

    public PetData[] GetPageData(int page)
    {
        PetData[] returned;
        switch(pages.Page)
        {  

            case 3:
                returned = new PetData[] { SpeciesData };
                break;
            default:
                returned = database.data;
                break;
        }
        return returned;
    }

    public void UpdateSprites(int page)
    {

        petCreator.free = true;
        GameObject[] sprites = GetPageSprites(page);
        
        if (page < 3) 
        {
            int index = petCreator.DirectionIndex;
            int spriteCount = sprites.Length;

            int furtherLeft = (index - 2 + spriteCount) % spriteCount;
            int left = (index - 1 + spriteCount) % spriteCount;
            int center = index;
            int right = (index + 1 + spriteCount) % spriteCount;
            int furtherRight = (index + 2 + spriteCount) % spriteCount;
        
            stands.petData = GetPageData(page);
            stands.sprites = new GameObject[] {
                sprites[furtherLeft],
                sprites[left],
                sprites[center],
                sprites[right],
                sprites[furtherRight]
            };
            
            

        } else 
        {
            stands.sprites = sprites;
        } 
    }

    public void UpdateSprites()
    {
        UpdateSprites(pages.Page);
    }

}
