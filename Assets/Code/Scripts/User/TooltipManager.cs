using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject tooltip;

    private GameObject currentTooltip;
    
    public void Update()
    {
       if (currentTooltip) 
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
        if(currentTooltip)
        {
            Destroy(currentTooltip);
            currentTooltip = null;
        }
    }
}
