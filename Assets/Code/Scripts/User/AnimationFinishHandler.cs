using UnityEngine;
using UnityEngine.Events;
public class AnimationFinishHandler: MonoBehaviour
{

    public readonly UnityEvent finishedAnimation = new UnityEvent();

    void TriggerEvents()
    {
        finishedAnimation.Invoke();
        finishedAnimation.RemoveAllListeners();
    }

}