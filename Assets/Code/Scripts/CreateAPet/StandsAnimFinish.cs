using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StandsAnimFinish : MonoBehaviour
{
    public UnityEvent finishEvent;

    public void FinishAnimation()
    {
        finishEvent.Invoke();    
    }
}
