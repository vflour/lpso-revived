using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageDisplay : MonoBehaviour
{

    public Page[] page;

    public TMP_Text titleText;
    public Image backgroundImage;

    public int pagenumber = 0;

    public GameObject canvas;
    public GameObject smallbutton;
    public int buttonwidth;
    public Sprite bluebutton;
    public GameObject[] buttons;
    public GameObject leftbutton;
    public GameObject rightbutton;
    public GameObject tempbutton;
    public GameObject pet;
    public GameObject[] petScreen;

    // Start is called before the first frame update
    void Start()
    {
        
            int pageamount = page.GetLength(0);
        buttons = new GameObject[pageamount];
        for (var i = 0; i < pageamount; i++)
        {
            GameObject tempbutton = Instantiate(smallbutton, new Vector3(Screen.width/2 + i * buttonwidth - (pageamount/2) * buttonwidth, 45, 0), Quaternion.identity);
            tempbutton.transform.SetParent(canvas.transform);
            buttons[i] = tempbutton;
        }

        updatepage();
    }
    void updatepage()
    {
        // changes the text and background image to correspond with each collect a pet page
        
        titleText.text = page[pagenumber].pagename;
        backgroundImage.sprite = page[pagenumber].background;

        // places page buttons at the bottom of the screen and automatically adds buttons for new pages

        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = smallbutton.GetComponent<Image>().sprite;
            buttons[i].transform.localScale = new Vector3(3.5f, 3.5f, 0);
        }

        //  changes the current corresponding screen button to a blue icon

        buttons[pagenumber].GetComponent <Image>().sprite = bluebutton;
        buttons[pagenumber].transform.localScale += new Vector3(0.5f, 0.5f, 0);

        // disables left and right buttons when there are no more pages to that direction

        if (pagenumber == 0)
        {
            leftbutton.SetActive(false);
        }
        else
        {
            leftbutton.SetActive(true);
        }

        if (pagenumber == page.GetLength(0) - 1)
        {
            rightbutton.SetActive(false);
        }
        else
        {
            rightbutton.SetActive(true);
        }
    }

    // takes user to next page in order if possible
    public void nextpage()
    {
        pagenumber += 1;
        updatepage();
    }

    //takes user to previous page in order if possible
    public void prevpage() 
    {
        pagenumber -= 1;
        updatepage();
    }

    void addButton(int ID)
    {
        //instantiate the button and catch it in a gameobject
        GameObject tempButton = Instantiate(pet);
        //this code is difficult, but i can break it down: 
        //tempButton.GetComponent<Button>() get your button
        //.onClick.AddListener(()  add a listener, this is basically what you do in unity editor usually
        //()=> means we give it a function between the {}
        tempButton.GetComponent<Button>().onClick.AddListener(() => {
            //write all the code here that you want to do with that pet
            //ID being the ID of the button you pressed, for example
            Debug.Log("Hi!");
            //  petScreen[ID].setActive(true);
        });
    }

}
