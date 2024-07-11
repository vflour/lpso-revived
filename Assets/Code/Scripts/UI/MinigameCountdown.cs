using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCountdown : MonoBehaviour
{
    [SerializeField] private TextPopup textPopup;
    public float x;
    public float y;
    public bool promptReady;

    public int textSize;
    public int textSpeed;

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
    }
    public IEnumerator Countdown()
    {
        int countdownSize = textSize/3;
        float countdownHold = 0.7f;
        float countdownSpeed = textSpeed;

        if (promptReady)
        {
            textPopup.SpawnText(x, y, "Ready?", textPopup.Bluestone, textPopup.BSPink, textSize, 2.5f, textSpeed, 2.5f, 3f);
            yield return new WaitForSeconds(3);
        }
        textPopup.SpawnText(x, y, "3", textPopup.ArialBlack, textPopup.ABPink, countdownSize, countdownHold, countdownSpeed, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        textPopup.SpawnText(x, y, "2", textPopup.ArialBlack, textPopup.ABPink, countdownSize, countdownHold, countdownSpeed, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        textPopup.SpawnText(x, y, "1", textPopup.ArialBlack, textPopup.ABPink, countdownSize, countdownHold, countdownSpeed, 1.5f, 3f);
        yield return new WaitForSeconds(1);
        textPopup.SpawnText(x, y, "Go!", textPopup.Bluestone, textPopup.BSPink, countdownSize, countdownHold, countdownSpeed, 1.5f, 3f);
        yield break;
    }

    public void startEnding(bool wonGame)
    {
        StartCoroutine(Ending(wonGame));
    }
    public IEnumerator Ending(bool wonGame)
    {
        textPopup.SpawnText(x, y, "Well Done!", textPopup.Bluestone, textPopup.BSPink, textSize, 2.5f, textSpeed, 2.5f, 3f);
        yield return new WaitForSeconds(3f);
        string secondtext = "Try Again!";
        if (wonGame)
        {
            secondtext = "Goal Reached!";
        }
        textPopup.SpawnText(x, y, secondtext, textPopup.Bluestone, textPopup.BSPink, textSize*2/3, 2.5f, textSpeed, 2.5f, 3f);
    }
}
