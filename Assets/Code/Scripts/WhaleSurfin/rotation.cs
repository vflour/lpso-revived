using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{

    public float degreesrotated;
    public float balltimer;
    public float statictimer = 1;
    public int maxrotations = 15;
    public int currentrotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 2)>=1)
        {
            degreesrotated *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        balltimer -= Time.deltaTime;
        if (balltimer < 0 )
        {
            everySecond();
        }
        
        if (currentrotation > maxrotations)
        {
            everyFifteenSeconds();
        }
        this.transform.Rotate(0.0f, 0.0f, degreesrotated * Time.deltaTime);

    }
    void everyFifteenSeconds()
    {
        balltimer = statictimer;
        degreesrotated *= -1;
        currentrotation = 0;
    }

    void everySecond()
    {
        balltimer = statictimer;
        currentrotation += 1;
        float comparetimer = Random.Range(5.0f, 16.0f);
        if (comparetimer < currentrotation)
        {
            everyFifteenSeconds();
        }
    }
}
