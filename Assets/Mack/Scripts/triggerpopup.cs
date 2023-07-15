using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class Program
{
    static void Main()
    {
        Dictionary<string, int> happyWords =
            new Dictionary<string, int>();

        happyWords.Add("Doing Great!", 1);
        happyWords.Add("TKeep It Up!", 2);
        happyWords.Add("Fantastic!", 3);
        happyWords.Add("So Agile!", 4);
        happyWords.Add("Keep Going!", 5);
        happyWords.Add("Wow!", 6);
        happyWords.Add("Way to go!", 7);
        happyWords.Add("Balance Master!", 8);
    }
}

public class triggerpopup : MonoBehaviour
{

    public TextMeshProUGUI encouragement;
    public float texttimer;
    public float resettimer = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        texttimer = Time.deltaTime;
        if (texttimer < 0 )
        {
            changetext();
        }
    }

    void changetext()
    {
        Random.Range(1, 9);
        
        texttimer = resettimer;
    }
}
