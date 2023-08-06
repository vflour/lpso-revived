using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.Pagination;
using UnityEngine.UI;

public class PalettePageGenerator : MonoBehaviour
{
    public CreateAPetSprites sprites;
    public PetSpriteColorizer colorizer;
    public PagedRect pagedRect;
    public PetPaletteType paletteType;
    public GameObject buttonPrefab;
    public int maxPerPage = 8;

    public void GeneratePages()
    {
        var colors = sprites.SpeciesData.palette[paletteType];
        int spriteNum = colors.Count; 
        var pages = new UI.Pagination.Page[spriteNum / maxPerPage];

        for(int i = 0; i < spriteNum; i++)
        {
            int pageNum = i / maxPerPage;
            if (pages[pageNum] == null)
                pages[pageNum] = pagedRect.AddPageUsingTemplate();
            
            var buttonObject = Instantiate(buttonPrefab, pages[pageNum].transform);
            buttonObject.SetActive(true);
            PaletteButton paletteButton = buttonObject.GetComponent<PaletteButton>();
            paletteButton.colorizer = colorizer;
            paletteButton.paletteType = paletteType;
            paletteButton.Color = colors[i];
        }

    }
}
