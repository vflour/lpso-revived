using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CreateAPetUI : MonoBehaviour
{

    public GameObject Screen1;
    public GameObject Screen2;
    public GameObject Screen3;

    public Button Continue1;
    public int CurrentSpecies;
    public string[] Species;
    public int[] SubSpecies;
    public TMP_Text SpeciesDisplay1;
    public TMP_Text SpeciesNumberDisplay;
    public Button SpeciesLeft;
    public Button SpeciesRight;

    public Button Continue2;
    public Button Back1;
    public TMP_Text PetTypeText;
    public TMP_Text SpeciesDisplay2;

    public Button CreatePet;
    public Button Back2;
    public Button GenderButton;
    public Image GenderImage;
    public int CurrentGender;
    public Sprite[] Gender;
    public TMP_InputField PetName;

    void Start()
    {
        Screen2.SetActive(false);
        Screen3.SetActive(false);
        CurrentSpecies = 0;
        CurrentGender = 0;
    }

    public void SpeciesInfo()
    {
        SpeciesDisplay1.text = Species[CurrentSpecies];
        SpeciesNumberDisplay.text = "Choice 1 of " + SubSpecies[CurrentSpecies];
    }

    public void Screen1Active()
    {
        Screen1.SetActive(true);
        Screen2.SetActive(false);
        Screen3.SetActive(false);
        SpeciesInfo();
    }

    void Screen2Active()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(true);
        Screen3.SetActive(false);
        PetTypeText.text = "What kind of " + SpeciesDisplay1.text + " is your pet?";
        SpeciesDisplay2.text = SpeciesDisplay1.text;
    }

    public void Screen3Active()
    {
        Screen1.SetActive(false);
        Screen2.SetActive(false);
        Screen3.SetActive(true);
    }

    public void PressContinue()
    {
        if (SubSpecies[CurrentSpecies] == 1)
        { Screen3Active(); }
        else
        { Screen2Active(); }
    }

    public void PressBack()
    {
        if (SubSpecies[CurrentSpecies] == 1)
        { Screen1Active(); }
        else
        { Screen2Active(); }
    }

    public void SwitchSpeciesAdd()
    {
        if (CurrentSpecies == 14)
        {
            CurrentSpecies = 0;
            SpeciesInfo();
            return;
        }
        CurrentSpecies += 1;
        SpeciesInfo();
    }

    public void SwitchSpeciesSubtract()
    {
        if (CurrentSpecies < 1)
        {
            CurrentSpecies = 14;
            SpeciesInfo();
            return;
        }
        CurrentSpecies -= 1;
        SpeciesInfo();
    }

    public void SwitchGenders()
    {
        if (CurrentGender == 2) {
            CurrentGender = 0;
            GenderImage.sprite = Gender[CurrentGender];
            return;
        }
        CurrentGender += 1;
        GenderImage.sprite = Gender[CurrentGender];
    }
}
