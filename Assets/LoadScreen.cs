using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    public GameObject LoadScreenObject;

    // Start is called before the first frame update
    void Start()
    {
        LoadScreenObject.SetActive(true);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float time = 5f;
        while (time >= 0)
        {
            time -= .1f;
            yield return new WaitForSeconds(.1f);
        }
        LoadScreenObject.SetActive(false);
    }
}
