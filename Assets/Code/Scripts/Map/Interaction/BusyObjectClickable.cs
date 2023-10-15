using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BusyObjectClickable : ObjectClickable
{
    
    public const float ANIM_TIMEOUT = 5.0f;
    
    public override void handle(Vector3 globalPosition, Vector3 mousePosition)
    {
        base.handle(globalPosition, mousePosition);
        if (_moveable) StartCoroutine(AnimateWhenReached());
    }

    private IEnumerator PlayAndWaitForAnimation()
    {
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        // play, get state & wait
        bool completed = false;
        var finishHandler = animator.GetComponent<AnimationFinishHandler>();

        finishHandler.finishedAnimation.AddListener(() => completed = true);
        PlayAnimation();
        
        stopWatch.Start();
        yield return new WaitUntil(() => completed == true || stopWatch.Elapsed.Seconds > ANIM_TIMEOUT );
        stopWatch.Stop();

        if (ANIM_TIMEOUT < stopWatch.Elapsed.Seconds)
            Debug.LogError("Busy obj animation timed out");
    }

    private IEnumerator AnimateWhenReached()
    {
        // wait to finish moving
        yield return WaitForMoveable();
        if (_moveable.HasChangedDirection(endCoordinates)) yield break;

        // init placement
        PlaceMoveable(MovementState.Busy, characterPosition.position, characterCoordinates);
        // play anim
        yield return PlayAndWaitForAnimation();
        // set to normal
        _moveable.State = MovementState.Idle;
    }
}
