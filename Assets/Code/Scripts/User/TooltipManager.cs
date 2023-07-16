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
           currentTooltip.transform.position = Input.mousePosition;
       }
    }

    public void Focus(string msg)
    {
        Unfocus();
        currentTooltip = Instantiate(tooltip, canvas.transform); 
        Tooltip tooltipComponent = currentTooltip.GetComponent<Tooltip>();
        tooltipComponent.text.text = msg;
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
