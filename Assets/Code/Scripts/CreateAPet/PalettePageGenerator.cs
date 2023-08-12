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
    public Image powderSprite;
    public PetPaletteType paletteType;
    public GameObject buttonPrefab;
    public int maxPerPage = 8;

    private Dictionary<PaletteColor, PaletteButton> buttons = new Dictionary<PaletteColor, PaletteButton>();

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
            paletteButton.generator = this;
            paletteButton.Color = colors[i];
            buttons[colors[i]] = paletteButton;
        }

        UpdateColor(colors[0]);
    }


    private PaletteColor _prevColor;

    public void UpdateColor(PaletteColor color)
    {
        // Toggle the selected object
        if (_prevColor != null) buttons[_prevColor].ToggleSelected(false);
        buttons[color].ToggleSelected(true);
        _prevColor = color;

        // Switch the powder color
        powderSprite.color = color.color;
    }

    public void UpdatePetColor(PaletteColor color)
    {
        UpdateColor(color);
        sprites.currentPet.colors[paletteType] = color;
        colorizer.SwitchPalette(paletteType, sprites.currentPet.colors);
    }

}
