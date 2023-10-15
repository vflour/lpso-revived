using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject tooltip;

    private GameObject currentTooltip;
    public bool IsFocused => currentTooltip != null;

    public void Update()
    {
        if (IsFocused) 
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = 100.0f;
            currentTooltip.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }

    public void Focus(TooltipData data)
    {
        Unfocus();
        currentTooltip = Instantiate(tooltip, canvas.transform); 
        Tooltip tooltipComponent = currentTooltip.GetComponent<Tooltip>();
        tooltipComponent.data = data; 
    }

    public void Unfocus()
    { 
        if(IsFocused)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}
