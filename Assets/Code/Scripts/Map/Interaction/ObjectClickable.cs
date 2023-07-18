using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectClickable : MapClickable
{
    public Animator animator;
    public string animationStateName;
    [Header("Character animation position")]
    public Transform characterPosition;
    [Header("After the animation")]
    public Transform endPosition;

    protected Vector3Int characterCoordinates => mapMovement.tilemap.WorldToCell(characterPosition.position);
    protected Vector3Int endCoordinates => mapMovement.tilemap.WorldToCell(endPosition.position);

    void Start()
    {
        base.Awake();
        // assumes non-player
        animator = _moveable.GetComponent<Character>().animator;
    }

    public override void handle(Vector3 globalPosition, Vector3 mousePosition)
    {
        if (_moveable)
        {
            if (!CanMove) return;
            MoveToCoordinates(endCoordinates);
        }
    }

    public void PlayAnimation()
    {
        animator.Play(animationStateName);
    }

    protected IEnumerator WaitForMoveable()
    {
        yield return new WaitUntil(() => _moveable.IsNavigating == false);
    }

    protected void PlaceMoveable(MovementState movementState, Vector3 position, Vector3 coordinates)
    {
        _moveable.State = movementState;
        _moveable.coordinates = characterCoordinates;
        _moveable.transform.position = characterPosition.position;
    }

}