using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.InputAction;

public class RaycastInput : MonoBehaviour
{
    private const float distance = 10f;
    private static readonly Vector3 _mouseOffset = Vector3.back * distance;
    private static readonly Vector3 _raycastDirection = Vector3.forward;
    
    public void HandleClick(CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseOrigin = mouseWorldPosition + _mouseOffset;
            if (!EventSystem.current.IsPointerOverGameObject())	// is the touch on the GUI
            {
                RaycastHit2D hit = Physics2D.Raycast(mouseOrigin, _raycastDirection);
                if (hit.collider != null)
                {
                    Clickable clickable = hit.transform.GetComponent<Clickable>();
                    clickable?.handle(hit.point, Input.mousePosition);
                }
            }
        }

    }

}
